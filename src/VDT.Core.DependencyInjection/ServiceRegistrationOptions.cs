using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Options for registering services to a <see cref="IServiceCollection"/>
    /// </summary>
    public class ServiceRegistrationOptions {
        /// <summary>
        /// Assemblies to scan for services
        /// </summary>
        public List<Assembly> Assemblies { get; set; } = new List<Assembly>();

        /// <summary>
        /// Options for methods that return service types for a given implementation type; service types that appear in any method will be registered
        /// </summary>
        public List<ServiceTypeProviderOptions> ServiceTypeProviders { get; set; } = new List<ServiceTypeProviderOptions>();

        /// <summary>
        /// Service lifetime to use if no <see cref="ServiceLifetimeProvider"/> is provided or the <see cref="ServiceLifetimeProvider"/> did not find a suitable lifetime
        /// </summary>
        public ServiceLifetime DefaultServiceLifetime { get; set; } = ServiceLifetime.Scoped;

        /// <summary>
        /// Method that register the found services to an <see cref="IServiceCollection"/> with the provided lifetime; if no method is provided the default implementation will create a <see cref="ServiceDescriptor"/>
        /// </summary>
        public ServiceRegistrar? ServiceRegistrar { get; set; }

        /// <summary>
        /// Add an assembly to scan for services
        /// </summary>
        /// <param name="assembly">The assembly to scan for services</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public ServiceRegistrationOptions AddAssembly(Assembly assembly) {
            Assemblies.Add(assembly);

            return this;
        }

        /// <summary>
        /// Add assemblies to scan for services
        /// </summary>
        /// <param name="assemblies">The assemblies to scan for services</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public ServiceRegistrationOptions AddAssemblies(IEnumerable<Assembly> assemblies) {
            Assemblies.AddRange(assemblies);

            return this;
        }

        /// <summary>
        /// Add assemblies to scan for services
        /// </summary>
        /// <param name="assemblies">The assemblies to scan for services</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public ServiceRegistrationOptions AddAssemblies(params Assembly[] assemblies) {
            Assemblies.AddRange(assemblies);

            return this;
        }

        /// <summary>
        /// Add a method that return service types to be registered for a given implementation type
        /// </summary>
        /// <param name="serviceTypeProvider">Method that returns service types for a given implementation type</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>When using decorators, the service types must differ from the implementation type</remarks>
        public ServiceRegistrationOptions AddServiceTypeProvider(ServiceTypeProvider serviceTypeProvider) {
            ServiceTypeProviders.Add(new ServiceTypeProviderOptions(serviceTypeProvider));

            return this;
        }

        /// <summary>
        /// Add a method that return service types to be registered for a given implementation type with a given lifetime provider
        /// </summary>
        /// <param name="serviceTypeProvider">Method that returns service types for a given implementation type</param>
        /// <param name="serviceLifetimeProvider">Method that returns a service lifetime for a given service and implementation type to be registered</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>When using decorators, the service types must differ from the implementation type</remarks>
        public ServiceRegistrationOptions AddServiceTypeProvider(ServiceTypeProvider serviceTypeProvider, ServiceLifetimeProvider serviceLifetimeProvider) {
            ServiceTypeProviders.Add(new ServiceTypeProviderOptions(serviceTypeProvider) {
                ServiceLifetimeProvider = serviceLifetimeProvider
            });

            return this;
        }

        /// <summary>
        /// Set the service lifetime that will be used if no service lifetime provider is supplied or the supplied service lifetime provider could not determine the service lifetime
        /// </summary>
        /// <param name="serviceLifetime">Specifies the default lifetime of services that will be registered</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public ServiceRegistrationOptions UseDefaultServiceLifetime(ServiceLifetime serviceLifetime) {
            DefaultServiceLifetime = serviceLifetime;

            return this;
        }

        /// <summary>
        /// Set the method that will register found services to an <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="serviceRegistrar">Method that registers services</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public ServiceRegistrationOptions UseServiceRegistrar(ServiceRegistrar serviceRegistrar) {
            ServiceRegistrar = serviceRegistrar;

            return this;
        }
    }
}
