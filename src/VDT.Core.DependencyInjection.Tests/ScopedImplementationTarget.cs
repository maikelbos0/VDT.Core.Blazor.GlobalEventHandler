namespace VDT.Core.DependencyInjection.Tests {
    [ScopedServiceImplementation(typeof(IScopedImplementationTarget))]
    public class ScopedImplementationTarget : IScopedImplementationTarget {
        public string GetValue() {
            return "Bar";
        }
    }
}
