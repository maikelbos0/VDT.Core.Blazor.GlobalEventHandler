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
        [Fact]
        public void Constructor_Adds_ScheduledEvent() {
            var fieldInfo = typeof(ScheduledEventService).GetField("scheduledEvents", BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new InvalidOperationException($"Field '{nameof(ScheduledEventService)}.scheduledEvents' was not found.");
            var eventService = Substitute.For<IEventService>();
            var dateTimeService = Substitute.For<IDateTimeService>();
            var taskService = Substitute.For<ITaskService>();
            var @event = Substitute.For<IScheduledEvent>();
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
            var @event = Substitute.For<IScheduledEvent>();
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
            var @event = Substitute.For<IScheduledEvent>();
            var service = new ScheduledEventService(eventService, dateTimeService, taskService, new[] { @event });
            var tokenSource = new CancellationTokenSource();

            dateTimeService.UtcNow.Returns(DateTime.MinValue.ToUniversalTime());
            taskService.Delay(Arg.Any<TimeSpan>(), Arg.Any<CancellationToken>()).Returns(Task.Delay(1));
            @event.CronExpression.Returns("* * * * *");

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
            var @event = Substitute.For<IScheduledEvent>();
            var service = new ScheduledEventService(eventService, dateTimeService, taskService, Enumerable.Empty<IScheduledEvent>());
            var tokenSource = new CancellationTokenSource();

            dateTimeService.UtcNow.Returns(DateTime.MinValue.ToUniversalTime());
            taskService.Delay(Arg.Any<TimeSpan>(), Arg.Any<CancellationToken>()).Returns(Task.Delay(1));
            @event.CronExpression.Returns("* * * * *");
            service.AddScheduledEvent(@event);

            var task = service.ExecuteAsync(tokenSource.Token);

            tokenSource.Cancel();

            await task;

            var scheduledEventRunners = fieldInfo.GetValue(service);
            var taskRunner = ((Dictionary<IScheduledEvent, Task>)scheduledEventRunners!)[@event];

            Assert.True(taskRunner.IsCompleted);
        }
    }
}
