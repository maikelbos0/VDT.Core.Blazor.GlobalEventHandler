using System;
using System.Threading.Tasks;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public class SyncWithReturnValueDecoratorInterceptorTests : DecoratorInterceptorTests<SyncWithReturnValueTarget> {
        public override Task Success(SyncWithReturnValueTarget target) {
            Assert.True(target.Success());

            return Task.CompletedTask;
        }

        public override Task Error(SyncWithReturnValueTarget target) {
            Assert.Throws<InvalidOperationException>(() => target.Error());

            return Task.CompletedTask;
        }

        public override Task VerifyContext(SyncWithReturnValueTarget target) {
            Assert.True(target.VerifyContext(42, "Foo"));

            return Task.CompletedTask;
        }
    }
}
