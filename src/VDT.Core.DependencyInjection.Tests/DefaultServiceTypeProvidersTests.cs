using VDT.Core.DependencyInjection.Tests.Targets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class DefaultServiceTypeProvidersTests {
        [Fact]
        public void SingleInterface_Returns_Single_Interface_If_Found() {
            var serviceTypes = DefaultServiceTypeProviders.SingleInterface(typeof(DefaultSingleInterfaceService));

            Assert.Equal(typeof(ISingleInterfaceService), Assert.Single(serviceTypes));
        }

        [Fact]
        public void SingleInterface_Returns_No_Services_For_No_Interfaces() {
            var serviceTypes = DefaultServiceTypeProviders.SingleInterface(typeof(ImplementationOnlyService));

            Assert.Empty(serviceTypes);
        }

        [Fact]
        public void SingleInterface_Returns_No_Services_For_Multiple_Interfaces() {
            var serviceTypes = DefaultServiceTypeProviders.SingleInterface(typeof(NamedService));

            Assert.Empty(serviceTypes);
        }

        [Fact]
        public void InterfaceByName_Returns_Interfaces_By_Name() {
            var serviceTypes = DefaultServiceTypeProviders.InterfaceByName(typeof(NamedService));

            Assert.Equal(typeof(INamedService), Assert.Single(serviceTypes));
        }

        [Fact]
        public void InterfaceByName_Returns_No_Services_For_No_Correctly_Named_Interfaces() {
            var serviceTypes = DefaultServiceTypeProviders.InterfaceByName(typeof(DefaultSingleInterfaceService));

            Assert.Empty(serviceTypes);
        }

        [Fact]
        public void CreateGenericInterfaceTypeProvider_Returns_ServiceTypeProvider_That_Returns_Correct_Constructed_Generic_Service_Types() {
            var provider = DefaultServiceTypeProviders.CreateGenericInterfaceTypeProvider(typeof(ICommandHandler<>));

            Assert.Equal(typeof(ICommandHandler<string>), Assert.Single(provider(typeof(StringCommandHandler))));
        }

        [Fact]
        public void CreateGenericInterfaceTypeProvider_Returns_ServiceTypeProvider_That_Returns_No_Services_For_Not_Generic_Interface() {
            var provider = DefaultServiceTypeProviders.CreateGenericInterfaceTypeProvider(typeof(ICommandHandler<>));

            Assert.Empty(provider(typeof(NamedService)));
        }

        [Fact]
        public void CreateGenericInterfaceTypeProvider_Throws_Exception_When_Not_Passing_Unbound_Generic_Type() {
            Assert.Throws<ServiceRegistrationException>(() => DefaultServiceTypeProviders.CreateGenericInterfaceTypeProvider(typeof(IGenericInterface)));
        }

        [Fact]
        public void CreateGenericInterfaceTypeProvider_Throws_Exception_When_Not_Passing_Interface_Types() {
            Assert.Throws<ServiceRegistrationException>(() => DefaultServiceTypeProviders.CreateGenericInterfaceTypeProvider(typeof(CommandHandler<>)));
        }
    }
}
