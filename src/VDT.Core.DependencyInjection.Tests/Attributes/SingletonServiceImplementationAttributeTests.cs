using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Attributes;
using VDT.Core.DependencyInjection.Tests.AttributeServiceTargets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Attributes {
    public class SingleTonServiceImplementationAttributeTests {
        [Fact]
        public void SingletonServiceImplementationAttribute_ServiceLifetime_Is_Singleton() {
            Assert.Equal(ServiceLifetime.Singleton, new SingletonServiceImplementationAttribute(typeof(IAttributeServiceImplementationTarget)).ServiceLifetime);
        }
    }
}
