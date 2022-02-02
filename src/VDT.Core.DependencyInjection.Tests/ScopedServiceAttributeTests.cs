using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Tests.AttributeServiceTargets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class ScopedServiceAttributeTests : ServiceAttributeTests {
        [Fact]
        public void ScopedServiceAttribute_ServiceLifetime_Is_Scoped() {
            Assert.Equal(ServiceLifetime.Scoped, new ScopedServiceAttribute(typeof(ScopedServiceTarget)).ServiceLifetime);
        }

        [Fact]
        public void AddAttributeServices_Adds_Interface_Services() {
            services.AddAttributeServices(typeof(ScopedServiceTarget).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<IScopedServiceTarget>();

            Assert.IsType<ScopedServiceTarget>(service);
        }

        [Fact]
        public void AddAttributeServices_Adds_Class_Services() {
            services.AddAttributeServices(typeof(ScopedServiceTarget).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<ScopedServiceTargetBase>();

            Assert.IsType<ScopedServiceTarget>(service);
        }

        [Fact]
        public void AddAttributeServices_Returns_Same_Object_Within_Same_Scope() {
            services.AddAttributeServices(typeof(ScopedServiceTarget).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope()) {
                Assert.Same(scope.ServiceProvider.GetRequiredService<IScopedServiceTarget>(), scope.ServiceProvider.GetRequiredService<IScopedServiceTarget>());
            }
        }

        [Fact]
        public void AddAttributeServices_Returns_New_Object_Within_Different_Scopes() {
            services.AddAttributeServices(typeof(ScopedServiceTarget).Assembly);

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
        public void AddAttributeServices_Adds_Interface_Services_With_Decorators() {
            services.AddAttributeServices(typeof(ScopedServiceTarget).Assembly, options => options.AddAttributeDecorators());

            var serviceProvider = services.BuildServiceProvider();

            var proxy = serviceProvider.GetRequiredService<IScopedServiceTarget>();

            Assert.Equal("Bar", proxy.GetValue());

            Assert.Equal(1, decorator.Calls);
        }
        
        [Fact]
        public void AddAttributeServices_Adds_Class_Services_With_Decorators() {
            services.AddAttributeServices(typeof(ScopedServiceTarget).Assembly, options => options.AddAttributeDecorators());

            var serviceProvider = services.BuildServiceProvider();

            var proxy = serviceProvider.GetRequiredService<ScopedServiceTargetBase>();

            Assert.Equal("Bar", proxy.GetValue());

            Assert.Equal(1, decorator.Calls);
        }

        [Fact]
        public void AddAttributeServices_With_Decorators_Returns_Same_Object_Within_Same_Scope() {
            services.AddAttributeServices(typeof(ScopedServiceTarget).Assembly, options => options.AddAttributeDecorators());

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope()) {
                Assert.Same(scope.ServiceProvider.GetRequiredService<IScopedServiceTarget>(), scope.ServiceProvider.GetRequiredService<IScopedServiceTarget>());
            }
        }

        [Fact]
        public void AddAttributeServices_With_Decorators_Returns_New_Object_Within_Different_Scopes() {
            services.AddAttributeServices(typeof(ScopedServiceTarget).Assembly, options => options.AddAttributeDecorators());

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
