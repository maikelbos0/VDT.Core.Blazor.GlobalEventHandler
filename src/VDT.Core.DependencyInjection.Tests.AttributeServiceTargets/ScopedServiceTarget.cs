namespace VDT.Core.DependencyInjection.Tests.AttributeServiceTargets {
    public class ScopedServiceTarget : ScopedServiceTargetBase, IScopedServiceTarget {
        public ScopedServiceTarget() : base("Bar") { }

        public override string GetValue() {
            return value;
        }
    }
}
