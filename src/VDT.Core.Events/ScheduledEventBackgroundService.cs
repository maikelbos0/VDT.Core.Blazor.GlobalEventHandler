using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace VDT.Core.Events {
    internal class ScheduledEventBackgroundService : BackgroundService {
        private readonly IScheduledEventService scheduledEventService;

        public ScheduledEventBackgroundService(IScheduledEventService scheduledEventService) {
            this.scheduledEventService = scheduledEventService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            await scheduledEventService.ExecuteAsync(stoppingToken);
        }
    }
}
