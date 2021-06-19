using System.Threading.Tasks;

namespace VDT.Core.Events {
    /// <summary>
    /// Provides a mechanism for asynchronously handling events of a certain type
    /// </summary>
    /// <typeparam name="TEvent">Type of the event to handle</typeparam>
    public interface IAsyncEventHandler<TEvent> {
        /// <summary>
        /// Triggers when an event occurs
        /// </summary>
        /// <param name="event">Event to handle</param>
        /// <returns>A <see cref="Task"/> that represents the operation of handling the event</returns>
        Task HandleAsync(TEvent @event);
    }
}
