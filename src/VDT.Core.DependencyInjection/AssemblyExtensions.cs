using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VDT.Core.DependencyInjection {
    internal static class AssemblyExtensions {
        public static IEnumerable<Assembly> GetAssemblies(this Assembly rootAssembly, Predicate<Assembly> filterPredicate, Predicate<AssemblyName> scanPredicate) {
            var referencedAssemblies = new HashSet<Assembly>() {
                rootAssembly 
            };
            var assembliesToScan = new List<Assembly>() {
                rootAssembly            
            };

            for (var i = 0; i < assembliesToScan.Count; i++) {
                var newAssemblies = assembliesToScan[i]
                    .GetReferencedAssemblies()
                    .Where(a => scanPredicate(a))
                    .Select(Assembly.Load)
                    .Where(a => !referencedAssemblies.Contains(a));

                assembliesToScan.AddRange(newAssemblies);
                referencedAssemblies.UnionWith(newAssemblies);
            }

            return referencedAssemblies
                .Where(a => filterPredicate(a))
                .ToList();
        }
    }
}
