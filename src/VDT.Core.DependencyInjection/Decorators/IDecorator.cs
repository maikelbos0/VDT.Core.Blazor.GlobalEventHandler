using System;

namespace VDT.Core.DependencyInjection.Decorators {
    /// <summary>
    /// Provides a mechanism to decorate a method
    /// </summary>
    public interface IDecorator {
        /// <summary>
        /// Executes before the decorated method starts execution
        /// </summary>
        /// <param name="context">Contextual information about the method being called</param>
        void BeforeExecute(MethodExecutionContext context) { }

        /// <summary>
        /// Executes after the decorated method completes execution
        /// </summary>
        /// <param name="context">Contextual information about the method being called</param>
        void AfterExecute(MethodExecutionContext context) { }

        /// <summary>
        /// Executes when the decorated method throws an exception
        /// </summary>
        /// <param name="context">Contextual information about the method being called</param>
        /// <param name="exception">Exception thrown that caused this method to be called</param>
        void OnError(MethodExecutionContext context, Exception exception) { }
    }
}
