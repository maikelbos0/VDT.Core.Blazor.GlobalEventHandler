using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace VDT.Core.Events {
    /// <summary>
    /// Service for registering and dispatching events on a schedule to an <see cref="IEventService"/>
    /// </summary>
    public sealed class ScheduledEventService : IScheduledEventService, IDisposable {
        private readonly IEventService eventService;
        private readonly ConcurrentQueue<IScheduledEvent> scheduledEvents = new ConcurrentQueue<IScheduledEvent>();
        private readonly Dictionary<IScheduledEvent, Task> scheduledEventRunners = new Dictionary<IScheduledEvent, Task>();
        private CancellationToken? stoppingToken;

        /// <summary>
        /// Create a scheduled event service
        /// </summary>
        /// <param name="eventService">Service that handles dispatched events</param>
        /// <param name="scheduledEvents">Events that should be dispatched on a schedule</param>
        public ScheduledEventService(IEventService eventService, IEnumerable<IScheduledEvent> scheduledEvents) {
            this.eventService = eventService;

            foreach (var scheduledEvent in scheduledEvents) {
                this.scheduledEvents.Enqueue(scheduledEvent);
            }
        }

        /// <summary>
        /// Add an <see cref="IScheduledEvent"/> to be dispatched by the service
        /// </summary>
        /// <param name="scheduledEvent">Event that should be dispatched on a schedule</param>
        public void AddScheduledEvent(IScheduledEvent scheduledEvent) {
            scheduledEvents.Enqueue(scheduledEvent);
        }

        /// <summary>
        /// Start the operation of dispatching the scheduled events
        /// </summary>
        /// <param name="stoppingToken">Triggered when dispatching should stop</param>
        /// <returns>A <see cref="Task"/> that represents the operation of dispatching the scheduled events</returns>
        public async Task ExecuteAsync(CancellationToken stoppingToken) {
            if (this.stoppingToken.HasValue) {
                return;
            }

            this.stoppingToken = stoppingToken;

            while (!stoppingToken.IsCancellationRequested) {
                while (scheduledEvents.TryDequeue(out var scheduledEvent)) {
                    scheduledEventRunners.Add(scheduledEvent, Run(scheduledEvent));
                }

                await Task.Delay(10);
            }
        }

        private async Task Run(IScheduledEvent scheduledEvent) {
            //while (stoppingToken != null && !stoppingToken.Value.IsCancellationRequested) {
            //    await Task.Delay(scheduledEvent.GetTimeToNextDispatch(DateTime.UtcNow), stoppingToken.Value);

            //    _ = eventService.DispatchObject(scheduledEvent);
            //}
        }

        void IDisposable.Dispose() {
        }
    }
}
