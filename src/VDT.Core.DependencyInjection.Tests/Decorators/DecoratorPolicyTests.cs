using NSubstitute;
using System;
using VDT.Core.DependencyInjection.Decorators;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public sealed class DecoratorPolicyTests {
        [Fact]
        public void GetDecorator_Works() {
            var policy = new DecoratorPolicy<TestDecorator>(m => true);
            var serviceProvider = Substitute.For<IServiceProvider>();
            var decorator = new TestDecorator();

            serviceProvider.GetService(typeof(TestDecorator)).Returns(decorator);

            Assert.Equal(decorator, policy.GetDecorator(serviceProvider));
        }
    }
}
