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
        public async Task ExecuteAsync_Creates_Task_For_Event_In_Constructor() {
            var fieldInfo = typeof(ScheduledEventService).GetField("scheduledEventRunners", BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new InvalidOperationException($"Field '{nameof(ScheduledEventService)}.scheduledEventRunners' was not found.");
            var eventService = Substitute.For<IEventService>();
            var @event = Substitute.For<IScheduledEvent>();
            var service = new ScheduledEventService(eventService, new[] { @event });
            var tokenSource = new CancellationTokenSource();

            @event.CronExpression.Returns("* * * * *");

            var task = service.ExecuteAsync(tokenSource.Token);

            tokenSource.Cancel();

            await task;

            var scheduledEventRunners = fieldInfo.GetValue(service);

            Assert.NotNull(scheduledEventRunners);
            Assert.Single((Dictionary<IScheduledEvent, Task>)scheduledEventRunners!);
        }

        [Fact]
        public async Task ExecuteAsync_Creates_Task_For_AddScheduledEvent() {
            var fieldInfo = typeof(ScheduledEventService).GetField("scheduledEventRunners", BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new InvalidOperationException($"Field '{nameof(ScheduledEventService)}.scheduledEventRunners' was not found.");
            var eventService = Substitute.For<IEventService>();
            var @event = Substitute.For<IScheduledEvent>();
            var service = new ScheduledEventService(eventService, Enumerable.Empty<IScheduledEvent>());
            var tokenSource = new CancellationTokenSource();

            @event.CronExpression.Returns("* * * * *");
            service.AddScheduledEvent(@event);

            var task = service.ExecuteAsync(tokenSource.Token);

            tokenSource.Cancel();

            await task;

            var scheduledEventRunners = fieldInfo.GetValue(service);

            Assert.NotNull(scheduledEventRunners);
            Assert.Single((Dictionary<IScheduledEvent, Task>)scheduledEventRunners!);
        }

        [Fact]
        public async Task Cancelling_Execute_Stops_Tasks() {
            var fieldInfo = typeof(ScheduledEventService).GetField("scheduledEventRunners", BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new InvalidOperationException($"Field '{nameof(ScheduledEventService)}.scheduledEventRunners' was not found.");
            var eventService = Substitute.For<IEventService>();
            var @event = Substitute.For<IScheduledEvent>();
            var service = new ScheduledEventService(eventService, Enumerable.Empty<IScheduledEvent>());
            var tokenSource = new CancellationTokenSource();

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
