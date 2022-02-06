using Microsoft.Extensions.DependencyInjection;
using System;

namespace VDT.Core.DependencyInjection {
    internal class ServiceContext {
        internal Type ServiceType { get; }
        internal Type ImplementationType { get; }
        internal ServiceLifetime ServiceLifetime { get; }

        internal ServiceContext(Type serviceType, Type implementationType, ServiceLifetime serviceLifetime) {
            ServiceType = serviceType;
            ImplementationType = implementationType;
            ServiceLifetime = serviceLifetime;
        }
    }
 }
