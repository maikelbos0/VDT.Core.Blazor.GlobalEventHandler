using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace VDT.Core.Events.Tests {
    public sealed class EventServiceTests {
        public class FooEvent { }
        public class BarEvent { }

        [Fact]
        public async Task DispatchEvent_Calls_Registered_EventHandler() {
            var service = new EventService();
            var @event = new FooEvent();
            var handler = Substitute.For<IEventHandler<FooEvent>>();

            service.RegisterHandler(handler);

            await service.DispatchEvent(@event);

            handler.Received(1).Handle(@event);
        }

        [Fact]
        public async Task DispatchEvent_Calls_Registered_EventHandler_Action() {
            var service = new EventService();
            var @event = new FooEvent();
            var handler = Substitute.For<Action<FooEvent>>();

            service.RegisterHandler(handler);

            await service.DispatchEvent(@event);

            handler.Received(1).Invoke(@event);
        }

        [Fact]
        public async Task DispatchEvent_Calls_All_Registered_EventHandlers() {
            var service = new EventService();
            var @event = new FooEvent();
            var handler1 = Substitute.For<IEventHandler<FooEvent>>();
            var handler2 = Substitute.For<IEventHandler<FooEvent>>();

            service.RegisterHandler(handler1);
            service.RegisterHandler(handler2);

            await service.DispatchEvent(@event);

            handler1.Received(1).Handle(@event);
            handler2.Received(1).Handle(@event);
        }

        [Fact]
        public async Task DispatchEvent_Calls_Only_EventHandlers_For_Correct_Event_Type() {
            var service = new EventService();
            var handler = Substitute.For<IEventHandler<BarEvent>>();

            service.RegisterHandler(handler);

            await service.DispatchEvent(new FooEvent());

            handler.DidNotReceive().Handle(Arg.Any<BarEvent>());
        }

        [Fact]
        public async Task DispatchEvent_Calls_Registered_AsyncEventHandler() {
            var service = new EventService();
            var @event = new FooEvent();
            var handler = Substitute.For<IAsyncEventHandler<FooEvent>>();

            service.RegisterHandler(handler);

            await service.DispatchEvent(@event);

            await handler.Received(1).HandleAsync(@event);
        }

        [Fact]
        public async Task DispatchEvent_Calls_Registered_AsyncEventHandler_Action() {
            var service = new EventService();
            var @event = new FooEvent();
            var handler = Substitute.For<Func<FooEvent, Task>>();

            service.RegisterHandler(handler);

            await service.DispatchEvent(@event);

            await handler.Received(1).Invoke(@event);
        }

        [Fact]
        public async Task DispatchEvent_Calls_All_Registered_AsyncEventHandlers() {
            var service = new EventService();
            var @event = new FooEvent();
            var handler1 = Substitute.For<IAsyncEventHandler<FooEvent>>();
            var handler2 = Substitute.For<IAsyncEventHandler<FooEvent>>();

            service.RegisterHandler(handler1);
            service.RegisterHandler(handler2);

            await service.DispatchEvent(@event);

            await handler1.Received(1).HandleAsync(@event);
            await handler2.Received(1).HandleAsync(@event);
        }

        [Fact]
        public async Task DispatchEvent_Calls_Only_AsyncEventHandlers_For_Correct_Event_Type() {
            var service = new EventService();
            var handler = Substitute.For<IAsyncEventHandler<BarEvent>>();

            service.RegisterHandler(handler);

            await service.DispatchEvent(new FooEvent());

            await handler.DidNotReceive().HandleAsync(Arg.Any<BarEvent>());
        }

        [Fact]
        public async Task DispatchEvent_Calls_EventHandler_From_ServiceProvider() {
            var handler = Substitute.For<IEventHandler<FooEvent>>();
            var serviceProvider = new ServiceCollection()
                .AddSingleton(handler)
                .BuildServiceProvider();
            var service = new EventService(serviceProvider);
            var @event = new FooEvent();

            await service .DispatchEvent(@event);

            handler.Received(1).Handle(@event);
        }

        [Fact]
        public async Task DispatchEvent_Calls_All_EventHandlers_From_ServiceProvider() {
            var handler1 = Substitute.For<IEventHandler<FooEvent>>();
            var handler2 = Substitute.For<IEventHandler<FooEvent>>();
            var serviceProvider = new ServiceCollection()
                .AddSingleton(handler1)
                .AddSingleton(handler2)
                .BuildServiceProvider();
            var service = new EventService(serviceProvider);
            var @event = new FooEvent();

            await service.DispatchEvent(@event);

            handler1.Received(1).Handle(@event);
            handler2.Received(1).Handle(@event);
        }

        [Fact]
        public async Task DispatchEvent_Calls_Only_EventHandlers_From_ServiceProvider_For_Correct_Event_Type() {
            var handler = Substitute.For<IEventHandler<BarEvent>>();
            var serviceProvider = new ServiceCollection()
                .AddSingleton(handler)
                .BuildServiceProvider();
            var service = new EventService(serviceProvider);

            await service.DispatchEvent(new FooEvent());

            handler.DidNotReceive().Handle(Arg.Any<BarEvent>());
        }

        [Fact]
        public async Task DispatchEvent_Calls_AsyncEventHandler_From_ServiceProvider() {
            var handler = Substitute.For<IAsyncEventHandler<FooEvent>>();
            var serviceProvider = new ServiceCollection()
                .AddSingleton(handler)
                .BuildServiceProvider();
            var service = new EventService(serviceProvider);
            var @event = new FooEvent();

            await service.DispatchEvent(@event);

            await handler.Received(1).HandleAsync(@event);
        }

        [Fact]
        public async Task DispatchEvent_Calls_All_AsyncEventHandlers_From_ServiceProvider() {
            var handler1 = Substitute.For<IAsyncEventHandler<FooEvent>>();
            var handler2 = Substitute.For<IAsyncEventHandler<FooEvent>>();
            var serviceProvider = new ServiceCollection()
                .AddSingleton(handler1)
                .AddSingleton(handler2)
                .BuildServiceProvider();
            var service = new EventService(serviceProvider);
            var @event = new FooEvent();

            await service.DispatchEvent(@event);

            await handler1.Received(1).HandleAsync(@event);
            await handler2.Received(1).HandleAsync(@event);
        }

        [Fact]
        public async Task DispatchEvent_Calls_Only_AsyncEventHandlers_From_ServiceProvider_For_Correct_Event_Type() {
            var handler = Substitute.For<IAsyncEventHandler<BarEvent>>();
            var serviceProvider = new ServiceCollection()
                .AddSingleton(handler)
                .BuildServiceProvider();
            var service = new EventService(serviceProvider);

            await service.DispatchEvent(new FooEvent());

            await handler.DidNotReceive().HandleAsync(Arg.Any<BarEvent>());
        }

        [Fact]
        public async Task DispatchEvent_Only_Manually_Registered_Succeeds_When_ServiceProvider_Available() {
            var handler = Substitute.For<IEventHandler<FooEvent>>();
            var serviceProvider = new ServiceCollection()
                .BuildServiceProvider();
            var service = new EventService(serviceProvider);
            var @event = new FooEvent();

            service.RegisterHandler(handler);

            await service.DispatchEvent(@event);

            handler.Received(1).Handle(@event);
        }

        [Fact]
        public async Task DispatchObject_Dispatches_As_Correct_Type() {
            var eventHandler = Substitute.For<IEventHandler<FooEvent>>();
            var service = new EventService();
            var scheduledEvent = (object)new FooEvent();

            service.RegisterHandler(eventHandler);

            await service.DispatchObject(scheduledEvent);

            eventHandler.Received(1).Handle(Arg.Any<FooEvent>());
        }

        // TODO async handler + provided async handler tests
    }
}
