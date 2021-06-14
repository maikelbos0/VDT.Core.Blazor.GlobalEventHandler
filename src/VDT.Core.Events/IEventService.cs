using System;

namespace VDT.Core.Events {
    /// <summary>
    /// Specifies the contract for event services
    /// </summary>
    public interface IEventService {
        /// <summary>
        /// Register an event handler
        /// </summary>
        /// <typeparam name="TEvent">Type of the event to handle</typeparam>
        /// <param name="handler">Handler that handles the event</param>
        /// <remarks>Multiple event handlers can be registered for the same event type</remarks>
        void RegisterHandler<TEvent>(IEventHandler<TEvent> handler);

        /// <summary>
        /// Register an action as an event handler
        /// </summary>
        /// <typeparam name="TEvent">Type of the event to handle</typeparam>
        /// <param name="action">Handler action that handles the event</param>
        /// <remarks>Multiple event handlers can be registered for the same event type</remarks>
        void RegisterHandler<TEvent>(Action<TEvent> action);

        /// <summary>
        /// Dispatch an event and trigger all registered event handlers for that event
        /// </summary>
        /// <typeparam name="TEvent">Type of the event to handle</typeparam>
        /// <param name="event">Event to handle</param>
        void Dispatch<TEvent>(TEvent @event);
    }
}
