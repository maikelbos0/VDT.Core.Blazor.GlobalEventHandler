using VDT.Core.DependencyInjection.Attributes;

namespace VDT.Core.DependencyInjection.Tests.Attributes.Targets {
    [SingletonService(typeof(AttributeServiceBaseClassTarget))]
    public abstract class AttributeServiceBaseClassTargetBase {
        public abstract string GetValue();
    }
}
