using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Options for registering services to a <see cref="IServiceCollection"/>
    /// </summary>
    public class ServiceRegistrationOptions {
        /// <summary>
        /// Methods that return service types for a given implementation type; service types that appear in any method will be registered
        /// </summary>
        public List<ServiceTypeFinder> ServiceTypeFinders { get; set; } = new List<ServiceTypeFinder>();

        /// <summary>
        /// Methods that returns a service lifetime for a given service and implementation type to be registered
        /// </summary>
        public ServiceLifetimeProvider? ServiceLifetimeProvider { get; set; }

        /// <summary>
        /// Method that register the found services to an <see cref="IServiceCollection"/> with the provided lifetime
        /// </summary>
        public ServiceRegistrar? ServiceRegistrar { get; set; }
    }
}
