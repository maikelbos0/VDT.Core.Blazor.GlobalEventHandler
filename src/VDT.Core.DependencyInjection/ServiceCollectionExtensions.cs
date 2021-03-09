using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using VDT.Core.DependencyInjection.Decorators;

namespace VDT.Core.DependencyInjection {
    public static class ServiceCollectionExtensions {
        /// <summary>
        /// Provides a mechanism to register all services found in <paramref name="assembly"/> marked with <see cref="TransientServiceAttribute"/>, <see cref="ScopedServiceAttribute"/> or <see cref="SingletonServiceAttribute"/>
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to</param>
        /// <param name="assembly">The <see cref="Assembly"/> in which to look for services</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public static IServiceCollection AddAttributeServices(this IServiceCollection services, Assembly assembly) {
            var attributeServices = assembly
                .GetTypes()
                .Select(t => new {
                    ServiceType = t,
                    Attribute = t.GetCustomAttribute<ServiceAttribute>()
                })
                .Where(s => s.Attribute != null);

            foreach (var service in attributeServices) {
                service.Attribute!.Register(services, service.ServiceType);
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
            return services;
        }
    }
 }
