using System;
using VDT.Core.DependencyInjection.Decorators;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    [AttributeUsage(AttributeTargets.Method)]
    public class TestDecoratorAttribute : Attribute, IDecorateAttribute<TestDecorator> { }
}
