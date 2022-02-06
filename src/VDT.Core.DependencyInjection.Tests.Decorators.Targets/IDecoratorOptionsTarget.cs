namespace VDT.Core.DependencyInjection.Tests.Decorators.Targets {
    public interface IDecoratorOptionsTarget {
        [TestDecorator]
        void ServiceDecorated();

        void ImplementationDecorated();

        void Undecorated();
    }
}