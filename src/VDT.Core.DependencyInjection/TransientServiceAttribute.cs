using System;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Marks a service to be registered as a transient service when calling <see cref="ServiceCollectionExtensions.AddAttributeServices"/>
    /// </summary>
    public sealed class TransientServiceAttribute : ServiceAttribute {
        /// <summary>
        /// Marks a service to be registered as a transient service when calling <see cref="ServiceCollectionExtensions.AddAttributeServices"/>
        /// </summary>
        /// <param name="implementationType">The type to use as implementation for this service</param>
        /// <remarks>When using decorators, the type specified in <paramref name="implementationType"/> must differ from the service type</remarks>
        public TransientServiceAttribute(Type implementationType) : base(implementationType) { }
    }
}
