using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class SingleTonServiceImplementationAttributeTests : ServiceAttributeTests {
        [Fact]
        public void AddAttributeServices_Adds_Services() {
            services.AddAttributeServices(typeof(ServiceAttributeTests).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<ISingletonServiceImplementationTarget>();

            Assert.IsType<SingletonServiceImplementationTarget>(service);
        }

        [Fact]
        public void AddAttributeServices_Always_Returns_Same_Object() {
            services.AddAttributeServices(typeof(ServiceAttributeTests).Assembly);

            var serviceProvider = services.BuildServiceProvider();
            ISingletonServiceImplementationTarget singletonTarget;

            using (var scope = serviceProvider.CreateScope()) {
                singletonTarget = scope.ServiceProvider.GetRequiredService<ISingletonServiceImplementationTarget>();
            }

            using (var scope = serviceProvider.CreateScope()) {
                Assert.Same(singletonTarget, scope.ServiceProvider.GetRequiredService<ISingletonServiceImplementationTarget>());
            }
        }

        [Fact]
        public void AddAttributeServices_Adds_Services_With_Decorators() {
            services.AddAttributeServices(typeof(ServiceAttributeTests).Assembly, options => options.AddAttributeDecorators());

            var serviceProvider = services.BuildServiceProvider();

            var proxy = serviceProvider.GetRequiredService<ISingletonServiceImplementationTarget>();

            Assert.Equal("Bar", proxy.GetValue());

            Assert.Equal(1, decorator.Calls);
        }

        [Fact]
        public void AddAttributeServices_With_Decorators_Always_Returns_Same_Object() {
            services.AddAttributeServices(typeof(ServiceAttributeTests).Assembly, options => options.AddAttributeDecorators());

            var serviceProvider = services.BuildServiceProvider();
            ISingletonServiceImplementationTarget singletonTarget;

            using (var scope = serviceProvider.CreateScope()) {
                singletonTarget = scope.ServiceProvider.GetRequiredService<ISingletonServiceImplementationTarget>();
            }

            using (var scope = serviceProvider.CreateScope()) {
                Assert.Same(singletonTarget, scope.ServiceProvider.GetRequiredService<ISingletonServiceImplementationTarget>());
            }
        }
    }
}
