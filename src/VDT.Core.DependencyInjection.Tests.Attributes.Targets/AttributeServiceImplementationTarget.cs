using VDT.Core.DependencyInjection.Attributes;

namespace VDT.Core.DependencyInjection.Tests.Attributes.Targets {
    [SingletonServiceImplementation(typeof(IAttributeServiceImplementationTarget))]
    public class AttributeServiceImplementationTarget : IAttributeServiceImplementationTarget {
        public string GetValue() {
            return "Bar";
        }
    }
}
