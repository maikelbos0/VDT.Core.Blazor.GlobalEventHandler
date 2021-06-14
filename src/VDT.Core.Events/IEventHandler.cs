namespace VDT.Core.Events {
    /// <summary>
    /// Provides a mechanism for handling events of a certain type
    /// </summary>
    /// <typeparam name="TEvent">Type of the event to handle</typeparam>
    public interface IEventHandler<TEvent> {
        /// <summary>
        /// Triggers when an event occurs
        /// </summary>
        /// <param name="event">Event to handle</param>
        void Handle(TEvent @event);
    }
}
