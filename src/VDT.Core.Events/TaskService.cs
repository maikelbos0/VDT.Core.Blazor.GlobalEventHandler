using System;
using System.Threading;
using System.Threading.Tasks;

namespace VDT.Core.Events {
    /// <summary>
    /// Service for static task members
    /// </summary>
    public sealed class TaskService : ITaskService {
        /// <summary>
        /// Creates a cancellable task that completes after a specified time interval
        /// </summary>
        /// <param name="delay">The time span to wait before completing the returned task, or TimeSpan.FromMilliseconds(-1) to wait indefinitely</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete</param>
        /// <returns>A task that represents the time delay</returns>
        public async Task Delay(TimeSpan delay, CancellationToken cancellationToken) {
            await Task.Delay(delay, cancellationToken);
        }
    }
}
