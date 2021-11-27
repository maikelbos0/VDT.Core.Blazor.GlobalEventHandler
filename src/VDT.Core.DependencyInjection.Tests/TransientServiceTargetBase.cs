using VDT.Core.DependencyInjection.Tests.Decorators;

namespace VDT.Core.DependencyInjection.Tests {
    [TransientService(typeof(TransientServiceTarget))]
    public abstract class TransientServiceTargetBase {
        protected readonly string value;

        protected TransientServiceTargetBase(string value) {
            this.value = value;
        }

        [TestDecorator]
        public abstract string GetValue();
    }
}