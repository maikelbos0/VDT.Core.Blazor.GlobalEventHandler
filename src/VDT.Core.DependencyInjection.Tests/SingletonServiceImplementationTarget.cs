namespace VDT.Core.DependencyInjection.Tests {
    [SingletonServiceImplementation(typeof(ISingletonServiceImplementationTarget))]
    public class SingletonServiceImplementationTarget : ISingletonServiceImplementationTarget {
        public string GetValue() {
            return "Bar";
        }
    }
}
