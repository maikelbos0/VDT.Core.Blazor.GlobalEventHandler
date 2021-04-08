namespace VDT.Core.DependencyInjection.Tests {
    [TransientServiceImplementation(typeof(ITransientServiceImplementationTarget))]
    public class TransientServiceImplementationTarget : ITransientServiceImplementationTarget {
        public string GetValue() {
            return "Bar";
        }
    }
}
