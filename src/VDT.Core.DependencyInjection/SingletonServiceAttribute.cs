using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Marks a service to be registered as a singleton service when calling <see cref="ServiceCollectionExtensions.AddAttributeServices"/>
    /// </summary>
    public sealed class SingletonServiceAttribute : ServiceAttribute {
        private static readonly MethodInfo addServiceMethod = typeof(ServiceCollectionServiceExtensions).GetMethod(nameof(ServiceCollectionServiceExtensions.AddSingleton), 2, BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(IServiceCollection) }, null) ?? throw new InvalidOperationException($"Method '{nameof(ServiceCollectionServiceExtensions)}.{nameof(ServiceCollectionServiceExtensions.AddSingleton)}' was not found.");
        private static readonly MethodInfo addDecoratedServiceMethod = typeof(Decorators.ServiceCollectionExtensions).GetMethod(nameof(Decorators.ServiceCollectionExtensions.AddSingleton), 2, BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(IServiceCollection), typeof(Action<Decorators.DecoratorOptions>) }, null) ?? throw new InvalidOperationException($"Method '{nameof(Decorators.ServiceCollectionExtensions)}.{nameof(Decorators.ServiceCollectionExtensions.AddSingleton)}' was not found.");

        /// <summary>
        /// Marks a service to be registered as a singleton service when calling <see cref="ServiceCollectionExtensions.AddAttributeServices"/>
        /// </summary>
        /// <param name="implementationType">The type to use as implementation for this service</param>
        /// <remarks>When using decorators, the type specified in <paramref name="implementationType"/> must differ from the service type</remarks>
        public SingletonServiceAttribute(Type implementationType) : base(implementationType) { }

        internal override void Register(IServiceCollection services, Type serviceType) {
            addServiceMethod.MakeGenericMethod(serviceType, ImplementationType).Invoke(null, new object[] { services });
        }

        internal override void Register(IServiceCollection services, Type serviceType, Action<Decorators.DecoratorOptions> decoratorSetupAction) {
            addDecoratedServiceMethod.MakeGenericMethod(serviceType, ImplementationType).Invoke(null, new object[] { services, decoratorSetupAction });
        }
    }
}
