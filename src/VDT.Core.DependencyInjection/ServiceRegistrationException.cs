using System;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Represents errors that occur during service registration
    /// </summary>
    public class ServiceRegistrationException : Exception {
        internal ServiceRegistrationException(string message) : base(message) {
        }
    }
}