using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace VDT.Core.DependencyInjection.Decorators {
    /// <summary>
    /// Extension methods for adding decorators to services to be registered using <see cref="ServiceRegistrationOptions"/>
    /// </summary>
    public static class ServiceRegistrationOptionsExtensions {
        private static readonly Dictionary<ServiceLifetime, MethodInfo> decoratedMethods = new Dictionary<ServiceLifetime, MethodInfo>() {
            { ServiceLifetime.Transient, typeof(ServiceCollectionExtensions)
                .GetMethod(nameof(ServiceCollectionExtensions.AddTransient), 2, BindingFlags.Public | BindingFlags.Static, typeof(IServiceCollection), typeof(Action<DecoratorOptions>)) },
            { ServiceLifetime.Scoped, typeof(ServiceCollectionExtensions)
                .GetMethod(nameof(ServiceCollectionExtensions.AddScoped), 2, BindingFlags.Public | BindingFlags.Static, typeof(IServiceCollection), typeof(Action<DecoratorOptions>)) },
            { ServiceLifetime.Singleton, typeof(ServiceCollectionExtensions)
                .GetMethod(nameof(ServiceCollectionExtensions.AddSingleton), 2, BindingFlags.Public | BindingFlags.Static, typeof(IServiceCollection), typeof(Action<DecoratorOptions>)) },
        };

        /// <summary>
        /// Use a service registrar that applies decorators to services using the provided setup action
        /// </summary>
        /// <param name="options">The options for registering services</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public static ServiceRegistrationOptions UseDecoratorServiceRegistrar(this ServiceRegistrationOptions options, Action<DecoratorOptions> setupAction) 
            => options.UseServiceRegistrar((services, serviceType, implementationType, serviceLifetime) 
                => decoratedMethods[serviceLifetime].MakeGenericMethod(serviceType, implementationType).Invoke(null, new object[] { services, setupAction }));
    }
}
