namespace VDT.Core.DependencyInjection.Tests {
    public class SingletonServiceTarget : SingletonServiceTargetBase, ISingletonServiceTarget {
        public SingletonServiceTarget() : base("Bar") { }

        public override string GetValue() {
            return value;
        }
    }
}
