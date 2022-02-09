using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class AssemblyExtensionsTests {
        [Fact]
        public void GetAssemblies_Finds_All_Assemblies() {
            var assembly = typeof(AssemblyExtensionsTests).Assembly;

            var foundAssemblies = assembly.GetAssemblies(a => true, a => false);

            Assert.Contains(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection.Tests");
            Assert.Contains(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection");
            Assert.Contains(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection.Tests.ConventionServiceTargets");
            Assert.Contains(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection.Tests.AssemblyTargets");
        }

        [Fact]
        public void GetAssemblies_Finds_Assemblies_Once() {
            var assembly = typeof(AssemblyExtensionsTests).Assembly;

            var foundAssemblies = assembly.GetAssemblies(a => true, a => false);

            Assert.Single(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection");
        }

        [Fact]
        public void GetAssemblies_Uses_Predicate() {
            var assembly = typeof(AssemblyExtensionsTests).Assembly;

            var foundAssemblies = assembly.GetAssemblies(a => !(a.FullName?.StartsWith("VDT.Core.DependencyInjection.Tests.ConventionServiceTargets") ?? false), a => false);

            Assert.Contains(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection.Tests");
            Assert.Contains(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection");
            Assert.DoesNotContain(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection.Tests.ConventionServiceTargets");
            Assert.Contains(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection.Tests.AssemblyTargets");
        }

        [Fact]
        public void GetAssemblies_Uses_EndpointPredicate() {
            var assembly = typeof(AssemblyExtensionsTests).Assembly;

            var foundAssemblies = assembly.GetAssemblies(a => true, a => a.FullName?.StartsWith("VDT.Core.DependencyInjection.Tests.ConventionServiceTargets") ?? false);

            Assert.Contains(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection.Tests");
            Assert.Contains(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection");
            Assert.DoesNotContain(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection.Tests.ConventionServiceTargets");
            Assert.DoesNotContain(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection.Tests.AssemblyTargets");
        }
    }
}
