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
        public void AddAttributeServices_Adds_Transient_Services() {
            services.AddAttributeServices(typeof(ServiceCollectionExtensionsTests).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<ITransientServiceTarget>();

            Assert.IsType<TransientServiceTarget>(service);
        }

        [Fact]
        public void AddAttributeServices_Transient_Always_Returns_New_Object() {
            services.AddAttributeServices(typeof(ServiceCollectionExtensionsTests).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope()) {
                Assert.NotSame(scope.ServiceProvider.GetRequiredService<ITransientServiceTarget>(), scope.ServiceProvider.GetRequiredService<ITransientServiceTarget>());
            }
        }

        [Fact]
        public void AddAttributeServices_Adds_Transient_Services_With_Decorators() {
            services.AddAttributeServices(typeof(ServiceCollectionExtensionsTests).Assembly, options => options.AddAttributeDecorators());

            var serviceProvider = services.BuildServiceProvider();

            var proxy = serviceProvider.GetRequiredService<ITransientServiceTarget>();

            Assert.Equal("Bar", proxy.GetValue());

            Assert.Equal(1, decorator.Calls);
        }

        [Fact]
        public void AddAttributeServices_Transient_With_Decorators_Always_Returns_New_Object() {
            services.AddAttributeServices(typeof(ServiceCollectionExtensionsTests).Assembly, options => options.AddAttributeDecorators());

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope()) {
                Assert.NotSame(scope.ServiceProvider.GetRequiredService<ITransientServiceTarget>(), scope.ServiceProvider.GetRequiredService<ITransientServiceTarget>());
            }
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

        [Fact]
        public void AddAttributeServices_Adds_Singleton_Services() {
            services.AddAttributeServices(typeof(ServiceCollectionExtensionsTests).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<ISingletonServiceTarget>();

            Assert.IsType<SingletonServiceTarget>(service);
        }

        [Fact]
        public void AddAttributeServices_Singleton_Always_Returns_Same_Object() {
            services.AddAttributeServices(typeof(ServiceCollectionExtensionsTests).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            Assert.Same(serviceProvider.GetRequiredService<ISingletonServiceTarget>(), serviceProvider.GetRequiredService<ISingletonServiceTarget>());
        }

        [Fact]
        public void AddAttributeServices_Adds_Singleton_Services_With_Decorators() {
            services.AddAttributeServices(typeof(ServiceCollectionExtensionsTests).Assembly, options => options.AddAttributeDecorators());

            var serviceProvider = services.BuildServiceProvider();

            var proxy = serviceProvider.GetRequiredService<ISingletonServiceTarget>();

            Assert.Equal("Bar", proxy.GetValue());

            Assert.Equal(1, decorator.Calls);
        }

        [Fact]
        public void AddAttributeServices_Singleton_With_Decorators_Always_Returns_Same_Object() {
            services.AddAttributeServices(typeof(ServiceCollectionExtensionsTests).Assembly, options => options.AddAttributeDecorators());

            var serviceProvider = services.BuildServiceProvider();

            Assert.Same(serviceProvider.GetRequiredService<ISingletonServiceTarget>(), serviceProvider.GetRequiredService<ISingletonServiceTarget>());
        }
    }
}
