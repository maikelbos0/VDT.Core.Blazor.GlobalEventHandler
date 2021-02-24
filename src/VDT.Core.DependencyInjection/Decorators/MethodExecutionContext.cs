using System;
using System.Reflection;

namespace VDT.Core.DependencyInjection.Decorators {
    public class MethodExecutionContext {
        public Type TargetType { get; }
        public object Target { get; }
        public MethodInfo Method { get; }
        public object[] Arguments { get; }
        public Type[] GenericArguments { get; }

        public MethodExecutionContext(Type targetType, object target, MethodInfo method, object[] arguments, Type[] genericArguments) {
            TargetType = targetType;
            Target = target;
            Method = method;
            Arguments = arguments;
            GenericArguments = genericArguments;
        }
    }
}
