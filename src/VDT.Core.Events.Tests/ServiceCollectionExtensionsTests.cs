using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace VDT.Core.Events.Tests {
    public sealed class ServiceCollectionExtensionsTests {
        [Fact]
        public void AddEventService_Adds_EventService() {
            var serviceProvider = new ServiceCollection()
                .AddEventService()
                .BuildServiceProvider();

            Assert.IsType<EventService>(serviceProvider.GetRequiredService<IEventService>());
        }

        [Fact]
        public void AddEventService_Adds_EventService_As_Singleton() {
            var serviceProvider = new ServiceCollection()
                .AddEventService()
                .BuildServiceProvider();
            IEventService service;

            using (var scope = serviceProvider.CreateScope()) {
                service = scope.ServiceProvider.GetRequiredService<IEventService>();
            }

            using (var scope = serviceProvider.CreateScope()) {
                Assert.Same(service, scope.ServiceProvider.GetRequiredService<IEventService>());
            }
        }

        [Fact]
        public void AddEventService_Adds_EventService_With_ServiceProvider() {
            var fieldInfo = typeof(EventService).GetField("serviceProvider", BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new InvalidOperationException($"Field '{nameof(EventService)}.serviceProvider' was not found.");
            var serviceProvider = new ServiceCollection()
                .AddEventService()
                .BuildServiceProvider();

            Assert.NotNull(fieldInfo.GetValue(serviceProvider.GetRequiredService<IEventService>()));
        }

        [Fact]
        public void AddScheduledEventService_Adds_ScheduledEventBackgroundService_As_HostedService() {
            var serviceProvider = new ServiceCollection()
                .AddEventService()
                .AddScheduledEventService()
                .BuildServiceProvider();

            var hostedServices = serviceProvider.GetServices<IHostedService>();

            Assert.IsType<ScheduledEventBackgroundService>(Assert.Single(hostedServices));
        }

        [Fact]
        public void AddScheduledEventService_Adds_ScheduledEventService() {
            var serviceProvider = new ServiceCollection()
                .AddEventService()
                .AddScheduledEventService()
                .BuildServiceProvider();

            Assert.IsType<ScheduledEventService>(serviceProvider.GetRequiredService<IScheduledEventService>());
        }

        [Fact]
        public void AddEventService_Adds_ScheduledEventService_As_Singleton() {
            var serviceProvider = new ServiceCollection()
                .AddEventService()
                .AddScheduledEventService()
                .BuildServiceProvider();
            IScheduledEventService service;

            using (var scope = serviceProvider.CreateScope()) {
                service = scope.ServiceProvider.GetRequiredService<IScheduledEventService>();
            }

            using (var scope = serviceProvider.CreateScope()) {
                Assert.Same(service, scope.ServiceProvider.GetRequiredService<IScheduledEventService>());
            }
        }

        [Fact]
        public void AddScheduledEventService_Adds_ScheduledEventService_With_Scheduled_Events() {
            var fieldInfo = typeof(ScheduledEventService).GetField("scheduledEvents", BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new InvalidOperationException($"Field '{nameof(ScheduledEventService)}.scheduledEvents' was not found.");
            var scheduledEvent = Substitute.For<IScheduledEvent>();
            var serviceProvider = new ServiceCollection()
                .AddScoped(serviceProvider => scheduledEvent)
                .AddEventService()
                .AddScheduledEventService()
                .BuildServiceProvider();

            var scheduledEvents = fieldInfo.GetValue(serviceProvider.GetRequiredService<IScheduledEventService>());

            Assert.NotNull(scheduledEvents);
            Assert.Single((IEnumerable<IScheduledEvent>)scheduledEvents!);
        }
    }
}
