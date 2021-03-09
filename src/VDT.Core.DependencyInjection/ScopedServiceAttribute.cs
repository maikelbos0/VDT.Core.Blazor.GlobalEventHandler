using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Marks a service to be registered as a scoped service when calling <see cref="ServiceCollectionExtensions.AddAttributeServices"/>
    /// </summary>
    public sealed class ScopedServiceAttribute : ServiceAttribute {
        private static readonly MethodInfo addServiceMethod = typeof(ServiceCollectionServiceExtensions).GetMethod(nameof(ServiceCollectionServiceExtensions.AddScoped), 2, BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(IServiceCollection) }, null) ?? throw new InvalidOperationException($"Method '{nameof(ServiceCollectionServiceExtensions)}.{nameof(ServiceCollectionServiceExtensions.AddScoped)}' was not found.");
        private static readonly MethodInfo addDecoratedServiceMethod = typeof(Decorators.ServiceCollectionExtensions).GetMethod(nameof(Decorators.ServiceCollectionExtensions.AddScoped), 2, BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(IServiceCollection), typeof(Action<Decorators.DecoratorOptions>) }, null) ?? throw new InvalidOperationException($"Method '{nameof(Decorators.ServiceCollectionExtensions)}.{nameof(Decorators.ServiceCollectionExtensions.AddScoped)}' was not found.");

        /// <summary>
        /// Marks a service to be registered as a scoped service when calling <see cref="ServiceCollectionExtensions.AddAttributeServices"/>
        /// </summary>
        /// <param name="implementationType">The type to use as implementation for this service</param>
        /// <remarks>When using decorators, the type specified in <paramref name="implementationType"/> must differ from the service type</remarks>
        public ScopedServiceAttribute(Type implementationType) : base(implementationType) { }

        internal override void Register(IServiceCollection services, Type serviceType) {
            addServiceMethod.MakeGenericMethod(serviceType, ImplementationType).Invoke(null, new object[] { services });
        }

        internal override void Register(IServiceCollection services, Type serviceType, Action<Decorators.DecoratorOptions> decoratorSetupAction) {
        }
    }
}
