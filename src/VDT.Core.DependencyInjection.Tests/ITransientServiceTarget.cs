using VDT.Core.DependencyInjection.Tests.Decorators.Targets;

namespace VDT.Core.DependencyInjection.Tests {
    [TransientService(typeof(TransientServiceTarget))]
    public interface ITransientServiceTarget {
        [TestDecorator]
        public string GetValue();
    }
}
