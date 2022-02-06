using VDT.Core.DependencyInjection.Attributes;

namespace VDT.Core.DependencyInjection.Tests.AttributeServiceTargets {
    [SingletonServiceImplementation(typeof(IAttributeServiceImplementationTarget))]
    public class AttributeServiceImplementationTarget : IAttributeServiceImplementationTarget {
        public string GetValue() {
            return "Bar";
        }
    }
}
