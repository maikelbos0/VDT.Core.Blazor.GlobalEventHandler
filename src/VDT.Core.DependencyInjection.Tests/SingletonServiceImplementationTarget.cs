using VDT.Core.DependencyInjection.Tests.Decorators;

namespace VDT.Core.DependencyInjection.Tests {
    [SingletonServiceImplementation(typeof(ISingletonServiceImplementationTarget))]
    public class SingletonServiceImplementationTarget : ISingletonServiceImplementationTarget {
        [TestDecorator]
        public string GetValue() {
            return "Bar";
        }
    }
}
