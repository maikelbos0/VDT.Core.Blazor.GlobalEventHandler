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
        /// Provides a mechanism to register all services found by the provided service type finders in the assemblies provided in the options
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to</param>
        /// <param name="setupAction">The action that sets up the options for finding and registering services to this collection</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public static IServiceCollection AddServices(this IServiceCollection services, Action<ServiceRegistrationOptions> setupAction) {
            var options = GetOptions(setupAction);

            foreach (var context in GetServices(options)) {
                var serviceLifetime = options.ServiceLifetimeProvider?.Invoke(context.ServiceType, context.ImplementationType) ?? options.DefaultServiceLifetime;

                if (options.ServiceRegistrar != null) {
                    options.ServiceRegistrar(services, context.ServiceType, context.ImplementationType, serviceLifetime);
                }
                else {
                    services.Add(new ServiceDescriptor(context.ServiceType, context.ImplementationType, serviceLifetime));
                }
            }

            return services;
        }

        private static ServiceRegistrationOptions GetOptions(Action<ServiceRegistrationOptions> setupAction) {
            var options = new ServiceRegistrationOptions();

            setupAction(options);

            return options;
        }

        private static IEnumerable<ServiceContext> GetServices(ServiceRegistrationOptions options) {
            return options
                .Assemblies
                .SelectMany(a => options.ServiceTypeFinders.Select(f => new { Assembly = a, ServiceTypeFinder = f }))
                .SelectMany(x => GetServices(x.Assembly, x.ServiceTypeFinder));
        }

        private static IEnumerable<ServiceContext> GetServices(Assembly assembly, ServiceTypeFinder serviceTypeFinder) {
            return assembly
                .GetTypes()
                .Where(t => !t.IsInterface && !t.IsAbstract && !t.IsGenericTypeDefinition)
                .SelectMany(implementationType => serviceTypeFinder(implementationType).Select(serviceType => new ServiceContext(serviceType, implementationType)));
        }
    }
}
