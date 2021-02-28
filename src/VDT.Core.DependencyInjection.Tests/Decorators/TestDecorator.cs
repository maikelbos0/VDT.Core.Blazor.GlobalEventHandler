using VDT.Core.DependencyInjection.Decorators;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public class TestDecorator : IDecorator {
        public int Calls { get; private set; }

        public void BeforeExecute(MethodExecutionContext context) {
            Calls++;
        }
    }
}
