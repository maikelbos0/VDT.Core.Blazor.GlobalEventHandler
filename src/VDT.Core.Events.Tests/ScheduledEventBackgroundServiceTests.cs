using NSubstitute;
using System.Threading;
using Xunit;

namespace VDT.Core.Events.Tests {
    public sealed class ScheduledEventBackgroundServiceTests {
        [Fact]
        public void ExecuteAsync_Calls_ScheduledEventService_ExecuteAsync() {
            var scheduledEventService = Substitute.For<IScheduledEventService>();
            var service = new ScheduledEventBackgroundService(scheduledEventService);
            var tokenSource = new CancellationTokenSource();

            service.StartAsync(tokenSource.Token);

            scheduledEventService.Received(1).ExecuteAsync(Arg.Any<CancellationToken>());
        }
    }
}
