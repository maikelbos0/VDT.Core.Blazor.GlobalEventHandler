using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Attributes;
using VDT.Core.DependencyInjection.Tests.Attributes.Targets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Attributes {
    public class TransientServiceImplementationAttributeTests {
        [Fact]
        public void TransientServiceImplementationAttribute_ServiceLifetime_Is_Transient() {
            Assert.Equal(ServiceLifetime.Transient, new TransientServiceImplementationAttribute(typeof(IAttributeServiceImplementationTarget)).ServiceLifetime);
        }
    }
}
