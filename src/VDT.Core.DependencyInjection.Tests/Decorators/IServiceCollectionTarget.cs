namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public interface IServiceCollectionTarget {
        string Value { get; set; }

        string GetValue();
    }
}