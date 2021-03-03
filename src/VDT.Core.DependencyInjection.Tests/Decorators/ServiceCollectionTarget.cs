using System.Threading.Tasks;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public class ServiceCollectionTarget : IServiceCollectionTarget {
        public string Value { get; set; } = "Bar";

        public async Task<string> GetValue() {
            await Task.Delay(100);

            return Value;
        }
    }
}
