using VDT.Core.DependencyInjection.Attributes;

namespace VDT.Core.DependencyInjection.Tests.AttributeServiceTargets {
    [SingletonService(typeof(AttributeServiceInterfaceTarget))]
    public interface IAttributeServiceInterfaceTarget {
        public string GetValue();
    }
}
