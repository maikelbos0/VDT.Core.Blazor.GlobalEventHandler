using VDT.Core.DependencyInjection.Tests.Decorators.Targets;

namespace VDT.Core.DependencyInjection.Tests {
    [TransientServiceImplementation(typeof(ITransientServiceImplementationTarget))]
    public class TransientServiceImplementationTarget : ITransientServiceImplementationTarget {
        [TestDecorator]
        public string GetValue() {
            return "Bar";
        }
    }
}
