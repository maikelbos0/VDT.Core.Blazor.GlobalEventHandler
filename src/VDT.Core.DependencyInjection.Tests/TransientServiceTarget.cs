namespace VDT.Core.DependencyInjection.Tests {
    public class TransientServiceTarget : ITransientServiceTarget {
        public string GetValue() {
            return "Bar";
        }
    }
}
