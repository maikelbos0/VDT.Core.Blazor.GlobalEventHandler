using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace VDT.Core.DependencyInjection.Attributes {
    /// <summary>
    /// Marks a service implementation to be registered as a transient service when calling <see cref="ServiceCollectionConventionExtensions.AddServices(IServiceCollection, Action{ServiceRegistrationOptions})"/>
    /// with <see cref="ServiceRegistrationOptionsAttributeExtensions.AddAttributeServiceTypeFinders(ServiceRegistrationOptions)"/> called on the <see cref="ServiceRegistrationOptions"/> builder action
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class TransientServiceImplementationAttribute : Attribute, IServiceImplementationAttribute {
        private static readonly MethodInfo addDecoratedServiceMethod = typeof(Decorators.ServiceCollectionDecoratorExtensions)
            .GetMethod(nameof(Decorators.ServiceCollectionDecoratorExtensions.AddTransient), 2, BindingFlags.Public | BindingFlags.Static, typeof(IServiceCollection), typeof(Action<Decorators.DecoratorOptions>));

        /// <summary>
        /// The type to use as service for this implementation
        /// </summary>
        public Type ServiceType { get; }

        /// <summary>
        /// The lifetime of services marked with this attribute is <see cref="ServiceLifetime.Transient"/>
        /// </summary>
        public ServiceLifetime ServiceLifetime => ServiceLifetime.Transient;

        /// <summary>
        /// Marks a service implementation to be registered as a transient service when calling <see cref="ServiceCollectionConventionExtensions.AddServices(IServiceCollection, Action{ServiceRegistrationOptions})"/>
        /// with <see cref="ServiceRegistrationOptionsAttributeExtensions.AddAttributeServiceTypeFinders(ServiceRegistrationOptions)"/> called on the <see cref="ServiceRegistrationOptions"/> builder action
        /// </summary>
        /// <param name="serviceType">The type to use as service for this implementation</param>
        /// <remarks>When using decorators, the type specified in <paramref name="serviceType"/> must differ from the implementation type</remarks>
        public TransientServiceImplementationAttribute(Type serviceType) {
            ServiceType = serviceType;
        }
    }
}
