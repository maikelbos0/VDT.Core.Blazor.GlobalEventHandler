using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace VDT.Core.Events {
    /// <summary>
    /// Extension methods for adding event services to an <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtensions {
        /// <summary>
        /// Add an <see cref="IEventService"/> to an <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public static IServiceCollection AddEventService(this IServiceCollection services) {
            return services.AddSingleton<IEventService, EventService>(serviceProvider => new EventService(serviceProvider));
        }

        /// <summary>
        /// Add an <see cref="IScheduledEventService"/> and a <see cref="BackgroundService"/> to execute it to an <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>Registration of an <see cref="IEventService"/> implementation is required for this hosted service</remarks>
        public static IServiceCollection AddScheduledEventService(this IServiceCollection services) {
            return services
                .AddSingleton<IDateTimeService, DateTimeService>()
                .AddSingleton<IScheduledEventService, ScheduledEventService>()
                .AddSingleton<ITaskService, TaskService>()
                .AddHostedService<ScheduledEventBackgroundService>();
        }
    }
}
