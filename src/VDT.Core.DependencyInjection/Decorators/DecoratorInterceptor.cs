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

                if (IsAsync(invocation.Method)) {
                    _ = DecorateAsync(invocation, context);
                }
                else {
                    Decorate(invocation, context);
                }
            }
            else {
                invocation.Proceed();
            }
        }

        private async Task DecorateAsync(IInvocation invocation, MethodExecutionContext context) {
            decorator.BeforeExecute(context);

            try {
                invocation.Proceed();

                await (Task)invocation.ReturnValue;
            }
            catch (Exception ex) {
                decorator.OnError(context, ex);
                throw;
            }

            decorator.AfterExecute(context);
        }

        private void Decorate(IInvocation invocation, MethodExecutionContext context) {
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

        private bool IsAsync(MethodInfo method) {
            return method.ReturnType == typeof(Task) || (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>));
        }

        private bool ShouldDecorate(MethodInfo methodInfo) {
            return predicate(methodInfo);
        }
    }
}
