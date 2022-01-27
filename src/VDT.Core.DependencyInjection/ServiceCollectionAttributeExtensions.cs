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
    public static class ServiceCollectionAttributeExtensions {
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
    }
 }
