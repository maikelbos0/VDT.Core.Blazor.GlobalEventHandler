using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Marks a service to be registered as a singleton service when calling <see cref="ServiceCollectionAttributeExtensions.AddAttributeServices(IServiceCollection, Assembly)"/>
    /// or <see cref="ServiceCollectionAttributeExtensions.AddAttributeServices(IServiceCollection, Assembly, Action{Decorators.DecoratorOptions})"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class SingletonServiceAttribute : ServiceAttribute, IServiceAttribute {
        private static readonly MethodInfo addDecoratedServiceMethod = typeof(Decorators.ServiceCollectionDecoratorExtensions)
            .GetMethod(nameof(Decorators.ServiceCollectionDecoratorExtensions.AddSingleton), 2, BindingFlags.Public | BindingFlags.Static, typeof(IServiceCollection), typeof(Action<Decorators.DecoratorOptions>));

        /// <summary>
        /// The type to use as implementation for this service
        /// </summary>
        public Type ImplementationType { get; }

        /// <summary>
        /// The lifetime of services marked with this attribute is <see cref="ServiceLifetime.Singleton"/>
        /// </summary>
        public ServiceLifetime ServiceLifetime => ServiceLifetime.Singleton;

        /// <summary>
        /// Marks a service to be registered as a singleton service when calling <see cref="ServiceCollectionAttributeExtensions.AddAttributeServices(IServiceCollection, Assembly)"/>
        /// or <see cref="ServiceCollectionAttributeExtensions.AddAttributeServices(IServiceCollection, Assembly, Action{Decorators.DecoratorOptions})"/>
        /// </summary>
        /// <param name="implementationType">The type to use as implementation for this service</param>
        /// <remarks>When using decorators, the type specified in <paramref name="implementationType"/> must differ from the service type</remarks>
        public SingletonServiceAttribute(Type implementationType) {
            ImplementationType = implementationType;
        }

        internal override void Register(IServiceCollection services, Type type) {
            services.AddSingleton(type, ImplementationType);
        }

        internal override void Register(IServiceCollection services, Type type, Action<Decorators.DecoratorOptions> decoratorSetupAction) {
            addDecoratedServiceMethod.MakeGenericMethod(type, ImplementationType).Invoke(null, new object[] { services, decoratorSetupAction });
        }
    }
}
