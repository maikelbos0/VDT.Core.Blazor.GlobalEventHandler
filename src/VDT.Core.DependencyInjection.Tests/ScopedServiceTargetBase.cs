using VDT.Core.DependencyInjection.Tests.Decorators;

namespace VDT.Core.DependencyInjection.Tests {
    [ScopedService(typeof(ScopedServiceTarget))]
    public abstract class ScopedServiceTargetBase {
        protected readonly string value;

        protected ScopedServiceTargetBase(string value) {
            this.value = value;
        }

        [TestDecorator]
        public abstract string GetValue();
    }
}