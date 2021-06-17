using Microsoft.Extensions.DependencyInjection;
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

        [Fact]
        public void Dispatch_Calls_EventHandler_From_ServiceProvider() {
            var handler = Substitute.For<IEventHandler<FooEvent>>();
            var serviceProvider = new ServiceCollection()
                .AddSingleton(handler)
                .BuildServiceProvider();
            var service = new EventService(serviceProvider);
            var @event = new FooEvent();

            service.Dispatch(@event);

            handler.Received(1).Handle(@event);
        }

        [Fact]
        public void Dispatch_Calls_All_EventHandlers_From_ServiceProvider() {
            var handler1 = Substitute.For<IEventHandler<FooEvent>>();
            var handler2 = Substitute.For<IEventHandler<FooEvent>>();
            var serviceProvider = new ServiceCollection()
                .AddSingleton(handler1)
                .AddSingleton(handler2)
                .BuildServiceProvider();
            var service = new EventService(serviceProvider);
            var @event = new FooEvent();

            service.Dispatch(@event);

            handler1.Received(1).Handle(@event);
            handler2.Received(1).Handle(@event);
        }

        [Fact]
        public void Dispatch_Calls_Only_EventHandlers_From_ServiceProvider_For_Correct_Event_Type() {
            var handler = Substitute.For<IEventHandler<BarEvent>>();
            var serviceProvider = new ServiceCollection()
                .AddSingleton(handler)
                .BuildServiceProvider();
            var service = new EventService(serviceProvider);

            service.Dispatch(new FooEvent());

            handler.DidNotReceive().Handle(Arg.Any<BarEvent>());
        }

        [Fact]
        public void Dispatch_Calls_Both_EventHandler_From_ServiceProvider_And_Manually_Registered() {
            var handler1 = Substitute.For<IEventHandler<FooEvent>>();
            var handler2 = Substitute.For<IEventHandler<FooEvent>>();
            var serviceProvider = new ServiceCollection()
                .AddSingleton(handler1)
                .BuildServiceProvider();
            var service = new EventService(serviceProvider);
            var @event = new FooEvent();

            service.RegisterHandler(handler2);

            service.Dispatch(@event);

            handler1.Received(1).Handle(@event);
            handler2.Received(1).Handle(@event);
        }

        [Fact]
        public void Dispatch_Only_Manually_Registered_Succeeds_When_ServiceProvider_Available() {
            var handler = Substitute.For<IEventHandler<FooEvent>>();
            var serviceProvider = new ServiceCollection()
                .BuildServiceProvider();
            var service = new EventService(serviceProvider);
            var @event = new FooEvent();

            service.RegisterHandler(handler);

            service.Dispatch(@event);

            handler.Received(1).Handle(@event);
        }


        [Fact]
        public void Dispatch_Object_Dispatches_Correctly() {
            var eventHandler = Substitute.For<IEventHandler<FooEvent>>();
            var eventService = new EventService();
            var scheduledEvent = (object)new FooEvent();

            eventService.RegisterHandler(eventHandler);

            eventService.Dispatch(scheduledEvent);

            eventHandler.Received(1).Handle(Arg.Any<FooEvent>());
        }
    }
}
