namespace VDT.Core.DependencyInjection.Decorators {
    /// <summary>
    /// Provides a mechanism for indicating with attributes which decorators to use on a method
    /// </summary>
    /// <typeparam name="TDecorator"></typeparam>
    public interface IDecorateAttribute<TDecorator> where TDecorator : class, IDecorator { }
}
