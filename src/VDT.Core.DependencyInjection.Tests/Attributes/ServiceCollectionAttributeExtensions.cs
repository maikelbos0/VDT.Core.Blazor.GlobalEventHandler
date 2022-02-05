using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Attributes;
using VDT.Core.DependencyInjection.Tests.AttributeServiceTargets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Attributes {
    public class ServiceCollectionAttributeExtensions {
        [Fact]
        public void AddAttributeServices_Adds_Services() {
            var services = new ServiceCollection();

            services.AddAttributeServices(typeof(IAttributeServiceInterfaceTarget).Assembly);

            Assert.Single(services, s => s.ServiceType == typeof(IAttributeServiceInterfaceTarget));
            Assert.DoesNotContain(services, s => s.ServiceType == typeof(AttributeServiceInterfaceTarget));
        }

        [Fact]
        public void AddAttributeServices_Adds_Services_With_Decorators() {
            var services = new ServiceCollection();

            services.AddAttributeServices(typeof(IAttributeServiceInterfaceTarget).Assembly, options => options.AddAttributeDecorators());

            Assert.NotNull(Assert.Single(services, s => s.ServiceType == typeof(IAttributeServiceInterfaceTarget)).ImplementationFactory);
            Assert.Single(services, s => s.ServiceType == typeof(AttributeServiceInterfaceTarget));
        }
    }
}
