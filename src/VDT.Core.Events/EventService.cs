using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace VDT.Core.Events {
    /// <summary>
    /// Service for registering and dispatching events
    /// </summary>
    public sealed class EventService : IEventService {
        private static readonly MethodInfo dispatchMethod = typeof(EventService).GetMethod(nameof(EventService.DispatchEvent)) ?? throw new InvalidOperationException($"Method '{nameof(EventService)}.{nameof(IEventService.DispatchEvent)}' was not found.");

        private readonly Dictionary<Type, List<object>> eventHandlers = new Dictionary<Type, List<object>>();
        private readonly Dictionary<Type, List<object>> asyncEventHandlers = new Dictionary<Type, List<object>>();
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
        public IEventService RegisterHandler<TEvent>(IEventHandler<TEvent> handler) {
            if (!eventHandlers.TryGetValue(typeof(TEvent), out var handlers)) {
                handlers = new List<object>();
                eventHandlers.Add(typeof(TEvent), handlers);
            }

            handlers.Add(handler);

            return this;
        }

        /// <summary>
        /// Register an event handler
        /// </summary>
        /// <typeparam name="TEvent">Type of the event to handle</typeparam>
        /// <param name="asyncHandler">Handler that handles the event</param>
        /// <remarks>Multiple event handlers can be registered for the same event type</remarks>
        public IEventService RegisterHandler<TEvent>(IAsyncEventHandler<TEvent> asyncHandler) {
            if (!asyncEventHandlers.TryGetValue(typeof(TEvent), out var asyncHandlers)) {
                asyncHandlers = new List<object>();
                asyncEventHandlers.Add(typeof(TEvent), asyncHandlers);
            }

            asyncHandlers.Add(asyncHandler);

            return this;
        }

        /// <summary>
        /// Register an action as an event handler
        /// </summary>
        /// <typeparam name="TEvent">Type of the event to handle</typeparam>
        /// <param name="action">Handler action that handles the event</param>
        /// <remarks>Multiple event handlers can be registered for the same event type</remarks>
        public IEventService RegisterHandler<TEvent>(Action<TEvent> action) {
            return RegisterHandler(new ActionEventHandler<TEvent>(action));
        }


        /// <summary>
        /// Register an action as an event handler
        /// </summary>
        /// <typeparam name="TEvent">Type of the event to handle</typeparam>
        /// <param name="action">Handler action that handles the event</param>
        /// <remarks>Multiple event handlers can be registered for the same event type</remarks>
        public IEventService RegisterHandler<TEvent>(Func<TEvent, Task> action) {
            return RegisterHandler(new AsyncActionEventHandler<TEvent>(action));
        }

        /// <summary>
        /// Dispatch an object by its exact type and trigger all registered event handlers for that event type
        /// </summary>
        /// <param name="object">Event to handle</param>
        /// <returns>A <see cref="Task"/> that represents the operation of handling the event</returns>
        /// <remarks>Event type is automatically resolved from the event object</remarks>
        public async Task DispatchObject(object @object) {
            await (Task)dispatchMethod.MakeGenericMethod(@object.GetType()).Invoke(this, new object[] { @object })!;
        }

        /// <summary>
        /// Dispatch an event and trigger all registered event handlers for that event type
        /// </summary>
        /// <typeparam name="TEvent">Type of the event to handle</typeparam>
        /// <param name="event">Event to handle</param>
        /// <returns>A <see cref="Task"/> that represents the operation of handling the event</returns>
        /// <remarks>Event type is the (inferred) type parameter <typeparamref name="TEvent"/></remarks>
        public async Task DispatchEvent<TEvent>(TEvent @event) {
            if (eventHandlers.TryGetValue(typeof(TEvent), out var handlers)) {
                foreach (var handler in handlers.Cast<IEventHandler<TEvent>>()) {
                    handler.Handle(@event);
                }
            }

            if (asyncEventHandlers.TryGetValue(typeof(TEvent), out var asyncHandlers)) {
                foreach (var asyncHandler in asyncHandlers.Cast<IAsyncEventHandler<TEvent>>()) {
                    await asyncHandler.HandleAsync(@event);
                }
            }

            if (serviceProvider != null) {
                foreach (var handler in serviceProvider.GetServices<IEventHandler<TEvent>>()) {
                    handler.Handle(@event);
                }

                foreach (var asyncHandler in serviceProvider.GetServices<IAsyncEventHandler<TEvent>>()) {
                    await asyncHandler.HandleAsync(@event);
                }
            }
        }
    }
}
