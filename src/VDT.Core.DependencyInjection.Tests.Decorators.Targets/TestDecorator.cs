using VDT.Core.DependencyInjection.Decorators;

namespace VDT.Core.DependencyInjection.Tests.Decorators.Targets {
    public class TestDecorator : IDecorator {
        public int Calls { get; private set; }

        public void AfterExecute(MethodExecutionContext context) {
            Calls++;
        }
    }
}
