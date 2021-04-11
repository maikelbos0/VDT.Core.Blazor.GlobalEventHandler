namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public class DecoratorOptionsTargetBase {
        public virtual void ImplementationDecorated() {
        }

        [TestDecorator]
        public virtual void ServiceDecorated() {
        }

        public virtual void Undecorated() {
        }
    }
}