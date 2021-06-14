using NSubstitute;
using System;
using Xunit;

namespace VDT.Core.Events.Tests {
    public sealed class EventServiceTests {
        public class FooEvent { }
        public class BarEvent { }

        [Fact]
        public void Dispatch_Calls_Registered_EventHandler() {
            var service = new EventService();
            var @event = new FooEvent();
            var handler = Substitute.For<IEventHandler<FooEvent>>();

            service.RegisterHandler(handler);

            service.Dispatch(@event);

            handler.Received(1).Handle(@event);
        }

        [Fact]
        public void Dispatch_Calls_Registered_EventHandler_Action() {
            var service = new EventService();
            var @event = new FooEvent();
            var handler = Substitute.For<Action<FooEvent>>();

            service.RegisterHandler(handler);

            service.Dispatch(@event);

            handler.Received(1).Invoke(@event);
        }

        [Fact]
        public void Dispatch_Calls_All_Registered_EventHandlers() {
            var service = new EventService();
            var @event = new FooEvent();
            var handler1 = Substitute.For<IEventHandler<FooEvent>>();
            var handler2 = Substitute.For<IEventHandler<FooEvent>>();

            service.RegisterHandler(handler1);
            service.RegisterHandler(handler2);

            service.Dispatch(@event);

            handler1.Received(1).Handle(@event);
            handler2.Received(1).Handle(@event);
        }

        [Fact]
        public void Dispatch_Calls_Only_EventHandlers_For_Correct_Event_Type() {
            var service = new EventService();
            var handler = Substitute.For<IEventHandler<BarEvent>>();

            service.RegisterHandler(handler);

            service.Dispatch(new FooEvent());

            handler.DidNotReceive().Handle(Arg.Any<BarEvent>());
        }
    }
}
