using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Extension methods for adding services to an <see cref="IServiceCollection"/> based on type and interface conventions
    /// </summary>
    public static class ServiceCollectionConventionExtensions {

        /// <summary>
        /// Provides a mechanism to register all services found in <paramref name="assembly"/> where the <paramref name="serviceFinder"/> finds service types for an implementation type as transient services
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to</param>
        /// <param name="assembly">The <see cref="Assembly"/> in which to look for services</param>
        /// <param name="serviceFinder">The method that will return service types for any given implementation type</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public static IServiceCollection AddTransientServices(this IServiceCollection services, Assembly assembly, Func<Type, IEnumerable<Type>> serviceFinder) {
            return services.AddServices(assembly, serviceFinder, (serviceType, implementationType) => ServiceLifetime.Transient);
        }

        /// <summary>
        /// Provides a mechanism to register all services found in <paramref name="assembly"/> where the <paramref name="serviceFinder"/> finds service types for an implementation type
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to</param>
        /// <param name="assembly">The <see cref="Assembly"/> in which to look for services</param>
        /// <param name="serviceFinder">The method that will return service types for any given implementation type</param>
        /// <param name="lifetimeProvider">The method that will return a <see cref="ServiceLifetime"/> for a given service type and implementation type</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public static IServiceCollection AddServices(this IServiceCollection services, Assembly assembly, Func<Type, IEnumerable<Type>> serviceFinder, Func<Type, Type, ServiceLifetime> lifetimeProvider) {
            foreach (var context in GetServices(assembly, serviceFinder)) {
                services.Add(new ServiceDescriptor(context.ServiceType, context.ImplementationType, lifetimeProvider(context.ServiceType, context.ImplementationType)));
            }

            return services;
        }

        private static IEnumerable<ServiceContext> GetServices(Assembly assembly, Func<Type, IEnumerable<Type>> serviceFinder) {
            return assembly
                .GetTypes()
                .Where(t => !t.IsInterface && !t.IsAbstract)
                .SelectMany(implementationType => serviceFinder(implementationType).Select(serviceType => new ServiceContext(serviceType, implementationType)));
        }
    }
}
