namespace VDT.Core.DependencyInjection.Decorators {
    public interface IDecorator {
        void BeforeExecute() { }
        void AfterExecute() { }
        void OnError() { }
    }
}
