using Castle.DynamicProxy;
using System;
using System.Reflection;

namespace VDT.Core.DependencyInjection.Decorators {
    internal sealed class DecoratorInterceptor : IInterceptor {
        private readonly IDecorator decorator;
        private readonly Func<MethodInfo, bool> predicate;

        internal DecoratorInterceptor(IDecorator decorator, Func<MethodInfo, bool> predicate) {
            this.decorator = decorator;
            this.predicate = predicate;
        }

        public void Intercept(IInvocation invocation) {
            if (ShouldIntercept(invocation.Method)) {
                var context = new MethodExecutionContext(invocation.TargetType, invocation.InvocationTarget, invocation.Method, invocation.Arguments, invocation.GenericArguments);

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
            else {
                invocation.Proceed();
            }
        }

        private bool ShouldIntercept(MethodInfo methodInfo) {
            return predicate(methodInfo);
        }
    }
}
