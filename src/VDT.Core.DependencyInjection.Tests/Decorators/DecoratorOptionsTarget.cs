namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public class DecoratorOptionsTarget : DecoratorOptionsTargetBase, IDecoratorOptionsTarget {
        [TestDecorator]
        public override void ImplementationDecorated() {
        }

        public override void ServiceDecorated() {
        }

        public override void Undecorated() {
        }
    }
}
