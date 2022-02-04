using VDT.Core.DependencyInjection.Tests.Decorators.Targets;

namespace VDT.Core.DependencyInjection.Tests.AttributeServiceTargets {
    [SingletonService(typeof(AttributeServiceTarget))]
    public abstract class AttributeServiceTargetBase {
        protected readonly string value;

        protected AttributeServiceTargetBase(string value) {
            this.value = value;
        }

        [TestDecorator]
        public abstract string GetValue();
    }
}
