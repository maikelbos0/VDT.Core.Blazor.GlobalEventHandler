using System.Threading;
using System.Threading.Tasks;

namespace VDT.Core.Events {
    /// <summary>
    /// Specifies the contract for scheduled event services
    /// </summary>
    public interface IScheduledEventService {
        /// <summary>
        /// Add an <see cref="IScheduledEvent"/> to be dispatched by the service
        /// </summary>
        /// <param name="scheduledEvent">Event that should be dispatched on a schedule</param>
        void AddScheduledEvent(IScheduledEvent scheduledEvent);

        /// <summary>
        /// Start the operation of dispatching the scheduled events
        /// </summary>
        /// <param name="stoppingToken">Triggered when dispatching should stop</param>
        /// <returns>A <see cref="Task"/> that represents the operation of dispatching the scheduled events</returns>
        Task ExecuteAsync(CancellationToken stoppingToken);
    }
}
