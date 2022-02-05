using VDT.Core.DependencyInjection.Attributes;
using VDT.Core.DependencyInjection.Tests.Decorators.Targets;

namespace VDT.Core.DependencyInjection.Tests.AttributeServiceTargets {
    [SingletonServiceImplementation(typeof(IAttributeServiceImplementationTarget))]
    public class AttributeServiceImplementationTarget : IAttributeServiceImplementationTarget {
        [TestDecorator]
        public string GetValue() {
            return "Bar";
        }
    }
}
