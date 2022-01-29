using Microsoft.Extensions.DependencyInjection;
using System;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Default service lifetime providers
    /// </summary>
    public static class DefaultServiceLifetimeProviders {
        /// <summary>
        /// Service lifetime provider that always returns <see cref="ServiceLifetime.Transient"/>
        /// </summary>
        /// <param name="serviceType">The service type</param>
        /// <param name="implementationType">The implementation type</param>
        /// <returns><see cref="ServiceLifetime.Transient"/></returns>
        public static ServiceLifetime Transient(Type serviceType, Type implementationType) => ServiceLifetime.Transient;

        /// <summary>
        /// Service lifetime provider that always returns <see cref="ServiceLifetime.Scoped"/>
        /// </summary>
        /// <param name="serviceType">The service type</param>
        /// <param name="implementationType">The implementation type</param>
        /// <returns><see cref="ServiceLifetime.Scoped"/></returns>
        public static ServiceLifetime Scoped(Type serviceType, Type implementationType) => ServiceLifetime.Scoped;

        /// <summary>
        /// Service lifetime provider that always returns <see cref="ServiceLifetime.Singleton"/>
        /// </summary>
        /// <param name="serviceType">The service type</param>
        /// <param name="implementationType">The implementation type</param>
        /// <returns><see cref="ServiceLifetime.Singleton"/></returns>
        public static ServiceLifetime Singleton(Type serviceType, Type implementationType) => ServiceLifetime.Singleton;
    }
}
