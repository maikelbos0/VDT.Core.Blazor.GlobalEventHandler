using VDT.Core.DependencyInjection.Tests.Decorators;

namespace VDT.Core.DependencyInjection.Tests {
    public interface ISingletonServiceImplementationTarget {
        [TestDecorator]
        public string GetValue();
    }
}
