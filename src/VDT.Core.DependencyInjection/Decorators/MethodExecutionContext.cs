using System;
using System.Reflection;

namespace VDT.Core.DependencyInjection.Decorators {
    public class MethodExecutionContext {
        public Type TargetType { get; }
        public object Target { get; }
        public MethodInfo Method { get; }
        public object[] Arguments { get; }
        public Type[] GenericArguments { get; }
    }
}
