namespace VDT.Core.DependencyInjection.Tests.Decorators {
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
