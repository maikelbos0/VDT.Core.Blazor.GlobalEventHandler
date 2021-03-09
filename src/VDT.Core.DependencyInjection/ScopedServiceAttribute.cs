using System;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Marks a service to be registered as a scoped service when calling <see cref="ServiceCollectionExtensions.AddAttributeServices"/>
    /// </summary>
    public sealed class ScopedServiceAttribute : ServiceAttribute {
        /// <summary>
        /// Marks a service to be registered as a scoped service when calling <see cref="ServiceCollectionExtensions.AddAttributeServices"/>
        /// </summary>
        /// <param name="implementationType">The type to use as implementation for this service</param>
        /// <remarks>When using decorators, the type specified in <paramref name="implementationType"/> must differ from the service type</remarks>
        public ScopedServiceAttribute(Type implementationType) : base(implementationType) { }
    }
}
