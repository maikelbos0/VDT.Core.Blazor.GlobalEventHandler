using VDT.Core.DependencyInjection.Tests.Decorators.Targets;

namespace VDT.Core.DependencyInjection.Tests.AttributeServiceTargets {
    [SingletonService(typeof(AttributeServiceTarget))]
    public interface IAttributeServiceTarget {
        [TestDecorator]
        public string GetValue();
    }
}
