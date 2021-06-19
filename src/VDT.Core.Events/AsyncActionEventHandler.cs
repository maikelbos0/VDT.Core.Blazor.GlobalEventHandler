using System;
using System.Threading.Tasks;

namespace VDT.Core.Events {
    internal sealed class AsyncActionEventHandler<TEvent> : IAsyncEventHandler<TEvent> {
        private readonly Func<TEvent, Task> action;

        public AsyncActionEventHandler(Func<TEvent, Task> action) {
            this.action = action;
        }

        public async Task HandleAsync(TEvent @event) {
            await action(@event);
        }
    }
}
