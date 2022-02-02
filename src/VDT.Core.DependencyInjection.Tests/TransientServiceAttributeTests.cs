using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Tests.AttributeServiceTargets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class TransientServiceAttributeTests : ServiceAttributeTests {
        [Fact]
        public void TransientServiceAttribute_ServiceLifetime_Is_Transient() {
            Assert.Equal(ServiceLifetime.Transient, new TransientServiceAttribute(typeof(TransientServiceTarget)).ServiceLifetime);
        }

        [Fact]
        public void AddAttributeServices_Adds_Interface_Services() {
            services.AddAttributeServices(typeof(TransientServiceTarget).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<ITransientServiceTarget>();

            Assert.IsType<TransientServiceTarget>(service);
        }

        [Fact]
        public void AddAttributeServices_Adds_Class_Services() {
            services.AddAttributeServices(typeof(TransientServiceTarget).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<TransientServiceTargetBase>();

            Assert.IsType<TransientServiceTarget>(service);
        }

        [Fact]
        public void AddAttributeServices_Always_Returns_New_Object() {
            services.AddAttributeServices(typeof(TransientServiceTarget).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope()) {
                Assert.NotSame(scope.ServiceProvider.GetRequiredService<ITransientServiceTarget>(), scope.ServiceProvider.GetRequiredService<ITransientServiceTarget>());
            }
        }

        [Fact]
        public void AddAttributeServices_Adds_Interface_Services_With_Decorators() {
            services.AddAttributeServices(typeof(TransientServiceTarget).Assembly, options => options.AddAttributeDecorators());

            var serviceProvider = services.BuildServiceProvider();

            var proxy = serviceProvider.GetRequiredService<ITransientServiceTarget>();

            Assert.Equal("Bar", proxy.GetValue());

            Assert.Equal(1, decorator.Calls);
        }

        [Fact]
        public void AddAttributeServices_Adds_Class_Services_With_Decorators() {
            services.AddAttributeServices(typeof(TransientServiceTarget).Assembly, options => options.AddAttributeDecorators());

            var serviceProvider = services.BuildServiceProvider();

            var proxy = serviceProvider.GetRequiredService<TransientServiceTargetBase>();

            Assert.Equal("Bar", proxy.GetValue());

            Assert.Equal(1, decorator.Calls);
        }

        [Fact]
        public void AddAttributeServices_With_Decorators_Always_Returns_New_Object() {
            services.AddAttributeServices(typeof(TransientServiceTarget).Assembly, options => options.AddAttributeDecorators());

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope()) {
                Assert.NotSame(scope.ServiceProvider.GetRequiredService<ITransientServiceTarget>(), scope.ServiceProvider.GetRequiredService<ITransientServiceTarget>());
            }
        }
    }
}
