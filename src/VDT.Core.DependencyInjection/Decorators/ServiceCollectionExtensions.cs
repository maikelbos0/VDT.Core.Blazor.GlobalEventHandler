using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace VDT.Core.DependencyInjection.Decorators {
    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services, Action<DecoratorOptions<TService>> setupAction)
            where TService : class
            where TImplementation : class, TService {

            var options = new DecoratorOptions<TService>();

            setupAction(options);

            return services
                .AddScoped<TService, TImplementation>()
                .AddScoped<TService, TService>(GetDecoratedProxyFactory<TService, TImplementation>(options));
        }

        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions<TService>> setupAction)
            where TService : class
            where TImplementation : class, TService {

            var options = new DecoratorOptions<TService>();

            setupAction(options);

            return services
                .AddScoped<TService, TImplementation>(implementationFactory)
                .AddScoped<TService, TService>(GetDecoratedProxyFactory<TService, TImplementation>(options));
        }

        private static Func<IServiceProvider, TService> GetDecoratedProxyFactory<TService, TImplementation>(DecoratorOptions<TService> options)
            where TService : class
            where TImplementation : class, TService {

            var generator = new Castle.DynamicProxy.ProxyGenerator();
            var isInterface = typeof(TService).IsInterface;

            return serviceProvider => {
                var target = serviceProvider.GetServices<TService>().Single(s => s is TImplementation);
                var decorators = options.Policies.Select(p => new DecoratorInterceptor(p.GetDecorator(serviceProvider), p.Predicate)).ToArray();

                if (isInterface) {
                    return generator.CreateInterfaceProxyWithTarget(target, decorators);
                }
                else {
                    return generator.CreateClassProxyWithTarget(target, decorators);
                }
            };
        }
    }
}
