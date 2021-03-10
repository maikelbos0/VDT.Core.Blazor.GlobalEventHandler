using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace VDT.Core.DependencyInjection.Decorators {
    internal sealed class DecoratorInterceptor : IInterceptor {
        private static readonly MethodInfo decorateTaskWithResultMethod = typeof(DecoratorInterceptor)
            .GetMethod(nameof(DecorateTaskWithResult), 1, BindingFlags.NonPublic | BindingFlags.Static, typeof(IDecorator), typeof(IInvocation), typeof(MethodExecutionContext));

        private static readonly Dictionary<MethodInfo, Action<IDecorator, IInvocation, MethodExecutionContext>> decoratorActions = new Dictionary<MethodInfo, Action<IDecorator, IInvocation, MethodExecutionContext>>();

        private static Action<IDecorator, IInvocation, MethodExecutionContext> GetDecoratorAction(MethodInfo method) {
            if (!decoratorActions.TryGetValue(method, out var action)) {
                if (method.ReturnType == typeof(Task)) {
                    action = DecorateTask;
                }
                else if (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>)) {
                    action = (Action<IDecorator, IInvocation, MethodExecutionContext>)decorateTaskWithResultMethod
                        .MakeGenericMethod(method.ReturnType.GetGenericArguments())
                        .CreateDelegate(typeof(Action<IDecorator, IInvocation, MethodExecutionContext>));
                }
                else {
                    action = Decorate;
                }

                decoratorActions[method] = action;
            }

            return action;
        }

        private static void DecorateTask(IDecorator decorator, IInvocation invocation, MethodExecutionContext context) {
            decorator.BeforeExecute(context);

            invocation.Proceed();
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

        private static void DecorateTaskWithResult<TResult>(IDecorator decorator, IInvocation invocation, MethodExecutionContext context) {
            decorator.BeforeExecute(context);

            invocation.Proceed();
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

        private static void Decorate(IDecorator decorator, IInvocation invocation, MethodExecutionContext context) {
            decorator.BeforeExecute(context);

            try {
                invocation.Proceed();
            }
            catch (Exception ex) {
                decorator.OnError(context, ex);
                throw;
            }

            decorator.AfterExecute(context);
        }

        private readonly IDecorator decorator;
        private readonly Predicate<MethodInfo> predicate;

        internal DecoratorInterceptor(IDecorator decorator, Predicate<MethodInfo> predicate) {
            this.decorator = decorator;
            this.predicate = predicate;
        }

        public void Intercept(IInvocation invocation) {
            if (ShouldDecorate(invocation.Method)) {
                var context = new MethodExecutionContext(invocation.TargetType, invocation.InvocationTarget, invocation.Method, invocation.Arguments, invocation.GenericArguments);
                var decoratorAction = GetDecoratorAction(invocation.Method);

                decoratorAction(decorator, invocation, context);
            }
            else {
                invocation.Proceed();
            }
        }

        private bool ShouldDecorate(MethodInfo methodInfo) {
            return predicate(methodInfo);
        }
    }
}
