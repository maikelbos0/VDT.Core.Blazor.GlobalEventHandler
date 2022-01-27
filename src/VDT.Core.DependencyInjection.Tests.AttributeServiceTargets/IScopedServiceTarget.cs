using VDT.Core.DependencyInjection.Tests.Decorators.Targets;

namespace VDT.Core.DependencyInjection.Tests.AttributeServiceTargets {
    [ScopedService(typeof(ScopedServiceTarget))]
    public interface IScopedServiceTarget {
        [TestDecorator]
        public string GetValue();
    }
}
