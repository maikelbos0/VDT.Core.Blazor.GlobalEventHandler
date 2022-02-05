using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Attributes;
using VDT.Core.DependencyInjection.Tests.AttributeServiceTargets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Attributes {
    public class SingleTonServiceAttributeTests {
        [Fact]
        public void SingletonServiceAttribute_ServiceLifetime_Is_Singleton() {
            Assert.Equal(ServiceLifetime.Singleton, new SingletonServiceAttribute(typeof(AttributeServiceTarget)).ServiceLifetime);
        }
    }
}
