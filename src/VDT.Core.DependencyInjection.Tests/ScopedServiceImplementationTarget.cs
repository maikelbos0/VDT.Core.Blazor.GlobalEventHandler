namespace VDT.Core.DependencyInjection.Tests {
    [ScopedServiceImplementation(typeof(IScopedServiceImplementationTarget))]
    public class ScopedServiceImplementationTarget : IScopedServiceImplementationTarget {
        public string GetValue() {
            return "Bar";
        }
    }
}
