using VDT.Core.DependencyInjection.Tests.Decorators.Targets;

namespace VDT.Core.DependencyInjection.Tests {
    [ScopedService(typeof(ScopedServiceTarget))]
    public interface IScopedServiceTarget {
        [TestDecorator]
        public string GetValue();
    }
}
