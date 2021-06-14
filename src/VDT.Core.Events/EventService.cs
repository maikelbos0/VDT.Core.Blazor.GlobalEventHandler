using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VDT.Core.Events {
    /// <summary>
    /// Service for registering and dispatching events
    /// </summary>
    public sealed class EventService : IEventService {
        private readonly Dictionary<Type, List<object>> eventHandlers = new Dictionary<Type, List<object>>();
        private readonly IServiceProvider? serviceProvider;

        /// <summary>
        /// Create an event service to manually add event handlers to
        /// </summary>
        public EventService() { }

        /// <summary>
        /// Create an event service that supports event handlers that are both manually added and resolved via the service provider
        /// </summary>
        /// <param name="serviceProvider">Service provider to resolve event handlers from</param>
        public EventService(IServiceProvider serviceProvider) {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Register an event handler
        /// </summary>
        /// <typeparam name="TEvent">Type of the event to handle</typeparam>
        /// <param name="handler">Handler that handles the event</param>
        /// <remarks>Multiple event handlers can be registered for the same event type</remarks>
        public void RegisterHandler<TEvent>(IEventHandler<TEvent> handler) {
            if (!eventHandlers.TryGetValue(typeof(TEvent), out var handlers)) {
                handlers = new List<object>();
                eventHandlers.Add(typeof(TEvent), handlers);
            }

            handlers.Add(handler);
        }

        /// <summary>
        /// Register an action as an event handler
        /// </summary>
        /// <typeparam name="TEvent">Type of the event to handle</typeparam>
        /// <param name="action">Handler action that handles the event</param>
        /// <remarks>Multiple event handlers can be registered for the same event type</remarks>
        public void RegisterHandler<TEvent>(Action<TEvent> action) {
            RegisterHandler(new ActionEventHandler<TEvent>(action));
        }

        /// <summary>
        /// Dispatch an event and trigger all registered event handlers for that event
        /// </summary>
        /// <typeparam name="TEvent">Type of the event to handle</typeparam>
        /// <param name="event">Event to handle</param>
        public void Dispatch<TEvent>(TEvent @event) {
            if (eventHandlers.TryGetValue(typeof(TEvent), out var handlers)) {
                foreach (var handler in handlers.Cast<IEventHandler<TEvent>>()) {
                    handler.Handle(@event);
                }
            }

            if (serviceProvider != null) {
                foreach (var handler in serviceProvider.GetServices<IEventHandler<TEvent>>()) {
                    handler.Handle(@event);
                }
            }
        }
    }
}
