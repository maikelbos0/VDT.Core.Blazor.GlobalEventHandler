using Microsoft.Extensions.DependencyInjection;
using System;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Signature for methods that return a service lifetime for a given service and implementation type
    /// </summary>
    /// <param name="serviceType">The service type</param>
    /// <param name="implementationType">The implementation type</param>
    /// <returns>A service lifetime to apply to this service registration</returns>
    public delegate ServiceLifetime ServiceLifetimeProvider(Type serviceType, Type implementationType);
}
