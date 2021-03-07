using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace VDT.Core.DependencyInjection.Decorators {
    /// <summary>
    /// Extension methods for adding decorated services to an <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtensions {
        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        public static IServiceCollection AddTransient<TService, TImplementation>(this IServiceCollection services, Action<DecoratorOptions<TService>> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services.AddTransient<TService, TImplementation, TImplementation>(setupAction);
        }

        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementationService">The type with which the implementation will be registered and resolved</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementationService"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        public static IServiceCollection AddTransient<TService, TImplementationService, TImplementation>(this IServiceCollection services, Action<DecoratorOptions<TService>> setupAction)
            where TService : class
            where TImplementationService : class, TService
            where TImplementation : class, TImplementationService {

            return services
                .AddProxy<TService, TImplementationService>((services, proxyFactory) => services.AddTransient(proxyFactory), setupAction)
                .AddTransient<TImplementationService, TImplementation>();
        }

        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// using the provided factory
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="implementationFactory">The factory that creates the service</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        public static IServiceCollection AddTransient<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions<TService>> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services.AddTransient<TService, TImplementation, TImplementation>(implementationFactory, setupAction);
        }

        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// using the provided factory
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementationService">The type with which the implementation will be registered and resolved</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="implementationFactory">The factory that creates the service</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementationService"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        public static IServiceCollection AddTransient<TService, TImplementationService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions<TService>> setupAction)
            where TService : class
            where TImplementationService : class, TService
            where TImplementation : class, TImplementationService {

            return services
                .AddProxy<TService, TImplementationService>((services, proxyFactory) => services.AddTransient(proxyFactory), setupAction)
                .AddTransient<TImplementationService, TImplementation>(implementationFactory);
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services, Action<DecoratorOptions<TService>> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services.AddScoped<TService, TImplementation, TImplementation>(setupAction);
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementationService">The type with which the implementation will be registered and resolved</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementationService"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        public static IServiceCollection AddScoped<TService, TImplementationService, TImplementation>(this IServiceCollection services, Action<DecoratorOptions<TService>> setupAction)
            where TService : class
            where TImplementationService : class, TService
            where TImplementation : class, TImplementationService {

            return services
                .AddProxy<TService, TImplementationService>((services, proxyFactory) => services.AddScoped(proxyFactory), setupAction)
                .AddScoped<TImplementationService, TImplementation>();
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// using the provided factory
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="implementationFactory">The factory that creates the service</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions<TService>> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services.AddScoped<TService, TImplementation, TImplementation>(implementationFactory, setupAction);
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// using the provided factory
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementationService">The type with which the implementation will be registered and resolved</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="implementationFactory">The factory that creates the service</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementationService"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        public static IServiceCollection AddScoped<TService, TImplementationService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions<TService>> setupAction)
            where TService : class
            where TImplementationService : class, TService
            where TImplementation : class, TImplementationService {

            return services
                .AddProxy<TService, TImplementationService>((services, proxyFactory) => services.AddScoped(proxyFactory), setupAction)
                .AddScoped<TImplementationService, TImplementation>(implementationFactory);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services, Action<DecoratorOptions<TService>> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services.AddSingleton<TService, TImplementation, TImplementation>(setupAction);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementationService">The type with which the implementation will be registered and resolved</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementationService"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        public static IServiceCollection AddSingleton<TService, TImplementationService, TImplementation>(this IServiceCollection services, Action<DecoratorOptions<TService>> setupAction)
            where TService : class
            where TImplementationService : class, TService
            where TImplementation : class, TImplementationService {

            return services
                .AddProxy<TService, TImplementationService>((services, proxyFactory) => services.AddSingleton(proxyFactory), setupAction)
                .AddSingleton<TImplementationService, TImplementation>();
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// using the provided factory
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="implementationFactory">The factory that creates the service</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions<TService>> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services.AddSingleton<TService, TImplementation, TImplementation>(implementationFactory, setupAction);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// using the provided factory
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementationService">The type with which the implementation will be registered and resolved</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="implementationFactory">The factory that creates the service</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementationService"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        public static IServiceCollection AddSingleton<TService, TImplementationService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions<TService>> setupAction)
            where TService : class
            where TImplementationService : class, TService
            where TImplementation : class, TImplementationService {

            return services
                .AddProxy<TService, TImplementationService>((services, proxyFactory) => services.AddSingleton(proxyFactory), setupAction)
                .AddSingleton<TImplementationService, TImplementation>(implementationFactory);
        }

        private static IServiceCollection AddProxy<TService, TImplementationService>(this IServiceCollection services, Func<IServiceCollection, Func<IServiceProvider, TService>, IServiceCollection> registerProxy, Action<DecoratorOptions<TService>> setupAction)
            where TService : class
            where TImplementationService : class, TService {

            VerifyRegistration<TService, TImplementationService>();

            var options = GetDecoratorOptions(setupAction);
            var proxyFactory = GetDecoratedProxyFactory<TService, TImplementationService>(options);

            return registerProxy(services, proxyFactory);
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
