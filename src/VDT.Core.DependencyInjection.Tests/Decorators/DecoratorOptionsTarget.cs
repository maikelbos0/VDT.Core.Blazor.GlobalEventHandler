namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public class DecoratorOptionsTarget : IDecoratorOptionsTarget {
        [TestDecorator]
        public void ServiceDecorated() {
        }

        public void ImplementationDecorated() {
        }

        public void Undecorated() {
        }
    }
}
