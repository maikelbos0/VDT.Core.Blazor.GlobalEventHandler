using System;

namespace VDT.Core.DependencyInjection {
    internal class ServiceContext {
        internal Type ServiceType { get; }
        internal Type ImplementationType { get; }

        internal ServiceContext(Type serviceType, Type implementationType) {
            ServiceType = serviceType;
            ImplementationType = implementationType;
        }
    }
 }
