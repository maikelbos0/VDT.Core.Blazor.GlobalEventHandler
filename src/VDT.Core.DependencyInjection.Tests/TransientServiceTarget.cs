namespace VDT.Core.DependencyInjection.Tests {
    public class TransientServiceTarget : TransientServiceTargetBase, ITransientServiceTarget {
        public override string GetValue() {
            return "Bar";
        }
    }
}
