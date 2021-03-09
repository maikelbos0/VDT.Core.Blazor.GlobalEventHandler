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
        public void AddAttributeServices_Scoped_Returns_Same_Object_Within_Same_Scope() {
            services.AddAttributeServices(typeof(ServiceCollectionExtensionsTests).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope()) {
                Assert.Same(scope.ServiceProvider.GetRequiredService<IScopedServiceTarget>(), scope.ServiceProvider.GetRequiredService<IScopedServiceTarget>());
            }
        }

        [Fact]
        public void AddAttributeServices_Scoped_Returns_New_Object_Within_Different_Scopes() {
            services.AddAttributeServices(typeof(ServiceCollectionExtensionsTests).Assembly);

            var serviceProvider = services.BuildServiceProvider();
            IScopedServiceTarget scopedTarget;

            using (var scope = serviceProvider.CreateScope()) {
                scopedTarget = scope.ServiceProvider.GetRequiredService<IScopedServiceTarget>();
            }

            using (var scope = serviceProvider.CreateScope()) {
                Assert.NotSame(scopedTarget, scope.ServiceProvider.GetRequiredService<IScopedServiceTarget>());
            }
        }

        [Fact]
        public void AddAttributeServices_Adds_Scoped_Services_With_Decorators() {
            services.AddAttributeServices(typeof(ServiceCollectionExtensionsTests).Assembly, options => options.AddAttributeDecorators());

            var serviceProvider = services.BuildServiceProvider();

            var proxy = serviceProvider.GetRequiredService<IScopedServiceTarget>();

            Assert.Equal("Bar", proxy.GetValue());

            Assert.Equal(1, decorator.Calls);
        }

        [Fact]
        public void AddAttributeServices_Scoped_With_Decorators_Returns_Same_Object_Within_Same_Scope() {
            services.AddAttributeServices(typeof(ServiceCollectionExtensionsTests).Assembly, options => options.AddAttributeDecorators());

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope()) {
                Assert.Same(scope.ServiceProvider.GetRequiredService<IScopedServiceTarget>(), scope.ServiceProvider.GetRequiredService<IScopedServiceTarget>());
            }
        }

        [Fact]
        public void AddAttributeServices_Scoped_With_Decorators_Returns_New_Object_Within_Different_Scopes() {
            services.AddAttributeServices(typeof(ServiceCollectionExtensionsTests).Assembly, options => options.AddAttributeDecorators());

            var serviceProvider = services.BuildServiceProvider();
            IScopedServiceTarget scopedTarget;

            using (var scope = serviceProvider.CreateScope()) {
                scopedTarget = scope.ServiceProvider.GetRequiredService<IScopedServiceTarget>();
            }

            using (var scope = serviceProvider.CreateScope()) {
                Assert.NotSame(scopedTarget, scope.ServiceProvider.GetRequiredService<IScopedServiceTarget>());
            }
        }
    }
}
