using VDT.Core.Demo.Decorators;
using VDT.Core.DependencyInjection;

namespace VDT.Core.Demo {
    [ScopedService(typeof(Demo))]
    public interface IDemo {
        [Log]
        string Execute();
    }
}