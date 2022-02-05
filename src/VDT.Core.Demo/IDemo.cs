using VDT.Core.Demo.Decorators;
using VDT.Core.DependencyInjection.Attributes;

namespace VDT.Core.Demo {
    [ScopedService(typeof(Demo))]
    public interface IDemo {
        [Log]
        string Execute();
    }
}