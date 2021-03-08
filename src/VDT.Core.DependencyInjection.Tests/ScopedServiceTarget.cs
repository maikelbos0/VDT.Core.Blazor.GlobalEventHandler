namespace VDT.Core.DependencyInjection.Tests {
    public class ScopedServiceTarget : IScopedServiceTarget {
        public string GetValue() {
            return "Bar";
        }
    }
}
