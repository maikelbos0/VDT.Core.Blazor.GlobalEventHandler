using System;
using System.Threading.Tasks;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public sealed class SyncWithoutReturnValueDecoratorInterceptorTests : DecoratorInterceptorTests<SyncWithoutReturnValueTarget> {
        public override Task Success(SyncWithoutReturnValueTarget target) {
            target.Success();

            return Task.CompletedTask;
        }

        public override Task Error(SyncWithoutReturnValueTarget target) {
            Assert.Throws<InvalidOperationException>(() => target.Error());

            return Task.CompletedTask;
        }

        public override Task VerifyContext(SyncWithoutReturnValueTarget target) {
            target.VerifyContext(42, "Foo");

            return Task.CompletedTask;
        }
    }
}
