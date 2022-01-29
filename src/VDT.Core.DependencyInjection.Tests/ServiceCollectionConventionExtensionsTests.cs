using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using VDT.Core.DependencyInjection.Tests.ConventionServiceTargets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class ServiceCollectionConventionExtensionsTests {
        protected readonly ServiceCollection services = new ServiceCollection();

        [Fact]
        public void AddTransientServices_Always_Returns_New_Object() {
            services.AddTransientServices(typeof(SomeService).Assembly, t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface)));

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope()) {
                Assert.NotSame(scope.ServiceProvider.GetRequiredService<ISomeService>(), scope.ServiceProvider.GetRequiredService<ISomeService>());
            }
        }

        [Fact]
        public void AddScopedServices_Returns_Same_Object_Within_Same_Scope() {
            services.AddScopedServices(typeof(SomeService).Assembly, t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface)));

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope()) {
                Assert.Same(scope.ServiceProvider.GetRequiredService<ISomeService>(), scope.ServiceProvider.GetRequiredService<ISomeService>());
            }
        }

        [Fact]
        public void AddScopedServices_Returns_New_Object_Within_Different_Scopes() {
            services.AddScopedServices(typeof(SomeService).Assembly, t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface)));

            var serviceProvider = services.BuildServiceProvider();
            ISomeService scopedTarget;

            using (var scope = serviceProvider.CreateScope()) {
                scopedTarget = scope.ServiceProvider.GetRequiredService<ISomeService>();
            }

            using (var scope = serviceProvider.CreateScope()) {
                Assert.NotSame(scopedTarget, scope.ServiceProvider.GetRequiredService<ISomeService>());
            }
        }

        [Fact]
        public void AddSingletonServices_Always_Returns_Same_Object() {
            services.AddSingletonServices(typeof(SomeService).Assembly, t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface)));

            var serviceProvider = services.BuildServiceProvider();
            ISomeService singletonTarget;

            using (var scope = serviceProvider.CreateScope()) {
                singletonTarget = scope.ServiceProvider.GetRequiredService<ISomeService>();
            }

            using (var scope = serviceProvider.CreateScope()) {
                Assert.Same(singletonTarget, scope.ServiceProvider.GetRequiredService<ISomeService>());
            }
        }

        [Fact]
        public void AddServices_Adds_Registrations_For_Found_Services_Of_A_Type() {
            services.AddServices(typeof(SomeService).Assembly, t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface)), (serviceType, implementationType) => ServiceLifetime.Scoped);

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<ISomeService>();

            Assert.IsType<SomeService>(service);
        }

        [Fact]
        public void AddServices_Does_Not_Add_Registrations_For_Other_Service_Types_Of_A_Type() {
            services.AddServices(typeof(SomeService).Assembly, t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface)), (serviceType, implementationType) => ServiceLifetime.Scoped);

            var serviceProvider = services.BuildServiceProvider();

            Assert.Throws<InvalidOperationException>(() => serviceProvider.GetRequiredService<IGenericInterface>());
        }
    }
}
