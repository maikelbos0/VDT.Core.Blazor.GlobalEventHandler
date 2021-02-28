using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;
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
            services.AddScoped<IServiceCollectionTarget, ServiceCollectionTarget, ServiceCollectionTarget>(options => {
                options.AddDecorator<TestDecorator>();
                options.AddDecorator<TestDecorator>();
            });

            var serviceProvider = services.BuildServiceProvider();
            var proxy = serviceProvider.GetRequiredService<IServiceCollectionTarget>();

            Assert.Equal("Bar", proxy.GetValue());

            Assert.Equal(2, decorator.Calls);
        }

        [Fact]
        public void AddScoped_With_Factory_Adds_DecoratorInjectors() {
            services.AddScoped<IServiceCollectionTarget, ServiceCollectionTarget, ServiceCollectionTarget>(serviceProvider => new ServiceCollectionTarget {
                Value = "Foo"
            }, options => {
                options.AddDecorator<TestDecorator>();
                options.AddDecorator<TestDecorator>();
            });

            var serviceProvider = services.BuildServiceProvider();
            var proxy = serviceProvider.GetRequiredService<IServiceCollectionTarget>();

            Assert.Equal("Foo", proxy.GetValue());

            Assert.Equal(2, decorator.Calls);
        }

        public class Invocation {
            public object? ReturnValue { get; set; }
        }

        [Fact]
        public async Task Test2() {
            var invocation = new Invocation {
                ReturnValue = GetValue()
            };

            var continuationGenerator = typeof(ServiceCollectionExtensionsTests).GetMethod(nameof(Wrap), BindingFlags.NonPublic | BindingFlags.Instance)!;

            continuationGenerator = continuationGenerator.MakeGenericMethod(typeof(string))!;

            continuationGenerator.Invoke(this, new object[] { invocation, invocation.ReturnValue });

            var result = await (Task<string>)invocation.ReturnValue!;

        }

        private void Wrap<TResult>(Invocation invocation, Task<TResult> task) {
            Func<Task<TResult>> continuation = async () => {
                Before();

                await task;

                After();

                return task.Result;
            };

            invocation.ReturnValue = continuation();
        }


        private void Before() {

        }

        private void After() {

        }

        public async Task<string> GetValue() {
            await Task.Delay(100);

            return "Foo";
        }
    }
}
