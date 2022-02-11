using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class AssemblyExtensionsTests {
        [Fact]
        public void GetAssemblies_Finds_All_Assemblies() {
            var assembly = typeof(AssemblyExtensionsTests).Assembly;

            var foundAssemblies = assembly.GetAssemblies(a => true, a => true);

            Assert.Contains(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection.Tests");
            Assert.Contains(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection");
            Assert.Contains(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection.Tests.Targets");
            Assert.Contains(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection.Tests.Targets.References");
        }

        [Fact]
        public void GetAssemblies_Finds_Assemblies_Once() {
            var assembly = typeof(AssemblyExtensionsTests).Assembly;

            var foundAssemblies = assembly.GetAssemblies(a => true, a => true);

            Assert.Single(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection");
        }

        [Fact]
        public void GetAssemblies_Uses_FilterPredicate() {
            var assembly = typeof(AssemblyExtensionsTests).Assembly;

            var foundAssemblies = assembly.GetAssemblies(a => a.GetName().Name != "VDT.Core.DependencyInjection.Tests.Targets", a => true);

            Assert.Contains(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection.Tests");
            Assert.Contains(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection");
            Assert.DoesNotContain(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection.Tests.Targets");
            Assert.Contains(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection.Tests.Targets.References");
        }

        [Fact]
        public void GetAssemblies_Uses_ScanPredicate() {
            var assembly = typeof(AssemblyExtensionsTests).Assembly;

            var foundAssemblies = assembly.GetAssemblies(a => true, a => a.Name != "VDT.Core.DependencyInjection.Tests.Targets");

            Assert.Contains(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection.Tests");
            Assert.Contains(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection");
            Assert.DoesNotContain(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection.Tests.Targets");
            Assert.DoesNotContain(foundAssemblies, a => a.GetName().Name == "VDT.Core.DependencyInjection.Tests.Targets.References");
        }
    }
}
