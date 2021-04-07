using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class ScopedServiceImplementationAttributeTests : ServiceAttributeTests {
        [Fact]
        public void AddAttributeServices_Adds_Services() {
            services.AddAttributeServices(typeof(ServiceAttributeTests).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<IScopedServiceImplementationTarget>();

            Assert.IsType<ScopedServiceImplementationTarget>(service);
        }

        [Fact]
        public void AddAttributeServices_Returns_Same_Object_Within_Same_Scope() {
            services.AddAttributeServices(typeof(ServiceAttributeTests).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope()) {
                Assert.Same(scope.ServiceProvider.GetRequiredService<IScopedServiceImplementationTarget>(), scope.ServiceProvider.GetRequiredService<IScopedServiceImplementationTarget>());
            }
        }

        [Fact]
        public void AddAttributeServices_Returns_New_Object_Within_Different_Scopes() {
            services.AddAttributeServices(typeof(ServiceAttributeTests).Assembly);

            var serviceProvider = services.BuildServiceProvider();
            IScopedServiceImplementationTarget scopedTarget;

            using (var scope = serviceProvider.CreateScope()) {
                scopedTarget = scope.ServiceProvider.GetRequiredService<IScopedServiceImplementationTarget>();
            }

            using (var scope = serviceProvider.CreateScope()) {
                Assert.NotSame(scopedTarget, scope.ServiceProvider.GetRequiredService<IScopedServiceImplementationTarget>());
            }
        }

        [Fact]
        public void AddAttributeServices_Adds_Services_With_Decorators() {
            services.AddAttributeServices(typeof(ServiceAttributeTests).Assembly, options => options.AddAttributeDecorators());

            var serviceProvider = services.BuildServiceProvider();

            var proxy = serviceProvider.GetRequiredService<IScopedServiceImplementationTarget>();

            Assert.Equal("Bar", proxy.GetValue());

            Assert.Equal(1, decorator.Calls);
        }

        [Fact]
        public void AddAttributeServices_With_Decorators_Returns_Same_Object_Within_Same_Scope() {
            services.AddAttributeServices(typeof(ServiceAttributeTests).Assembly, options => options.AddAttributeDecorators());

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope()) {
                Assert.Same(scope.ServiceProvider.GetRequiredService<IScopedServiceImplementationTarget>(), scope.ServiceProvider.GetRequiredService<IScopedServiceImplementationTarget>());
            }
        }

        [Fact]
        public void AddAttributeServices_With_Decorators_Returns_New_Object_Within_Different_Scopes() {
            services.AddAttributeServices(typeof(ServiceAttributeTests).Assembly, options => options.AddAttributeDecorators());

            var serviceProvider = services.BuildServiceProvider();
            IScopedServiceImplementationTarget scopedTarget;

            using (var scope = serviceProvider.CreateScope()) {
                scopedTarget = scope.ServiceProvider.GetRequiredService<IScopedServiceImplementationTarget>();
            }

            using (var scope = serviceProvider.CreateScope()) {
                Assert.NotSame(scopedTarget, scope.ServiceProvider.GetRequiredService<IScopedServiceImplementationTarget>());
            }
        }
    }
}
