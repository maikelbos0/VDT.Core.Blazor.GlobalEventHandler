using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Attributes;
using VDT.Core.DependencyInjection.Tests.Attributes.Targets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Attributes {
    public class TransientServiceAttributeTests {
        [Fact]
        public void TransientServiceAttribute_ServiceLifetime_Is_Transient() {
            Assert.Equal(ServiceLifetime.Transient, new TransientServiceAttribute(typeof(AttributeServiceInterfaceTarget)).ServiceLifetime);
        }
    }
}
