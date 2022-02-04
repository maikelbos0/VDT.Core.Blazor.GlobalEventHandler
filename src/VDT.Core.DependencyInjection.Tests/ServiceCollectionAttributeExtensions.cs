using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Tests.AttributeServiceTargets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class ServiceCollectionAttributeExtensions {
        [Fact]
        public void AddAttributeServices_Adds_Services() {
            var services = new ServiceCollection();

            services.AddAttributeServices(typeof(ISingletonServiceTarget).Assembly);

            Assert.Single(services, s => s.ServiceType == typeof(ISingletonServiceTarget));
            Assert.DoesNotContain(services, s => s.ServiceType == typeof(SingletonServiceTarget));
        }

        [Fact]
        public void AddAttributeServices_Adds_Services_With_Decorators() {
            var services = new ServiceCollection();

            services.AddAttributeServices(typeof(ISingletonServiceTarget).Assembly, options => options.AddAttributeDecorators());

            Assert.Single(services, s => s.ServiceType == typeof(ISingletonServiceTarget));
            Assert.Single(services, s => s.ServiceType == typeof(SingletonServiceTarget));
        }
    }
}
