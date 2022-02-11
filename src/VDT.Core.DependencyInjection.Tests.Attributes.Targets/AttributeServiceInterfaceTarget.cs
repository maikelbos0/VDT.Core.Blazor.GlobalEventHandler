namespace VDT.Core.DependencyInjection.Tests.Attributes.Targets {
    public class AttributeServiceInterfaceTarget : IAttributeServiceInterfaceTarget {
        public string GetValue() {
            return "Bar";
        }
    }
}
