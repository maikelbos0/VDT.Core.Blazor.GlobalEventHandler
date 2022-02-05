namespace VDT.Core.DependencyInjection.Tests.AttributeServiceTargets {
    public class AttributeServiceInterfaceTarget : IAttributeServiceInterfaceTarget {
        public string GetValue() {
            return "Bar";
        }
    }
}
