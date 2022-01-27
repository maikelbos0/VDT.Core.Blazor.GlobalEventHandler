namespace VDT.Core.DependencyInjection.Tests.AttributeServiceTargets {
    public class SingletonServiceTarget : SingletonServiceTargetBase, ISingletonServiceTarget {
        public SingletonServiceTarget() : base("Bar") { }

        public override string GetValue() {
            return value;
        }
    }
}
