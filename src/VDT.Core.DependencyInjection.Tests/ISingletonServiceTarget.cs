using VDT.Core.DependencyInjection.Tests.Decorators;

namespace VDT.Core.DependencyInjection.Tests {
    [SingletonService(typeof(SingletonServiceTarget))]
    public interface ISingletonServiceTarget {
        [TestDecorator]
        public string GetValue();
    }
}
