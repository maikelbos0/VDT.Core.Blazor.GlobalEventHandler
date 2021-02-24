using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace VDT.Core.DependencyInjection.Decorators {
    internal abstract class DecoratorPolicy {
        internal Func<MethodInfo, bool> Predicate { get; }

        internal DecoratorPolicy(Func<MethodInfo, bool> predicate) {
            Predicate = predicate;
        }

        internal abstract IDecorator GetDecorator(IServiceProvider serviceProvider);
    }

    internal class DecoratorPolicy<TDecorator> : DecoratorPolicy where TDecorator : class, IDecorator {
        internal DecoratorPolicy(Func<MethodInfo, bool> predicate) : base(predicate) { }

        internal override IDecorator GetDecorator(IServiceProvider serviceProvider) {
            return serviceProvider.GetRequiredService<TDecorator>();
        }
    }
}
