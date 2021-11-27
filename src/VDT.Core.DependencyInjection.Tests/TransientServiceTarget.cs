namespace VDT.Core.DependencyInjection.Tests {
    public class TransientServiceTarget : TransientServiceTargetBase, ITransientServiceTarget {
        public TransientServiceTarget() : base("Bar") { }

        public override string GetValue() {
            return value;
        }
    }
}
