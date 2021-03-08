using System;
using VDT.Core.DependencyInjection.Decorators;

namespace VDT.Core.Demo.Decorators {
    [AttributeUsage(AttributeTargets.Method)]
    public class LogAttribute : Attribute, IDecorateAttribute<LogDecorator> {
    }
}
