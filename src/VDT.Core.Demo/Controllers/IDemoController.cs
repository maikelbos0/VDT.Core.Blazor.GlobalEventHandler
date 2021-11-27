using VDT.Core.Demo.Decorators;
using VDT.Core.DependencyInjection;

namespace VDT.Core.Demo.Controllers
{
    [ScopedService(typeof(DemoController))]
    public interface IDemoController {
        [Log]
        string Execute();
    }
}