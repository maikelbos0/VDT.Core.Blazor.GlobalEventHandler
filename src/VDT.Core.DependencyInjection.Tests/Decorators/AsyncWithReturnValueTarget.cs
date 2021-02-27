using System;
using System.Threading.Tasks;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public class AsyncWithReturnValueTarget {
        public virtual async Task<bool> Success() {
            await Task.Delay(100);

            return true;
        }

        public virtual async Task<bool> Error() {
            await Task.Delay(100);

            throw new InvalidOperationException("Error class called");
        }

        public virtual async Task<bool> VerifyContext<TFoo>(TFoo foo, string bar) {
            await Task.Delay(100);

            return true;
        }
    }
}
