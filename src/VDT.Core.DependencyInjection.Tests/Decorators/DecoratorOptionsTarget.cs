namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public class DecoratorOptionsTarget : IDecoratorOptionsTarget {
        public void ServiceDecorated() {
        }

        [TestDecorator]
        public void ImplementationDecorated() {
        }

        public void Undecorated() {
        }
    }
}
