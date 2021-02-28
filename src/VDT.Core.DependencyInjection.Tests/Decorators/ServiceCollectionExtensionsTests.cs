using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Decorators;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public class ServiceCollectionTarget {
        public virtual void Execute() {
        }
    }

    public sealed class ServiceCollectionExtensionsTests {
        [Fact]
        public void AddScoped_Adds_DecoratorInjectors() {
            var services = new ServiceCollection();
            var decorator = new TestDecorator();

            services.AddSingleton(decorator);
            services.AddScoped<ServiceCollectionTarget, ServiceCollectionTarget>(options => {
                options.AddDecorator<TestDecorator>();
            });

            var serviceProvider = services.BuildServiceProvider();
            var proxy = serviceProvider.GetRequiredService<ServiceCollectionTarget>();

            proxy.Execute();

            Assert.Equal(1, decorator.Calls);
        }
    }
}
