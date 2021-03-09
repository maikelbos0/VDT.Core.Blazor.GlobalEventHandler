using System;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Marks a service to be registered when calling <see cref="ServiceCollectionExtensions.AddAttributeServices"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public abstract class ServiceAttribute : Attribute {
        /// <summary>
        /// The type to use as implementation for this service
        /// </summary>
        public Type ImplementationType { get; }

        internal ServiceAttribute(Type implementationType) {
            ImplementationType = implementationType;
        }
    }
}
