using System;
using System.Linq;
using System.Reflection;

namespace VDT.Core.Events {
    internal static class TypeExtensions {
        internal static MethodInfo GetMethod(this Type type, string name, int genericParameterCount) {
            var methods = type.GetMethods()
                .Where(m => m.Name == name && m.GetGenericArguments().Length == genericParameterCount);

            if (methods.Count() > 1) {
                throw new AmbiguousMatchException("Ambiguous match found.");
            }

            return methods.SingleOrDefault() ?? throw new InvalidOperationException($"Method '{type.FullName}.{name}' was not found.");
        }
    }
}
