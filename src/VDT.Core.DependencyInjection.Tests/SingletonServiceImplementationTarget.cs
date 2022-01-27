using VDT.Core.DependencyInjection.Tests.Decorators.Targets;

namespace VDT.Core.DependencyInjection.Tests {
    [SingletonServiceImplementation(typeof(ISingletonServiceImplementationTarget))]
    public class SingletonServiceImplementationTarget : ISingletonServiceImplementationTarget {
        [TestDecorator]
        public string GetValue() {
            return "Bar";
        }
    }
}
