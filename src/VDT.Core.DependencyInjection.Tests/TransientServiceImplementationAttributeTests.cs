using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Tests.AttributeServiceTargets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class TransientServiceImplementationAttributeTests {
        [Fact]
        public void TransientServiceImplementationAttribute_ServiceLifetime_Is_Transient() {
            Assert.Equal(ServiceLifetime.Transient, new TransientServiceImplementationAttribute(typeof(AttributeServiceImplementationTarget)).ServiceLifetime);
        }
    }
}
