namespace VDT.Core.DependencyInjection.Tests {
    public class ScopedServiceTarget : ScopedServiceTargetBase, IScopedServiceTarget {
        public override string GetValue() {
            return "Bar";
        }
    }
}
