namespace VDT.Core.DependencyInjection.Decorators {
    /// <summary>
    /// Provides a mechanism for indicating with attributes which decorators to use on a method
    /// </summary>
    /// <typeparam name="TDecorator">The type of the decorator to invoke</typeparam>
    public interface IDecorateAttribute<TDecorator> where TDecorator : class, IDecorator { }
}
