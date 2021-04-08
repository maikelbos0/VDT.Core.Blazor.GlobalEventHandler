namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public interface IDecoratorOptionsTarget {
        [TestDecorator]
        void ServiceDecorated();

        void ImplementationDecorated();

        void Undecorated();
    }
}