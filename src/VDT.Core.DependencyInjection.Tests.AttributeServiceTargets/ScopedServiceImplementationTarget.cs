using VDT.Core.DependencyInjection.Tests.Decorators.Targets;

namespace VDT.Core.DependencyInjection.Tests.AttributeServiceTargets {
    [ScopedServiceImplementation(typeof(IScopedServiceImplementationTarget))]
    public class ScopedServiceImplementationTarget : IScopedServiceImplementationTarget {
        [TestDecorator]
        public string GetValue() {
            return "Bar";
        }
    }
}
