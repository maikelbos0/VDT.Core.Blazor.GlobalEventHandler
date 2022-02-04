using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Tests.AttributeServiceTargets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class ServiceRegistrationOptionsAttributeExtensionsTests {
        [Fact]
        public void AddAttributeServiceTypeFinders_Adds_ServiceTypeFinder_For_ImplementationServiceAttributes() {
            var services = new ServiceCollection();

            services.AddServices(options => {
                options.AddAttributeServiceTypeFinders();
                options.Assemblies.Add(typeof(AttributeServiceImplementationTarget).Assembly);
            });

            var service = Assert.Single(services, s => s.ImplementationType == typeof(AttributeServiceImplementationTarget));
            
            Assert.Equal(typeof(IAttributeServiceImplementationTarget), service.ServiceType);
            Assert.Equal(ServiceLifetime.Singleton, service.Lifetime);
        }

        [Fact]
        public void AddAttributeServiceTypeFinders_Adds_ServiceTypeFinder_For_Interface_ServiceAttributes() {
            var services = new ServiceCollection();

            services.AddServices(options => {
                options.AddAttributeServiceTypeFinders();
                options.Assemblies.Add(typeof(IAttributeServiceTarget).Assembly);
            });

            var service = Assert.Single(services, s => s.ServiceType == typeof(IAttributeServiceTarget));

            Assert.Equal(typeof(AttributeServiceTarget), service.ImplementationType);
            Assert.Equal(ServiceLifetime.Singleton, service.Lifetime);
        }

        [Fact]
        public void AddAttributeServiceTypeFinders_Adds_ServiceTypeFinder_For_Base_Class_ServiceAttributes() {
            var services = new ServiceCollection();

            services.AddServices(options => {
                options.AddAttributeServiceTypeFinders();
                options.Assemblies.Add(typeof(AttributeServiceTargetBase).Assembly);
            });

            var service = Assert.Single(services, s => s.ServiceType == typeof(AttributeServiceTargetBase));

            Assert.Equal(typeof(AttributeServiceTarget), service.ImplementationType);
            Assert.Equal(ServiceLifetime.Singleton, service.Lifetime);
        }
    }
}
