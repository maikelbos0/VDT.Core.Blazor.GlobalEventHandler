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
            services.AddTransientServices(typeof(NamedService).Assembly, t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface)));

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope()) {
                Assert.NotSame(scope.ServiceProvider.GetRequiredService<INamedService>(), scope.ServiceProvider.GetRequiredService<INamedService>());
            }
        }

        [Fact]
        public void AddScopedServices_Returns_Same_Object_Within_Same_Scope() {
            services.AddScopedServices(typeof(NamedService).Assembly, t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface)));

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope()) {
                Assert.Same(scope.ServiceProvider.GetRequiredService<INamedService>(), scope.ServiceProvider.GetRequiredService<INamedService>());
            }
        }

        [Fact]
        public void AddScopedServices_Returns_New_Object_Within_Different_Scopes() {
            services.AddScopedServices(typeof(NamedService).Assembly, t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface)));

            var serviceProvider = services.BuildServiceProvider();
            INamedService scopedTarget;

            using (var scope = serviceProvider.CreateScope()) {
                scopedTarget = scope.ServiceProvider.GetRequiredService<INamedService>();
            }

            using (var scope = serviceProvider.CreateScope()) {
                Assert.NotSame(scopedTarget, scope.ServiceProvider.GetRequiredService<INamedService>());
            }
        }

        [Fact]
        public void AddSingletonServices_Always_Returns_Same_Object() {
            services.AddSingletonServices(typeof(NamedService).Assembly, t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface)));

            var serviceProvider = services.BuildServiceProvider();
            INamedService singletonTarget;

            using (var scope = serviceProvider.CreateScope()) {
                singletonTarget = scope.ServiceProvider.GetRequiredService<INamedService>();
            }

            using (var scope = serviceProvider.CreateScope()) {
                Assert.Same(singletonTarget, scope.ServiceProvider.GetRequiredService<INamedService>());
            }
        }

        [Fact]
        public void AddServices_Adds_Registrations_For_Found_Services_Of_A_Type() {
            services.AddServices(typeof(NamedService).Assembly, t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface)), (serviceType, implementationType) => ServiceLifetime.Scoped);

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<INamedService>();

            Assert.IsType<NamedService>(service);
        }

        [Fact]
        public void AddServices_Does_Not_Add_Registrations_For_Other_Service_Types_Of_A_Type() {
            services.AddServices(typeof(NamedService).Assembly, t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface)), (serviceType, implementationType) => ServiceLifetime.Scoped);

            var serviceProvider = services.BuildServiceProvider();

            Assert.Throws<InvalidOperationException>(() => serviceProvider.GetRequiredService<IGenericInterface>());
        }
    }
}
