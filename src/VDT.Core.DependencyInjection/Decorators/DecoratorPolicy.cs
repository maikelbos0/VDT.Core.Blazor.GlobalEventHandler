using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace VDT.Core.DependencyInjection.Decorators {
    internal abstract class DecoratorPolicy {
        private readonly Func<MethodInfo, bool> shouldInterceptFunc;

        internal DecoratorPolicy(Func<MethodInfo, bool> shouldInterceptFunc) {
            this.shouldInterceptFunc = shouldInterceptFunc;
        }

        internal bool ShouldIntercept(MethodInfo methodInfo) {
            return shouldInterceptFunc(methodInfo);
        }

        internal abstract IDecorator Resolve(IServiceProvider serviceProvider);
    }

    internal class DecoratorPolicy<TDecorator> : DecoratorPolicy where TDecorator : class, IDecorator {
        internal DecoratorPolicy(Func<MethodInfo, bool> shouldInterceptFunc) : base(shouldInterceptFunc) { }

        internal override IDecorator Resolve(IServiceProvider serviceProvider) {
            return serviceProvider.GetRequiredService<TDecorator>();
        }
    }
}
