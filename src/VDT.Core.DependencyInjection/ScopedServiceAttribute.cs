﻿using System;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Marks a service to be registered as a scoped service when calling <see cref="ServiceCollectionExtensions.AddAttributeServices"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public sealed class ScopedServiceAttribute : Attribute {
        public Type ImplementationType { get; }

        /// <summary>
        /// Marks a service to be registered as a scoped service when calling <see cref="ServiceCollectionExtensions.AddAttributeServices"/>
        /// </summary>
        /// <param name="implementationType">The type to use as implementation for this service</param>
        /// <remarks>When using decorators, the type specified in <paramref name="implementationType"/> must differ from the service type</remarks>
        public ScopedServiceAttribute(Type implementationType) {
            ImplementationType = implementationType;
        }
    }
}
