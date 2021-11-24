using VDT.Core.DependencyInjection.Tests.Decorators;

namespace VDT.Core.DependencyInjection.Tests {
    [ScopedService(typeof(ScopedServiceTarget))]
    public abstract class ScopedServiceTargetBase {
        [TestDecorator]
        public abstract string GetValue();
    }
}