using System;

namespace VDT.Core.DependencyInjection.Decorators {
    /// <summary>
    /// Represents errors that occur during decorated service registration
    /// </summary>
    public class ServiceRegistrationException : Exception {
        internal ServiceRegistrationException(string message) : base(message) {
        }
    }
}