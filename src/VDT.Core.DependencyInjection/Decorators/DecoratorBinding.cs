using System;
using System.Reflection;

namespace VDT.Core.DependencyInjection.Decorators {
    internal class DecoratorBinding {
        internal MethodInfo Method { get; }
        internal Type ServiceType { get; }
        internal MethodInfo ServiceMethod {
            get {
                if (Method.DeclaringType == ServiceType) {
                    return Method;
                }
                else if (ServiceType.IsInterface) {
                    // interface mapping
                    return null!;
                }
                else {
                    // base class mapping
                    return null!;
                }
            }
        }
        internal Type DecoratorType { get; }

        internal DecoratorBinding(MethodInfo method, Type serviceType, Type decoratorType) {
            Method = method;
            ServiceType = serviceType;
            DecoratorType = decoratorType;
        }
    }
}
