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
        /// Signature for methods that return service types for a given implementation type
        /// </summary>
        /// <param name="implementationType">The implementation type for which to return service types</param>
        /// <returns>An enumerable of service types to register this implementation type for</returns>
        public delegate IEnumerable<Type> ServiceTypeFinder(Type implementationType);

        /// <summary>
        /// Signature for methods that return a service lifetime for a given service and implementation type
        /// </summary>
        /// <param name="serviceType">The service type</param>
        /// <param name="implementationType">The implementation type</param>
        /// <returns>A service lifetime to apply to this service registration</returns>
        public delegate ServiceLifetime ServiceLifetimeProvider(Type serviceType, Type implementationType);

        /// <summary>
        /// Provides a mechanism to register all services found in <paramref name="assembly"/> where the <paramref name="serviceTypeFinder"/> finds service types for an implementation type as transient services
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to</param>
        /// <param name="assembly">The <see cref="Assembly"/> in which to look for services</param>
        /// <param name="serviceTypeFinder">The method that will return service types for any given implementation type</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public static IServiceCollection AddTransientServices(this IServiceCollection services, Assembly assembly, ServiceTypeFinder serviceTypeFinder) {
            return services.AddServices(assembly, serviceTypeFinder, (serviceType, implementationType) => ServiceLifetime.Transient);
        }
        /// <summary>
        /// Provides a mechanism to register all services found in <paramref name="assembly"/> where the <paramref name="serviceTypeFinder"/> finds service types for an implementation type as transient services
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to</param>
        /// <param name="assembly">The <see cref="Assembly"/> in which to look for services</param>
        /// <param name="serviceTypeFinder">The method that will return service types for any given implementation type</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public static IServiceCollection AddScopedServices(this IServiceCollection services, Assembly assembly, ServiceTypeFinder serviceTypeFinder) {
            return services.AddServices(assembly, serviceTypeFinder, (serviceType, implementationType) => ServiceLifetime.Scoped);
        }
        /// <summary>
        /// Provides a mechanism to register all services found in <paramref name="assembly"/> where the <paramref name="serviceTypeFinder"/> finds service types for an implementation type as transient services
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to</param>
        /// <param name="assembly">The <see cref="Assembly"/> in which to look for services</param>
        /// <param name="serviceTypeFinder">The method that will return service types for any given implementation type</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public static IServiceCollection AddSingletonServices(this IServiceCollection services, Assembly assembly, ServiceTypeFinder serviceTypeFinder) {
            return services.AddServices(assembly, serviceTypeFinder, (serviceType, implementationType) => ServiceLifetime.Singleton);
        }

        /// <summary>
        /// Provides a mechanism to register all services found in <paramref name="assembly"/> where the <paramref name="serviceTypeFinder"/> finds service types for an implementation type
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to</param>
        /// <param name="assembly">The <see cref="Assembly"/> in which to look for services</param>
        /// <param name="serviceTypeFinder">The method that will return service types for any given implementation type</param>
        /// <param name="serviceLifetimeProvider">The method that will return a <see cref="ServiceLifetime"/> for a given service type and implementation type</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public static IServiceCollection AddServices(this IServiceCollection services, Assembly assembly, ServiceTypeFinder serviceTypeFinder, ServiceLifetimeProvider serviceLifetimeProvider) {
            foreach (var context in GetServices(assembly, serviceTypeFinder)) {
                services.Add(new ServiceDescriptor(context.ServiceType, context.ImplementationType, serviceLifetimeProvider(context.ServiceType, context.ImplementationType)));
            }

            return services;
        }

        private static IEnumerable<ServiceContext> GetServices(Assembly assembly, ServiceTypeFinder serviceTypeFinder) {
            return assembly
                .GetTypes()
                .Where(t => !t.IsInterface && !t.IsAbstract)
                .SelectMany(implementationType => serviceTypeFinder(implementationType).Select(serviceType => new ServiceContext(serviceType, implementationType)));
        }
    }
}
