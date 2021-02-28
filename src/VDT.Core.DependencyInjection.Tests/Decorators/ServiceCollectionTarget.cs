namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public class ServiceCollectionTarget : IServiceCollectionTarget {
        public string Value { get; set; } = "Bar";

        public virtual string GetValue() {
            return Value;
        }
    }
}
