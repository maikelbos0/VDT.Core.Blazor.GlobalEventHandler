using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using VDT.Core.DependencyInjection.Decorators;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Marks a service to be registered when calling <see cref="ServiceCollectionExtensions.AddAttributeServices(IServiceCollection, Assembly)"/>
    /// or <see cref="ServiceCollectionExtensions.AddAttributeServices(IServiceCollection, Assembly, Action{DecoratorOptions})"/>
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

        internal abstract void Register(IServiceCollection services, Type serviceType);

        internal abstract void Register(IServiceCollection services, Type serviceType, Action<DecoratorOptions> decoratorSetupAction);
    }
}
