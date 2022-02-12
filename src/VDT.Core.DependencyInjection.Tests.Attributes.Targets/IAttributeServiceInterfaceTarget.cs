using VDT.Core.DependencyInjection.Attributes;

namespace VDT.Core.DependencyInjection.Tests.Attributes.Targets {
    [SingletonService(typeof(AttributeServiceInterfaceTarget))]
    public interface IAttributeServiceInterfaceTarget {
        public string GetValue();
    }
}
