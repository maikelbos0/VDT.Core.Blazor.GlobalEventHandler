using System;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public class SyncWithReturnValueTarget {
        public virtual bool Success() {
            return true;
        }

        public virtual bool Error() {
            throw new InvalidOperationException("Error class called");
        }

        public virtual bool VerifyContext<TFoo>(TFoo foo, string bar) {
            return true;
        }
    }
}
