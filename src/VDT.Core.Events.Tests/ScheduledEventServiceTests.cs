using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System.Linq;
using Xunit;

namespace VDT.Core.Events.Tests {
    public sealed class ScheduledEventServiceTests {
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
