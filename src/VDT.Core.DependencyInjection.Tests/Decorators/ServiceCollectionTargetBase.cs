using System.Threading.Tasks;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public abstract class ServiceCollectionTargetBase : IServiceCollectionTargetImplementation {
        public abstract string Value { get; set; }

        public abstract Task<string> GetValue();
    }
}   