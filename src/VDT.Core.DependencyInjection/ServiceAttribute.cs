using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using VDT.Core.DependencyInjection.Decorators;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Marks a service to be registered when calling <see cref="ServiceCollectionExtensions.AddAttributeServices(IServiceCollection, Assembly)"/>
    /// or <see cref="ServiceCollectionExtensions.AddAttributeServices(IServiceCollection, Assembly, Action{DecoratorOptions})"/>
    /// </summary>
    public abstract class ServiceAttribute : Attribute {
        internal abstract void Register(IServiceCollection services, Type type);

        internal abstract void Register(IServiceCollection services, Type type, Action<DecoratorOptions> decoratorSetupAction);
    }
}
