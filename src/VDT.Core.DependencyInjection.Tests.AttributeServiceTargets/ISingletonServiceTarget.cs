using VDT.Core.DependencyInjection.Tests.Decorators.Targets;

namespace VDT.Core.DependencyInjection.Tests.AttributeServiceTargets {
    [SingletonService(typeof(SingletonServiceTarget))]
    public interface ISingletonServiceTarget {
        [TestDecorator]
        public string GetValue();
    }
}
