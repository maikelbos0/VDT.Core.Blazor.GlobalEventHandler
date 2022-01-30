using VDT.Core.DependencyInjection.Tests.ConventionServiceTargets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class DefaultServiceTypeFindersTests {
        [Fact]
        public void SingleInterface_Returns_Single_Interface_If_Found() {
            var serviceTypes = DefaultServiceTypeFinders.SingleInterface(typeof(DefaultSingleInterfaceService));

            Assert.Equal(typeof(ISingleInterfaceService), Assert.Single(serviceTypes));
        }

        [Fact]
        public void SingleInterface_Returns_No_Services_For_No_Interfaces() {
            var serviceTypes = DefaultServiceTypeFinders.SingleInterface(typeof(ImplementationOnlyService));

            Assert.Empty(serviceTypes);
        }

        [Fact]
        public void SingleInterface_Returns_No_Services_For_Multiple_Interfaces() {
            var serviceTypes = DefaultServiceTypeFinders.SingleInterface(typeof(NamedService));

            Assert.Empty(serviceTypes);
        }

        [Fact]
        public void InterfaceByName_Returns_Interfaces_By_Name() {
            var serviceTypes = DefaultServiceTypeFinders.InterfaceByName(typeof(NamedService));

            Assert.Equal(typeof(INamedService), Assert.Single(serviceTypes));
        }

        [Fact]
        public void InterfaceByName_Returns_No_Services_For_No_Correctly_Named_Interfaces() {
            var serviceTypes = DefaultServiceTypeFinders.InterfaceByName(typeof(DefaultSingleInterfaceService));

            Assert.Empty(serviceTypes);
        }
    }
}
