using System;

namespace VDT.Core.DependencyInjection {
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public sealed class ScopedServiceAttribute : Attribute {
        public Type Target { get; }

        public ScopedServiceAttribute(Type target) {
            Target = target;
        }
    }
}
