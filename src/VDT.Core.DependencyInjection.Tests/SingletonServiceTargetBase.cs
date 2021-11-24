using VDT.Core.DependencyInjection.Tests.Decorators;

namespace VDT.Core.DependencyInjection.Tests {
    [SingletonService(typeof(SingletonServiceTarget))]
    public abstract class SingletonServiceTargetBase {
        [TestDecorator]
        public abstract string GetValue();
    }
}