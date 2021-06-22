using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace VDT.Core.Events.Tests {
    public sealed class ScheduledEventServiceTests {
        public class FooEvent : IScheduledEvent {
            public string CronExpression { get; } = "* * * * * *";
            public bool CronExpressionIncludesSeconds { get; } = true;
            public DateTime? PreviousDispatch { get; set; }
        }

        [Fact]
        public void Constructor_Adds_ScheduledEvent() {
            var fieldInfo = typeof(ScheduledEventService).GetField("scheduledEvents", BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new InvalidOperationException($"Field '{nameof(ScheduledEventService)}.scheduledEvents' was not found.");
            var eventService = Substitute.For<IEventService>();
            var dateTimeService = Substitute.For<IDateTimeService>();
            var taskService = Substitute.For<ITaskService>();
            var @event = new FooEvent();
            var service = new ScheduledEventService(eventService, dateTimeService, taskService, new[] { @event });

            var scheduledEvents = (IEnumerable<IScheduledEvent>?)fieldInfo.GetValue(service);

            Assert.NotNull(scheduledEvents);
            Assert.Equal(@event, Assert.Single(scheduledEvents!));
        }

        [Fact]
        public void AddScheduledEvent_Adds_ScheduledEvent() {
            var fieldInfo = typeof(ScheduledEventService).GetField("scheduledEvents", BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new InvalidOperationException($"Field '{nameof(ScheduledEventService)}.scheduledEvents' was not found.");
            var eventService = Substitute.For<IEventService>();
            var dateTimeService = Substitute.For<IDateTimeService>();
            var taskService = Substitute.For<ITaskService>();
            var @event = new FooEvent();
            var service = new ScheduledEventService(eventService, dateTimeService, taskService, new[] { @event });

            var scheduledEvents = (IEnumerable<IScheduledEvent>?)fieldInfo.GetValue(service);

            Assert.NotNull(scheduledEvents);
            Assert.Equal(@event, Assert.Single(scheduledEvents!));
        }


        [Fact]
        public async Task ExecuteAsync_Creates_Task_For_ScheduledEvent() {
            var fieldInfo = typeof(ScheduledEventService).GetField("scheduledEventRunners", BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new InvalidOperationException($"Field '{nameof(ScheduledEventService)}.scheduledEventRunners' was not found.");
            var eventService = Substitute.For<IEventService>();
            var dateTimeService = Substitute.For<IDateTimeService>();
            var taskService = Substitute.For<ITaskService>();
            var @event = new FooEvent();
            var service = new ScheduledEventService(eventService, dateTimeService, taskService, new[] { @event });
            var tokenSource = new CancellationTokenSource();

            dateTimeService.UtcNow.Returns(DateTime.MinValue.ToUniversalTime());
            taskService.Delay(Arg.Any<TimeSpan>(), Arg.Any<CancellationToken>()).Returns(Task.Delay(1));

            var task = service.ExecuteAsync(tokenSource.Token);

            tokenSource.Cancel();

            await task;

            var scheduledEventRunners = fieldInfo.GetValue(service);

            Assert.NotNull(scheduledEventRunners);
            Assert.Single((Dictionary<IScheduledEvent, Task>)scheduledEventRunners!);
        }

        [Fact]
        public async Task Cancelling_ExecuteAsync_Stops_Tasks() {
            var fieldInfo = typeof(ScheduledEventService).GetField("scheduledEventRunners", BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new InvalidOperationException($"Field '{nameof(ScheduledEventService)}.scheduledEventRunners' was not found.");
            var eventService = Substitute.For<IEventService>();
            var dateTimeService = Substitute.For<IDateTimeService>();
            var taskService = Substitute.For<ITaskService>();
            var @event = new FooEvent();
            var service = new ScheduledEventService(eventService, dateTimeService, taskService, Enumerable.Empty<IScheduledEvent>());
            var tokenSource = new CancellationTokenSource();

            dateTimeService.UtcNow.Returns(DateTime.MinValue.ToUniversalTime());
            taskService.Delay(Arg.Any<TimeSpan>(), Arg.Any<CancellationToken>()).Returns(Task.Delay(1));
            service.AddScheduledEvent(@event);

            var task = service.ExecuteAsync(tokenSource.Token);

            tokenSource.Cancel();

            await task;

            var scheduledEventRunners = fieldInfo.GetValue(service);
            var taskRunner = ((Dictionary<IScheduledEvent, Task>)scheduledEventRunners!)[@event];

            Assert.True(taskRunner.IsCompleted);
        }

        [Fact]
        public async Task ExecuteAsync_Dispatches_Event() {
            var eventService = Substitute.For<IEventService>();
            var dateTimeService = Substitute.For<IDateTimeService>();
            var taskService = Substitute.For<ITaskService>();
            var @event = new FooEvent();
            var service = new ScheduledEventService(eventService, dateTimeService, taskService, Enumerable.Empty<IScheduledEvent>());
            var tokenSource = new CancellationTokenSource();

            dateTimeService.UtcNow.Returns(DateTime.UtcNow);
            taskService.Delay(Arg.Any<TimeSpan>(), Arg.Any<CancellationToken>()).Returns(Task.Delay(1));
            service.AddScheduledEvent(@event);

            var delay = TimeSpan.FromSeconds(Math.Ceiling(dateTimeService.UtcNow.TimeOfDay.TotalSeconds)) - dateTimeService.UtcNow.TimeOfDay;
            var task = service.ExecuteAsync(tokenSource.Token);

            tokenSource.Cancel();

            await task;

            Assert.Equal(dateTimeService.UtcNow, @event.PreviousDispatch);            
            await taskService.Received().Delay(delay, tokenSource.Token);
            await eventService.Received().DispatchObject(@event);
        }
    }
}
