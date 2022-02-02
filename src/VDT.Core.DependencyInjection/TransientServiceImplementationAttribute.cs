using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Marks a service implementation to be registered as a transient service when calling <see cref="ServiceCollectionAttributeExtensions.AddAttributeServices(IServiceCollection, Assembly)"/>
    /// or <see cref="ServiceCollectionAttributeExtensions.AddAttributeServices(IServiceCollection, Assembly, Action{Decorators.DecoratorOptions})"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class TransientServiceImplementationAttribute : ServiceAttribute, IServiceImplementationAttribute {
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
        /// Marks a service to be registered as a transient service when calling <see cref="ServiceCollectionAttributeExtensions.AddAttributeServices(IServiceCollection, Assembly)"/>
        /// or <see cref="ServiceCollectionAttributeExtensions.AddAttributeServices(IServiceCollection, Assembly, Action{Decorators.DecoratorOptions})"/>
        /// </summary>
        /// <param name="serviceType">The type to use as service for this implementation</param>
        /// <remarks>When using decorators, the type specified in <paramref name="serviceType"/> must differ from the implementation type</remarks>
        public TransientServiceImplementationAttribute(Type serviceType) {
            ServiceType = serviceType;
        }

        internal override void Register(IServiceCollection services, Type type) {
            services.AddTransient(ServiceType, type);
        }

        internal override void Register(IServiceCollection services, Type type, Action<Decorators.DecoratorOptions> decoratorSetupAction) {
            addDecoratedServiceMethod.MakeGenericMethod(ServiceType, type).Invoke(null, new object[] { services, decoratorSetupAction });
        }
    }
}
