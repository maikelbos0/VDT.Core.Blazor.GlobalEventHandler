using System;
using System.Reflection;

namespace VDT.Core.DependencyInjection.Decorators {
    internal class DecoratorBinding {
        internal MethodInfo Method { get; }
        internal Type ServiceType { get; }
        internal Type DecoratorType { get; }

        internal DecoratorBinding(MethodInfo method, Type serviceType, Type decoratorType) {
            Method = method;
            ServiceType = serviceType;
            DecoratorType = decoratorType;
        }

        internal MethodInfo? GetServiceMethod() {
            if (Method.DeclaringType == ServiceType) {
                return Method;
            }
            else if (ServiceType.IsInterface) {
                var map = Method.DeclaringType?.GetInterfaceMap(ServiceType);

                if (map != null) {
                    var index = Array.IndexOf(map.Value.TargetMethods, Method);

                    if (index > -1) {
                        return map.Value.InterfaceMethods[index];
                    }
                }
            }
            else {
                // base class mapping
            }

            return null;
        }
    }
}
