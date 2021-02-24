using System;

namespace VDT.Core.DependencyInjection.Decorators {
    public interface IDecorator {
        void BeforeExecute(MethodExecutionContext context) { }
        void AfterExecute(MethodExecutionContext context) { }
        void OnError(MethodExecutionContext context, Exception exception) { }
    }
}
