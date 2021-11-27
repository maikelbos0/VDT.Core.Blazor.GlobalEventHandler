using System;
using VDT.Core.DependencyInjection.Decorators;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class TestDecoratorAttribute : Attribute, IDecorateAttribute<TestDecorator> { }
}
