using Microsoft.Extensions.DependencyInjection;
using System;

namespace VDT.Core.DependencyInjection.Decorators {
    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddScoped<TService, TImplementation, TMethodDecorator>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
            where TMethodDecorator : IDecorator {

            return services.AddScoped<TService, TImplementation>();
        }

        public static IServiceCollection AddScoped<TService, TImplementation, TMethodDecorator>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
            where TMethodDecorator : IDecorator {

            return services.AddScoped<TService, TImplementation>(implementationFactory);
        }
    }
}
