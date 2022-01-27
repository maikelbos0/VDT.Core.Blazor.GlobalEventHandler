namespace VDT.Core.DependencyInjection.Tests.Decorators.Targets {
    public class DecoratorOptionsTarget : IDecoratorOptionsTarget {
        [TestDecorator]
        public void ImplementationDecorated() {
        }

        public void ServiceDecorated() {
        }

        public void Undecorated() {
        }
    }
}
