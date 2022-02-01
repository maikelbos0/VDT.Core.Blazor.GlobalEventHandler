namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Options for a method that returns service types for a given implementation type
    /// </summary>
    public class ServiceTypeFinderOptions {
        /// <summary>
        /// Create options for a method that returns service types for a given implementation type
        /// </summary>
        /// <param name="serviceTypeFinder">Method that returns service types for a given implementation type</param>
        public ServiceTypeFinderOptions(ServiceTypeFinder serviceTypeFinder) {
            ServiceTypeFinder = serviceTypeFinder;
        }

        /// <summary>
        /// Method that returns service types for a given implementation type
        /// </summary>
        public ServiceTypeFinder ServiceTypeFinder { get; set; }

        /// <summary>
        /// Methods that returns a service lifetime for a given service and implementation type to be registered
        /// </summary>
        public ServiceLifetimeProvider? ServiceLifetimeProvider { get; set; }
    }
}
