using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace VDT.Core.Events {
    /// <summary>
    /// Service for registering and dispatching events on a schedule to an <see cref="IEventService"/>
    /// </summary>
    public sealed class ScheduledEventService : IScheduledEventService {
        private readonly IEventService eventService;
        private readonly IDateTimeService dateTimeService;
        private readonly ITaskService taskService;
        private readonly ConcurrentQueue<IScheduledEvent> scheduledEvents = new ConcurrentQueue<IScheduledEvent>();
        private readonly Dictionary<IScheduledEvent, Task> scheduledEventRunners = new Dictionary<IScheduledEvent, Task>();

        /// <summary>
        /// Create a scheduled event service
        /// </summary>
        /// <param name="eventService">Service that handles dispatched events</param>
        /// <param name="dateTimeService">Service for static date and time values</param>
        /// <param name="taskService"></param>
        /// <param name="scheduledEvents">Events that should be dispatched on a schedule</param>
        public ScheduledEventService(IEventService eventService, IDateTimeService dateTimeService, ITaskService taskService, IEnumerable<IScheduledEvent> scheduledEvents) {
            this.eventService = eventService;
            this.dateTimeService = dateTimeService;

            this.taskService = taskService;
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
            while (!stoppingToken.IsCancellationRequested) {
                while (scheduledEvents.TryDequeue(out var scheduledEvent)) {
                    scheduledEventRunners.Add(scheduledEvent, Run(scheduledEvent, stoppingToken));
                }

                await taskService.Delay(TimeSpan.FromMilliseconds(10), CancellationToken.None);
            }
        }

        private async Task Run(IScheduledEvent scheduledEvent, CancellationToken stoppingToken) {
            while (!stoppingToken.IsCancellationRequested) {
                await taskService.Delay(scheduledEvent.GetTimeToNextDispatch(dateTimeService.UtcNow), stoppingToken);

                _ = eventService.DispatchObject(scheduledEvent);
            }
        }
    }
}
