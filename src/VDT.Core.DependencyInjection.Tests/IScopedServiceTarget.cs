using VDT.Core.DependencyInjection.Tests.Decorators;

namespace VDT.Core.DependencyInjection.Tests {
    [ScopedService(typeof(ScopedServiceTarget))]
    public interface IScopedServiceTarget {
        [TestDecorator]
        public string GetValue();
    }
}
