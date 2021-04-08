using VDT.Core.DependencyInjection.Tests.Decorators;

namespace VDT.Core.DependencyInjection.Tests {
    [ScopedServiceImplementation(typeof(IScopedServiceImplementationTarget))]
    public class ScopedServiceImplementationTarget : IScopedServiceImplementationTarget {
        [TestDecorator]
        public string GetValue() {
            return "Bar";
        }
    }
}
