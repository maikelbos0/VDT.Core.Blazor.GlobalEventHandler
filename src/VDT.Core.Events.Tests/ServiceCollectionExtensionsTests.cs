using Microsoft.Extensions.DependencyInjection;
using System;
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
    }
}
