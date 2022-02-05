using VDT.Core.DependencyInjection.Attributes;
using VDT.Core.DependencyInjection.Tests.Decorators.Targets;

namespace VDT.Core.DependencyInjection.Tests.AttributeServiceTargets {
    [SingletonService(typeof(AttributeServiceInterfaceTarget))]
    public interface IAttributeServiceInterfaceTarget {
        [TestDecorator]
        public string GetValue();
    }
}
