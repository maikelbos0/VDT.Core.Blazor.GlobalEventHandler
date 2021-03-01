using Castle.DynamicProxy;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace VDT.Core.DependencyInjection.Decorators {
    internal sealed class DecoratorInterceptor : IInterceptor {
        private readonly IDecorator decorator;
        private readonly Predicate<MethodInfo> predicate;

        internal DecoratorInterceptor(IDecorator decorator, Predicate<MethodInfo> predicate) {
            this.decorator = decorator;
            this.predicate = predicate;
        }

        public void Intercept(IInvocation invocation) {
            if (ShouldDecorate(invocation.Method)) {
                var context = new MethodExecutionContext(invocation.TargetType, invocation.InvocationTarget, invocation.Method, invocation.Arguments, invocation.GenericArguments);

                decorator.BeforeExecute(context);

                if (IsAsync(invocation.Method)) {
                    DecorateAsync(invocation, context);
                }
                else {
                    Decorate(invocation, context);
                }
            }
            else {
                invocation.Proceed();
            }
        }

        private void DecorateAsync(IInvocation invocation, MethodExecutionContext context) {
            invocation.Proceed();

            if (HasReturnValue(invocation.Method)) {
                var decorator = typeof(DecoratorInterceptor).GetMethod(nameof(DecorateTaskWithResult), BindingFlags.NonPublic | BindingFlags.Instance)?
                    .MakeGenericMethod(context.Method.ReturnType.GetGenericArguments()) ?? throw new InvalidOperationException($"Method '{nameof(DecoratorInterceptor)}.{nameof(DecorateTaskWithResult)}' was not found.");

                decorator.Invoke(this, new object[] { invocation, context });
            }
            else {
                DecorateTask(invocation, context);
            }
        }

        private void DecorateTask(IInvocation invocation, MethodExecutionContext context) {
            invocation.ReturnValue = ((Func<Task>)(async () => {
                try {
                    await (Task)invocation.ReturnValue;
                }
                catch (Exception ex) {
                    decorator.OnError(context, ex);
                    throw;
                }

                decorator.AfterExecute(context);
            }))();
        }

        private void DecorateTaskWithResult<TResult>(IInvocation invocation, MethodExecutionContext context) {
            invocation.ReturnValue = ((Func<Task<TResult>>)(async () => {
                var task = (Task<TResult>)invocation.ReturnValue;

                try {
                    await task;
                }
                catch (Exception ex) {
                    decorator.OnError(context, ex);
                    throw;
                }

                decorator.AfterExecute(context);

                return task.Result;
            }))();
        }

        private void Decorate(IInvocation invocation, MethodExecutionContext context) {
            try {
                invocation.Proceed();
            }
            catch (Exception ex) {
                decorator.OnError(context, ex);
                throw;
            }

            decorator.AfterExecute(context);
        }

        private bool HasReturnValue(MethodInfo method) {
            return method.ReturnType != typeof(void) && method.ReturnType != typeof(Task);
        }

        private bool IsAsync(MethodInfo method) {
            return method.ReturnType == typeof(Task) || (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>));
        }

        private bool ShouldDecorate(MethodInfo methodInfo) {
            return predicate(methodInfo);
        }
    }
}
