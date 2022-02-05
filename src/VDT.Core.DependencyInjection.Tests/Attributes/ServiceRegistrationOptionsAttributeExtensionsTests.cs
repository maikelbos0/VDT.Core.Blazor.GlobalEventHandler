using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Attributes;
using VDT.Core.DependencyInjection.Tests.AttributeServiceTargets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Attributes {
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
                options.Assemblies.Add(typeof(IAttributeServiceInterfaceTarget).Assembly);
            });

            var service = Assert.Single(services, s => s.ServiceType == typeof(IAttributeServiceInterfaceTarget));

            Assert.Equal(typeof(AttributeServiceInterfaceTarget), service.ImplementationType);
            Assert.Equal(ServiceLifetime.Singleton, service.Lifetime);
        }

        [Fact]
        public void AddAttributeServiceTypeFinders_Adds_ServiceTypeFinder_For_Base_Class_ServiceAttributes() {
            var services = new ServiceCollection();

            services.AddServices(options => {
                options.AddAttributeServiceTypeFinders();
                options.Assemblies.Add(typeof(AttributeServiceBaseClassTargetBase).Assembly);
            });

            var service = Assert.Single(services, s => s.ServiceType == typeof(AttributeServiceBaseClassTargetBase));

            Assert.Equal(typeof(AttributeServiceBaseClassTarget), service.ImplementationType);
            Assert.Equal(ServiceLifetime.Singleton, service.Lifetime);
        }
    }
}
