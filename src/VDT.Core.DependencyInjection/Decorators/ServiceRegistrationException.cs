using System;

namespace VDT.Core.DependencyInjection.Decorators {
    public class ServiceRegistrationException : Exception {
        public ServiceRegistrationException(string message) : base(message) {
        }
    }
}