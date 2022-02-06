using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Attributes;
using VDT.Core.DependencyInjection.Tests.AttributeServiceTargets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Attributes {
    public class ScopedServiceImplementationAttributeTests {
        [Fact]
        public void ScopedServiceImplementationAttribute_ServiceLifetime_Is_Scoped() {
            Assert.Equal(ServiceLifetime.Scoped, new ScopedServiceImplementationAttribute(typeof(IAttributeServiceImplementationTarget)).ServiceLifetime);
        }
    }
}
