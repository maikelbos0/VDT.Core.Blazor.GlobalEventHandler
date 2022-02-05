using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Attributes;
using VDT.Core.DependencyInjection.Tests.AttributeServiceTargets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Attributes {
    public class ScopedServiceAttributeTests {
        [Fact]
        public void ScopedServiceAttribute_ServiceLifetime_Is_Scoped() {
            Assert.Equal(ServiceLifetime.Scoped, new ScopedServiceAttribute(typeof(AttributeServiceTarget)).ServiceLifetime);
        }
    }
}
