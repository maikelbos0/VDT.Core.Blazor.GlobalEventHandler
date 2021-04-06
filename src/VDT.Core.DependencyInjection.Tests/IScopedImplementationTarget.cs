using VDT.Core.DependencyInjection.Tests.Decorators;

namespace VDT.Core.DependencyInjection.Tests {
    public interface IScopedImplementationTarget {
        [TestDecorator]
        public string GetValue();
    }
}
