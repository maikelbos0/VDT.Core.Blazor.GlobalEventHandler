using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Tests.AttributeServiceTargets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class TransientServiceAttributeTests {
        [Fact]
        public void TransientServiceAttribute_ServiceLifetime_Is_Transient() {
            Assert.Equal(ServiceLifetime.Transient, new TransientServiceAttribute(typeof(AttributeServiceTarget)).ServiceLifetime);
        }
    }
}
