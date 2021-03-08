using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Tests.Decorators;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class ServiceCollectionExtensionsTests {
        private readonly ServiceCollection services;
        private readonly TestDecorator decorator;

        public ServiceCollectionExtensionsTests() {
            services = new ServiceCollection();
            decorator = new TestDecorator();

            services.AddSingleton(decorator);
        }

        [Fact]
        public void AddAttributeServices_Adds_Scoped_Services() {
            services.AddAttributeServices(typeof(ServiceCollectionExtensionsTests).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<IScopedServiceTarget>();

            Assert.IsType<ScopedServiceTarget>(service);
        }

        [Fact]
        public void AddAttributeServices_Adds_Scoped_Services_With_Decorators() {
            services.AddAttributeServices(typeof(ServiceCollectionExtensionsTests).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            var proxy = serviceProvider.GetRequiredService<IScopedServiceTarget>();

            Assert.Equal("Bar", proxy.GetValue());

            Assert.Equal(1, decorator.Calls);
        }
    }
}
