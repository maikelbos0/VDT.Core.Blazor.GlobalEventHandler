using System;
using System.Collections.Generic;
using System.Linq;

namespace VDT.Core.DependencyInjection.Attributes {
    /// <summary>
    /// Extension methods for adding service type providers for services marked with attributes to services to be registered using <see cref="ServiceRegistrationOptions"/>
    /// </summary>
    public static class ServiceRegistrationOptionsExtensions {
        /// <summary>
        /// Add service type providers and service lifetime providers that find and use <see cref="TransientServiceAttribute"/>, <see cref="ScopedServiceAttribute"/>, <see cref="SingletonServiceAttribute"/>,
        /// <see cref="TransientServiceImplementationAttribute"/>, <see cref="ScopedServiceImplementationAttribute"/> or <see cref="SingletonServiceImplementationAttribute"/> to register services
        /// </summary>
        /// <param name="options">The options for registering services</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public static ServiceRegistrationOptions AddAttributeServiceTypeProviders(this ServiceRegistrationOptions options) {
            // Attributes on implementation types
            options.AddServiceTypeProvider(
                implementationType => implementationType.GetCustomAttributes(typeof(IServiceImplementationAttribute), false).Cast<IServiceImplementationAttribute>().Select(a => a.ServiceType),
                (serviceType, implementationType) => implementationType.GetCustomAttributes(typeof(IServiceImplementationAttribute), false).Cast<IServiceImplementationAttribute>().FirstOrDefault(a => a.ServiceType == serviceType)?.ServiceLifetime
            );

            // Attributes on service interface types
            options.AddServiceTypeProvider(
                implementationType => implementationType.GetInterfaces().Where(serviceType => serviceType.GetCustomAttributes(typeof(IServiceAttribute), false).Any()),
                (serviceType, implementationType) => serviceType.GetCustomAttributes(typeof(IServiceAttribute), false).Cast<IServiceAttribute>().FirstOrDefault(a => a.ImplementationType == implementationType)?.ServiceLifetime
            );

            // Attributes on service class types
            options.AddServiceTypeProvider(
                implementationType => {
                    var currentServiceType = implementationType.BaseType;
                    var serviceTypes = new List<Type>();

                    while (currentServiceType != null) {
                        serviceTypes.Add(currentServiceType);
                        currentServiceType = currentServiceType.BaseType;
                    }

                    return serviceTypes.Where(serviceType => serviceType.GetCustomAttributes(typeof(IServiceAttribute), false).Any());
                },
                (serviceType, implementationType) => serviceType.GetCustomAttributes(typeof(IServiceAttribute), false).Cast<IServiceAttribute>().FirstOrDefault(a => a.ImplementationType == implementationType)?.ServiceLifetime
            );

            return options;
        }

    }
}
