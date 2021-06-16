using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System.Linq;
using Xunit;

namespace VDT.Core.Events.Tests {
    public sealed class ScheduledEventServiceTests {
        public class TestEvent : IScheduledEvent {
            public string CronExpression => throw new System.NotImplementedException();
        }

        [Fact]
        public void Dispatch_Dispatches_Correctly() {
            var eventHandler = Substitute.For<IEventHandler<TestEvent>>();
            var eventService = new EventService();
            var scheduledEventService = new ScheduledEventService(eventService, Enumerable.Empty<IScheduledEvent>());
            var scheduledEvent = (IScheduledEvent)new TestEvent();

            eventService.RegisterHandler(eventHandler);

            scheduledEventService.Dispatch(scheduledEvent);

            eventHandler.Received(1).Handle(Arg.Any<TestEvent>());
        }

        [Fact]
        public void Test() {
            var serviceProvider = new ServiceCollection()
                .AddEventService()
                .AddSingleton<ScheduledEventService>()
                .BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<ScheduledEventService>();
        }

    }
}
