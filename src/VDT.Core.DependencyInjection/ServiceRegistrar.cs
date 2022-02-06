using Microsoft.Extensions.DependencyInjection;
using System;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Signature for methods that register services to an <see cref="IServiceCollection"/>
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to</param>
    /// <param name="serviceType">The service type</param>
    /// <param name="implementationType">The implementation type</param>
    /// <param name="serviceLifetime">The lifetime of this service</param>
    public delegate void ServiceRegistrar(IServiceCollection services, Type serviceType, Type implementationType, ServiceLifetime serviceLifetime);
}
