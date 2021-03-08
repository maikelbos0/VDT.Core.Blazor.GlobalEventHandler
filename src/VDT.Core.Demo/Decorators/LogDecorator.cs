using System.Diagnostics;
using VDT.Core.DependencyInjection.Decorators;

namespace VDT.Core.Demo.Decorators {
    public class LogDecorator : IDecorator {
        public void BeforeExecute(MethodExecutionContext context) {
            Debug.WriteLine($"Executing '{context.TargetType.FullName}.{context.Method.Name}'");
        }

        public void AfterExecute(MethodExecutionContext context) {
            Debug.WriteLine($"Executed '{context.TargetType.FullName}.{context.Method.Name}'");
        }

        public void OnError(MethodExecutionContext context, System.Exception exception) {
            Debug.WriteLine($"Failed to execute '{context.TargetType.FullName}.{context.Method.Name}': {exception.Message}");
        }
    }
}
