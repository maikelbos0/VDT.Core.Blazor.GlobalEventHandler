using VDT.Core.DependencyInjection.Attributes;

namespace VDT.Core.DependencyInjection.Tests.AttributeServiceTargets {
    [SingletonService(typeof(AttributeServiceBaseClassTarget))]
    public abstract class AttributeServiceBaseClassTargetBase {
        public abstract string GetValue();
    }
}
