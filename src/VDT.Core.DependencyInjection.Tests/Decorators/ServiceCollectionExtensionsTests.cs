using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Decorators;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public sealed class ServiceCollectionExtensionsTests {
        private readonly ServiceCollection services;
        private readonly TestDecorator decorator;

        public ServiceCollectionExtensionsTests() {
            services = new ServiceCollection();
            decorator = new TestDecorator();

            services.AddSingleton(decorator);
        }

        [Fact]
        public void AddScoped_Adds_DecoratorInjectors() {
            services.AddScoped<ServiceCollectionTarget, ServiceCollectionTarget>(options => {
                options.AddDecorator<TestDecorator>();
            });

            var serviceProvider = services.BuildServiceProvider();
            var proxy = serviceProvider.GetRequiredService<ServiceCollectionTarget>();

            Assert.Equal("Bar", proxy.GetValue());

            Assert.Equal(1, decorator.Calls);
        }

        [Fact]
        public void AddScoped_With_Factory_Adds_DecoratorInjectors() {
            services.AddScoped<ServiceCollectionTarget, ServiceCollectionTarget>(serviceProvider => new ServiceCollectionTarget {
                Value = "Foo"
            }, options => {
                options.AddDecorator<TestDecorator>();
            });

            var serviceProvider = services.BuildServiceProvider();
            var proxy = serviceProvider.GetRequiredService<ServiceCollectionTarget>();

            Assert.Equal("Foo", proxy.GetValue());

            Assert.Equal(1, decorator.Calls);
        }
    }
}
