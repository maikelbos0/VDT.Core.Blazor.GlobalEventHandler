using VDT.Core.DependencyInjection.Attributes;
using VDT.Core.DependencyInjection.Tests.Decorators.Targets;

namespace VDT.Core.DependencyInjection.Tests.AttributeServiceTargets {
    [SingletonService(typeof(AttributeServiceBaseClassTarget))]
    public abstract class AttributeServiceBaseClassTargetBase {
        [TestDecorator]
        public abstract string GetValue();
    }
}
