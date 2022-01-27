using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using VDT.Core.DependencyInjection.Tests.ConventionServiceTargets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class ServiceCollectionConventionExtensionsTests {
        protected readonly ServiceCollection services = new ServiceCollection();

        [Fact]
        public void AddServices_Adds_Registrations_For_Found_Services_Of_A_Type() {
            services.AddTransientServices(typeof(SomeService).Assembly, t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface)));

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<ISomeService>();

            Assert.IsType<SomeService>(service);
        }

        [Fact]
        public void AddServices_Does_Not_Add_Registrations_For_Other_Service_Types_Of_A_Type() {
            services.AddTransientServices(typeof(SomeService).Assembly, t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface)));

            var serviceProvider = services.BuildServiceProvider();

            Assert.Throws<InvalidOperationException>(() => serviceProvider.GetRequiredService<IGenericInterface>());
        }

        [Fact]
        public void AddServices_Always_Returns_New_Object() {
            services.AddTransientServices(typeof(SomeService).Assembly, t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface)));

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope()) {
                Assert.NotSame(scope.ServiceProvider.GetRequiredService<ISomeService>(), scope.ServiceProvider.GetRequiredService<ISomeService>());
            }
        }
    }
}
