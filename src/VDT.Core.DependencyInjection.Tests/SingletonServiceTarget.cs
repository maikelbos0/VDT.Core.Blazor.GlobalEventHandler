namespace VDT.Core.DependencyInjection.Tests {
    public class SingletonServiceTarget : ISingletonServiceTarget {
        public string GetValue() {
            return "Bar";
        }
    }
}
