namespace VDT.Core.DependencyInjection.Tests {
    public class SingletonServiceTarget : SingletonServiceTargetBase, ISingletonServiceTarget {
        public override string GetValue() {
            return "Bar";
        }
    }
}
