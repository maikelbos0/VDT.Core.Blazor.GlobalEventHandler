using System;

namespace VDT.Core.DependencyInjection {
    internal class ServiceAttributeContext {
        internal Type Type { get; }
        internal ServiceAttribute Attribute { get; }

        public ServiceAttributeContext(Type type, ServiceAttribute attribute) {
            Type = type;
            Attribute = attribute;
        }
    }
 }
