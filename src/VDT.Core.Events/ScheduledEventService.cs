using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace VDT.Core.Events {
    // TODO split the scheduler service and the background job

    /// <summary>
    /// Service for dispatching events to an <see cref="IEventService"/> on a schedule
    /// </summary>
    public class ScheduledEventService : BackgroundService {
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
        /// <param name="stoppingToken">Triggered when <see cref="IHostedService.StopAsync(CancellationToken)"/> is called</param>
        /// <returns>A <see cref="Task"/> that represents the operation of dispatching the scheduled events</returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken) {
            throw new NotImplementedException();
        }

        internal void Dispatch(IScheduledEvent scheduledEvent) {
            //eventService.Dispatch(scheduledEvent);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources
        /// </summary>
        public override void Dispose() {
            base.Dispose();
        }
    }
}
