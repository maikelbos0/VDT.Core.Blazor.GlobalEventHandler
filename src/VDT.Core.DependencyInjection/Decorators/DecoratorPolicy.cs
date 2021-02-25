using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace VDT.Core.DependencyInjection.Decorators {
    internal abstract class DecoratorPolicy {
        internal Predicate<MethodInfo> Predicate { get; }

        internal DecoratorPolicy(Predicate<MethodInfo> predicate) {
            Predicate = predicate;
        }

        internal abstract IDecorator GetDecorator(IServiceProvider serviceProvider);
    }

    internal sealed class DecoratorPolicy<TDecorator> : DecoratorPolicy where TDecorator : class, IDecorator {
        internal DecoratorPolicy(Predicate<MethodInfo> predicate) : base(predicate) { }

        internal override IDecorator GetDecorator(IServiceProvider serviceProvider) {
            return serviceProvider.GetRequiredService<TDecorator>();
        }
    }
}
