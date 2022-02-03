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
                options.Assemblies.Add(typeof(SingletonServiceImplementationTarget).Assembly);
            });

            var service = Assert.Single(services, s => s.ImplementationType == typeof(SingletonServiceImplementationTarget));
            
            Assert.Equal(typeof(ISingletonServiceImplementationTarget), service.ServiceType);
            Assert.Equal(ServiceLifetime.Singleton, service.Lifetime);
        }

        [Fact]
        public void AddAttributeServiceTypeFinders_Adds_ServiceTypeFinder_For_Interface_ServiceAttributes() {
            var services = new ServiceCollection();

            services.AddServices(options => {
                options.AddAttributeServiceTypeFinders();
                options.Assemblies.Add(typeof(ISingletonServiceTarget).Assembly);
            });

            var service = Assert.Single(services, s => s.ServiceType == typeof(ISingletonServiceTarget));

            Assert.Equal(typeof(SingletonServiceTarget), service.ImplementationType);
            Assert.Equal(ServiceLifetime.Singleton, service.Lifetime);
        }

        [Fact]
        public void AddAttributeServiceTypeFinders_Adds_ServiceTypeFinder_For_Base_Class_ServiceAttributes() {
            var services = new ServiceCollection();

            services.AddServices(options => {
                options.AddAttributeServiceTypeFinders();
                options.Assemblies.Add(typeof(SingletonServiceTargetBase).Assembly);
            });

            var service = Assert.Single(services, s => s.ServiceType == typeof(SingletonServiceTargetBase));

            Assert.Equal(typeof(SingletonServiceTarget), service.ImplementationType);
            Assert.Equal(ServiceLifetime.Singleton, service.Lifetime);
        }
    }
}
