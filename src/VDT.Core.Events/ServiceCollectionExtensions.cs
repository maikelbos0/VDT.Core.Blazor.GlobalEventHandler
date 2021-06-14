using Microsoft.Extensions.DependencyInjection;

namespace VDT.Core.Events {
    /// <summary>
    /// Extension methods for adding event services to an <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtensions {
        /// <summary>
        /// Add event services to an <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public static IServiceCollection AddEventService(this IServiceCollection services) {
            return services.AddSingleton<IEventService, EventService>(serviceProvider => new EventService(serviceProvider));
        }
    }
}
