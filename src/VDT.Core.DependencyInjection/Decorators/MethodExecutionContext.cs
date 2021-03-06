using System;
using System.Reflection;

namespace VDT.Core.DependencyInjection.Decorators {
    /// <summary>
    /// Contextual information about the method being called that is being decorated
    /// </summary>
    public sealed class MethodExecutionContext {
        /// <summary>
        /// The <see cref="Type" /> of the target object
        /// </summary>
        public Type TargetType { get; }

        /// <summary>
        /// The object on which the target method is invoked
        /// </summary>
        public object Target { get; }

        /// <summary>
        /// The method on the service type that is invoked
        /// </summary>
        public MethodInfo Method { get; }

        /// <summary>
        /// The argument values passed into the method call
        /// </summary>
        public object[] Arguments { get; }

        /// <summary>
        /// The generic argument types with which the method is called
        /// </summary>
        public Type[] GenericArguments { get; }

        internal MethodExecutionContext(Type targetType, object target, MethodInfo method, object[] arguments, Type[] genericArguments) {
            TargetType = targetType;
            Target = target;
            Method = method;
            Arguments = arguments;
            GenericArguments = genericArguments;
        }
    }
}
