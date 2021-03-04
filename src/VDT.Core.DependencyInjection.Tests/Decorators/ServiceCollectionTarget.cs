using System.Threading.Tasks;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public class ServiceCollectionTarget : ServiceCollectionTargetBase {
        public override string Value { get; set; } = "Bar";

        public override async Task<string> GetValue() {
            await Task.Delay(1);

            return Value;
        }
    }
}
