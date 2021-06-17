using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace VDT.Core.Events {
    /// <summary>
    /// Service for registering and dispatching events on a schedule to an <see cref="IEventService"/>
    /// </summary>
    public sealed class ScheduledEventService : IScheduledEventService, IDisposable {
        private readonly IEventService eventService;
        private List<IScheduledEvent> scheduledEvents = new List<IScheduledEvent>();

        /// <summary>
        /// Create a scheduled event service
        /// </summary>
        /// <param name="eventService">Service that handles dispatched events</param>
        /// <param name="scheduledEvents">Events that should be dispatched on a schedule</param>
        public ScheduledEventService(IEventService eventService, IEnumerable<IScheduledEvent> scheduledEvents) {
            this.eventService = eventService;
            this.scheduledEvents.AddRange(scheduledEvents);
        }

        /// <summary>
        /// Add an <see cref="IScheduledEvent"/> to be dispatched by the service
        /// </summary>
        /// <param name="scheduledEvent">Event that should be dispatched on a schedule</param>
        public void AddScheduledEvent(IScheduledEvent scheduledEvent) {
            scheduledEvents.Add(scheduledEvent);
        }

        /// <summary>
        /// Start the operation of dispatching the scheduled events
        /// </summary>
        /// <param name="stoppingToken">Triggered when dispatching should stop</param>
        /// <returns>A <see cref="Task"/> that represents the operation of dispatching the scheduled events</returns>
        public Task ExecuteAsync(CancellationToken stoppingToken) {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose() {
        }
    }
}
