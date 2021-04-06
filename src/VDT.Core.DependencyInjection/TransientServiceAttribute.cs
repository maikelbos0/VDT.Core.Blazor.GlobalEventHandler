using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Marks a service to be registered as a transient service when calling <see cref="ServiceCollectionExtensions.AddAttributeServices(IServiceCollection, Assembly)"/>
    /// or <see cref="ServiceCollectionExtensions.AddAttributeServices(IServiceCollection, Assembly, Action{Decorators.DecoratorOptions})"/>
    /// </summary>
    public sealed class TransientServiceAttribute : ServiceAttribute {
        private static readonly MethodInfo addServiceMethod = typeof(ServiceCollectionServiceExtensions)
            .GetMethod(nameof(ServiceCollectionServiceExtensions.AddTransient), 2, BindingFlags.Public | BindingFlags.Static, typeof(IServiceCollection));
        
        private static readonly MethodInfo addDecoratedServiceMethod = typeof(Decorators.ServiceCollectionExtensions)
            .GetMethod(nameof(Decorators.ServiceCollectionExtensions.AddTransient), 2, BindingFlags.Public | BindingFlags.Static, typeof(IServiceCollection), typeof(Action<Decorators.DecoratorOptions>));

        /// <summary>
        /// The type to use as implementation for this service
        /// </summary>
        public Type ImplementationType { get; }

        /// <summary>
        /// Marks a service to be registered as a transient service when calling <see cref="ServiceCollectionExtensions.AddAttributeServices(IServiceCollection, Assembly)"/>
        /// or <see cref="ServiceCollectionExtensions.AddAttributeServices(IServiceCollection, Assembly, Action{Decorators.DecoratorOptions})"/>
        /// </summary>
        /// <param name="implementationType">The type to use as implementation for this service</param>
        /// <remarks>When using decorators, the type specified in <paramref name="implementationType"/> must differ from the service type</remarks>
        public TransientServiceAttribute(Type implementationType) {
            ImplementationType = implementationType;
        }

        internal override void Register(IServiceCollection services, Type type) {
            addServiceMethod.MakeGenericMethod(type, ImplementationType).Invoke(null, new object[] { services });
        }

        internal override void Register(IServiceCollection services, Type type, Action<Decorators.DecoratorOptions> decoratorSetupAction) {
            addDecoratedServiceMethod.MakeGenericMethod(type, ImplementationType).Invoke(null, new object[] { services, decoratorSetupAction });
        }
    }
}
