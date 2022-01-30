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

        [Fact]
        public void CreateGenericInterfaceTypeFinder_Returns_ServiceTypeFinder_That_Returns_Correct_Constructed_Generic_Service_Types() {
            var finder = DefaultServiceTypeFinders.CreateGenericInterfaceTypeFinder(typeof(ICommandHandler<>));

            Assert.Equal(typeof(ICommandHandler<string>), Assert.Single(finder(typeof(StringCommandHandler))));
        }

        [Fact]
        public void CreateGenericInterfaceTypeFinder_Throws_Exception_When_Not_Passing_Unbound_Generic_Type() {
            Assert.Throws<ServiceRegistrationException>(() => DefaultServiceTypeFinders.CreateGenericInterfaceTypeFinder(typeof(IGenericInterface)));
        }

        [Fact]
        public void CreateGenericInterfaceTypeFinder_Throws_Exception_When_Not_Passing_Interface_Types() {
            Assert.Throws<ServiceRegistrationException>(() => DefaultServiceTypeFinders.CreateGenericInterfaceTypeFinder(typeof(CommandHandler<>)));
        }
    }
}
