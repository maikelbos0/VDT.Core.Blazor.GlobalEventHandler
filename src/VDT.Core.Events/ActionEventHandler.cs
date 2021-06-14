using System;

namespace VDT.Core.Events {
    internal sealed class ActionEventHandler<TEvent> : IEventHandler<TEvent> {
        private readonly Action<TEvent> action;

        public ActionEventHandler(Action<TEvent> action) {
            this.action = action;
        }

        public void Handle(TEvent domainEvent) {
            action(domainEvent);
        }
    }
}
