using Castle.DynamicProxy;
using System;

namespace VDT.Core.DependencyInjection.Decorators {
    internal class DecoratorInterceptor : IInterceptor {
        private readonly IDecorator decorator;

        public DecoratorInterceptor(IDecorator decorator) {
            this.decorator = decorator;
        }

        public void Intercept(IInvocation invocation) {
            throw new NotImplementedException();
        }
    }
}
