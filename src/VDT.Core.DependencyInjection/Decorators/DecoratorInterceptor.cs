using Castle.DynamicProxy;
using System;
using System.Reflection;

namespace VDT.Core.DependencyInjection.Decorators {
    internal class DecoratorInterceptor : IInterceptor {
        private readonly IDecorator decorator;
        private readonly Func<MethodInfo, bool> predicate;

        internal DecoratorInterceptor(IDecorator decorator, Func<MethodInfo, bool> predicate) {
            this.decorator = decorator;
            this.predicate = predicate;
        }

        public void Intercept(IInvocation invocation) {
            if (ShouldIntercept(invocation.Method)) {
                throw new NotImplementedException();
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
