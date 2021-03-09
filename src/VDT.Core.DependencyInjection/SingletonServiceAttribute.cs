using Microsoft.Extensions.DependencyInjection;
using System;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Marks a service to be registered as a singleton service when calling <see cref="ServiceCollectionExtensions.AddAttributeServices"/>
    /// </summary>
    public sealed class SingletonServiceAttribute : ServiceAttribute {
        /// <summary>
        /// Marks a service to be registered as a singleton service when calling <see cref="ServiceCollectionExtensions.AddAttributeServices"/>
        /// </summary>
        /// <param name="implementationType">The type to use as implementation for this service</param>
        /// <remarks>When using decorators, the type specified in <paramref name="implementationType"/> must differ from the service type</remarks>
        public SingletonServiceAttribute(Type implementationType) : base(implementationType) { }

        internal override void Register(IServiceCollection services, Type serviceType) {
        }

        internal override void Register(IServiceCollection services, Type serviceType, Action<Decorators.DecoratorOptions> decoratorSetupAction) {
        }
    }
}
