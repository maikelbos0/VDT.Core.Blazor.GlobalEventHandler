using VDT.Core.Demo.Decorators;

namespace VDT.Core.Demo {
    public interface IDemo {
        [Log]
        string Execute();
    }
}