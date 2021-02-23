using Microsoft.Extensions.DependencyInjection;
using System;

namespace VDT.Core.DependencyInjection.Decorators {
    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services, Action<DecoratorOptions<TService>> setupAction)
            where TService : class
            where TImplementation : class, TService {
                        
            return services
                .AddScoped<TImplementation, TImplementation>()
                .AddScoped<TService, TImplementation>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        }

        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions<TService>> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services
                .AddScoped<TImplementation, TImplementation>(implementationFactory)
                .AddScoped<TService, TImplementation>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        }
    }
}
