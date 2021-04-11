using System;
using System.Reflection;

namespace VDT.Core.DependencyInjection {
    internal static class TypeExtensions {
        internal static MethodInfo GetMethod(this Type type, string name, int genericParameterCount, BindingFlags bindingFlags, params Type[] types) {
            return type.GetMethod(name, genericParameterCount, bindingFlags, null, types, null) ?? throw new InvalidOperationException($"Method '{type.FullName}.{name}' was not found.");
        }
    }
}
