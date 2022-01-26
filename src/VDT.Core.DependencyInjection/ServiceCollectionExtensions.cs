using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VDT.Core.DependencyInjection.Decorators;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Extension methods for adding services to an <see cref="IServiceCollection"/> based on attributes
    /// </summary>
    public static class ServiceCollectionExtensions {
        /// <summary>
        /// Provides a mechanism to register all services found in <paramref name="assembly"/> marked with <see cref="TransientServiceAttribute"/>, <see cref="ScopedServiceAttribute"/> or <see cref="SingletonServiceAttribute"/>
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to</param>
        /// <param name="assembly">The <see cref="Assembly"/> in which to look for services</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public static IServiceCollection AddAttributeServices(this IServiceCollection services, Assembly assembly) {
            foreach (var context in GetServiceAttributes(assembly)) {
                context.Attribute.Register(services, context.Type);
            }

            return services;
        }

        /// <summary>
        /// Provides a mechanism to register all services found in <paramref name="assembly"/> marked with <see cref="TransientServiceAttribute"/>, <see cref="ScopedServiceAttribute"/> or <see cref="SingletonServiceAttribute"/>
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to</param>
        /// <param name="assembly">The <see cref="Assembly"/> in which to look for services</param>
        /// <param name="decoratorSetupAction">The action that sets up the decorators for these services</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public static IServiceCollection AddAttributeServices(this IServiceCollection services, Assembly assembly, Action<DecoratorOptions> decoratorSetupAction) {
            foreach (var context in GetServiceAttributes(assembly)) {
                context.Attribute.Register(services, context.Type, decoratorSetupAction);
            }

            return services;
        }

        private static IEnumerable<ServiceAttributeContext> GetServiceAttributes(Assembly assembly) {
            return assembly
                .GetTypes()
                .Select(t => new ServiceAttributeContext(t, t.GetCustomAttribute<ServiceAttribute>()!))
                .Where(s => s.Attribute != null);
        }

        /// <summary>
        /// Provides a mechanism to register all services found in <paramref name="assembly"/> where the <paramref name="serviceFinder"/> finds service types for an implementation type as transient services
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to</param>
        /// <param name="assembly">The <see cref="Assembly"/> in which to look for services</param>
        /// <param name="serviceFinder">The method that will return service types for any given implementation type</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public static IServiceCollection AddTransientServices(this IServiceCollection services, Assembly assembly, Func<Type, IEnumerable<Type>> serviceFinder) {
            foreach (var context in GetServices(assembly, serviceFinder)) {
                services.AddTransient(context.ServiceType, context.ImplementationType);
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
