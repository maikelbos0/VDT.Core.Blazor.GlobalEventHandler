using System;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public class SyncWithoutReturnValueTarget {
        public virtual void Success() {
        }

        public virtual void Error() {
            throw new InvalidOperationException("Error class called");
        }

        public virtual void VerifyContext<TFoo>(TFoo foo, string bar) {
        }
    }
}
