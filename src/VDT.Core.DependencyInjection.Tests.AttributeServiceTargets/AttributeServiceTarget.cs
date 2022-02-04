namespace VDT.Core.DependencyInjection.Tests.AttributeServiceTargets {
    public class AttributeServiceTarget : AttributeServiceTargetBase, IAttributeServiceTarget {
        public AttributeServiceTarget() : base("Bar") { }

        public override string GetValue() {
            return value;
        }
    }
}
