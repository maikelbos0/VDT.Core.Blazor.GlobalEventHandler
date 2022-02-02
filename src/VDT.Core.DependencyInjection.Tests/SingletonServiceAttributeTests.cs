using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Tests.AttributeServiceTargets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class SingleTonServiceAttributeTests : ServiceAttributeTests {
        [Fact]
        public void SingletonServiceAttribute_ServiceLifetime_Is_Singleton() {
            Assert.Equal(ServiceLifetime.Singleton, new SingletonServiceAttribute(typeof(SingletonServiceTarget)).ServiceLifetime);
        }

        [Fact]
        public void AddAttributeServices_Adds_Interface_Services() {
            services.AddAttributeServices(typeof(SingletonServiceTarget).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<ISingletonServiceTarget>();

            Assert.IsType<SingletonServiceTarget>(service);
        }
        
        [Fact]
        public void AddAttributeServices_Adds_Class_Services() {
            services.AddAttributeServices(typeof(SingletonServiceTarget).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<SingletonServiceTargetBase>();

            Assert.IsType<SingletonServiceTarget>(service);
        }

        [Fact]
        public void AddAttributeServices_Always_Returns_Same_Object() {
            services.AddAttributeServices(typeof(SingletonServiceTarget).Assembly);

            var serviceProvider = services.BuildServiceProvider();
            ISingletonServiceTarget singletonTarget;

            using (var scope = serviceProvider.CreateScope()) {
                singletonTarget = scope.ServiceProvider.GetRequiredService<ISingletonServiceTarget>();
            }

            using (var scope = serviceProvider.CreateScope()) {
                Assert.Same(singletonTarget, scope.ServiceProvider.GetRequiredService<ISingletonServiceTarget>());
            }
        }

        [Fact]
        public void AddAttributeServices_Adds_Interface_Services_With_Decorators() {
            services.AddAttributeServices(typeof(SingletonServiceTarget).Assembly, options => options.AddAttributeDecorators());

            var serviceProvider = services.BuildServiceProvider();

            var proxy = serviceProvider.GetRequiredService<ISingletonServiceTarget>();

            Assert.Equal("Bar", proxy.GetValue());

            Assert.Equal(1, decorator.Calls);
        }

        [Fact]
        public void AddAttributeServices_Adds_Class_Services_With_Decorators() {
            services.AddAttributeServices(typeof(SingletonServiceTarget).Assembly, options => options.AddAttributeDecorators());

            var serviceProvider = services.BuildServiceProvider();

            var proxy = serviceProvider.GetRequiredService<SingletonServiceTargetBase>();

            Assert.Equal("Bar", proxy.GetValue());

            Assert.Equal(1, decorator.Calls);
        }

        [Fact]
        public void AddAttributeServices_With_Decorators_Always_Returns_Same_Object() {
            services.AddAttributeServices(typeof(SingletonServiceTarget).Assembly, options => options.AddAttributeDecorators());

            var serviceProvider = services.BuildServiceProvider();
            ISingletonServiceTarget singletonTarget;

            using (var scope = serviceProvider.CreateScope()) {
                singletonTarget = scope.ServiceProvider.GetRequiredService<ISingletonServiceTarget>();
            }

            using (var scope = serviceProvider.CreateScope()) {
                Assert.Same(singletonTarget, scope.ServiceProvider.GetRequiredService<ISingletonServiceTarget>());
            }
        }
    }
}
