using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace VDT.Core.DependencyInjection.Decorators {
    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services, Action<DecoratorOptions<TService>> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services.AddScoped<TService, TImplementation, TImplementation>(setupAction);
        }

        public static IServiceCollection AddScoped<TService, TImplementationService, TImplementation>(this IServiceCollection services, Action<DecoratorOptions<TService>> setupAction)
            where TService : class
            where TImplementationService : class, TService
            where TImplementation : class, TImplementationService {

            return services
                .AddProxy<TService, TImplementationService>(proxyFactory => services.AddScoped(proxyFactory), setupAction)
                .AddScoped<TImplementationService, TImplementation>();
        }

        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions<TService>> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services.AddScoped<TService, TImplementation, TImplementation>(implementationFactory, setupAction);
        }

        public static IServiceCollection AddScoped<TService, TImplementationService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions<TService>> setupAction)
            where TService : class
            where TImplementationService : class, TService
            where TImplementation : class, TImplementationService {

            return services
                .AddProxy<TService, TImplementationService>(proxyFactory => services.AddScoped(proxyFactory), setupAction)
                .AddScoped<TImplementationService, TImplementation>(implementationFactory);
        }

        private static IServiceCollection AddProxy<TService, TImplementationService>(this IServiceCollection services, Func<Func<IServiceProvider, TService>, IServiceCollection> registerProxy, Action<DecoratorOptions<TService>> setupAction)
            where TService : class
            where TImplementationService : class, TService {

            VerifyRegistration<TService, TImplementationService>();

            var options = GetDecoratorOptions(setupAction);
            var proxyFactory = GetDecoratedProxyFactory<TService, TImplementationService>(options);

            return registerProxy(proxyFactory);
        }

        private static void VerifyRegistration<TService, TImplementationService>()
            where TService : class
            where TImplementationService : class, TService {

            if (typeof(TService) == typeof(TImplementationService)) {
                throw new ServiceRegistrationException($"Implementation service type '{typeof(TImplementationService).FullName}' can not be equal to service type '{typeof(TService).FullName}'.");
            }
        }

        private static DecoratorOptions<TService> GetDecoratorOptions<TService>(Action<DecoratorOptions<TService>> setupAction)
            where TService : class {

            var options = new DecoratorOptions<TService>();
            setupAction(options);

            return options;
        }

        private static Func<IServiceProvider, TService> GetDecoratedProxyFactory<TService, TImplementationService>(DecoratorOptions<TService> options)
            where TService : class
            where TImplementationService : class, TService {

            var generator = new Castle.DynamicProxy.ProxyGenerator();
            var isInterface = typeof(TService).IsInterface;

            return serviceProvider => {
                var target = serviceProvider.GetRequiredService<TImplementationService>();
                var decorators = options.Policies.Select(p => new DecoratorInterceptor(p.GetDecorator(serviceProvider), p.Predicate)).ToArray();

                if (isInterface) {
                    return generator.CreateInterfaceProxyWithTarget<TService>(target, decorators);
                }
                else {
                    return generator.CreateClassProxyWithTarget<TService>(target, decorators);
                }
            };
        }
    }
}
