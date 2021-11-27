using VDT.Core.DependencyInjection.Tests.Decorators;

namespace VDT.Core.DependencyInjection.Tests {
    [SingletonService(typeof(SingletonServiceTarget))]
    public abstract class SingletonServiceTargetBase {
        protected readonly string value;

        protected SingletonServiceTargetBase(string value) {
            this.value = value;
        }

        [TestDecorator]
        public abstract string GetValue();
    }
}