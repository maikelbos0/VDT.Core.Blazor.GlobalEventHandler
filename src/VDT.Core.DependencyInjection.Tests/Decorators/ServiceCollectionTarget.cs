namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public class ServiceCollectionTarget {
        public string Value { get; set; } = "Bar";

        public virtual string GetValue() {
            return Value;
        }
    }
}
