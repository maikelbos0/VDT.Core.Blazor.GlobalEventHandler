using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class ServiceCollectionExtensionsTests {
        private readonly ServiceCollection services;

        public ServiceCollectionExtensionsTests() {
            services = new ServiceCollection();
        }

        [Fact]
        public void AddAttributeServices_Adds_Scoped_Services() {
            services.AddAttributeServices(typeof(ServiceCollectionExtensionsTests).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<IScopedServiceTarget>();

            Assert.IsType<ScopedServiceTarget>(service);
        }
    }
}
