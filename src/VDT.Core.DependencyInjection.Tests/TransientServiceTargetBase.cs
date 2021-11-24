using VDT.Core.DependencyInjection.Tests.Decorators;

namespace VDT.Core.DependencyInjection.Tests {
    [TransientService(typeof(TransientServiceTarget))]
    public abstract class TransientServiceTargetBase {
        [TestDecorator]
        public abstract string GetValue();
    }
}