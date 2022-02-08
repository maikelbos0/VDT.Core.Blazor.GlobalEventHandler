using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class AssemblyExtensionsTests {
        [Fact]
        public void GetAssemblies_Finds_All_Assemblies() {
            var assembly = typeof(AssemblyExtensionsTests).Assembly;

            var foundAssemblies = assembly.GetAssemblies(a => true, a => false);

            Assert.Contains(assembly, foundAssemblies);
            Assert.Contains(typeof(ServiceRegistrationOptions).Assembly, foundAssemblies);
            Assert.Contains(typeof(FactAttribute).Assembly, foundAssemblies);
            Assert.Contains(typeof(string).Assembly, foundAssemblies);
            Assert.Contains(typeof(System.IO.File).Assembly, foundAssemblies);
        }

        [Fact]
        public void GetAssemblies_Finds_Assemblies_Once() {
            var assembly = typeof(AssemblyExtensionsTests).Assembly;

            var foundAssemblies = assembly.GetAssemblies(a => true, a => false);

            Assert.Single(foundAssemblies, a => a == typeof(Castle.Core.ProxyServices).Assembly);
        }
    }
}
