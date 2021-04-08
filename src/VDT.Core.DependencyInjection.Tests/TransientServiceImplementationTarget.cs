using VDT.Core.DependencyInjection.Tests.Decorators;

namespace VDT.Core.DependencyInjection.Tests {
    [TransientServiceImplementation(typeof(ITransientServiceImplementationTarget))]
    public class TransientServiceImplementationTarget : ITransientServiceImplementationTarget {
        [TestDecorator]
        public string GetValue() {
            return "Bar";
        }
    }
}
