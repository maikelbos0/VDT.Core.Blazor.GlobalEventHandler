using System;
using System.Threading.Tasks;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public class AsyncWithoutReturnValueTarget {
        public virtual async Task Success() {
            await Task.Delay(100);
        }

        public virtual async Task Error() {
            await Task.Delay(100);

            throw new InvalidOperationException("Error class called");
        }

        public virtual async Task VerifyContext<TFoo>(TFoo foo, string bar) {
            await Task.Delay(100);
        }
    }
}
