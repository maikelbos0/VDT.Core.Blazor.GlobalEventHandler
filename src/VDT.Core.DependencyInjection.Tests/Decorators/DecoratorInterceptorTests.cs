using Castle.DynamicProxy;
using NSubstitute;
using System;
using System.Threading.Tasks;
using VDT.Core.DependencyInjection.Decorators;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public abstract class DecoratorInterceptorTests<TTarget> where TTarget : class, new() {
        private readonly IDecorator decorator;
        private readonly DecoratorInterceptor interceptor;
        private readonly TTarget target;
        private readonly TTarget proxy;

        public DecoratorInterceptorTests() {
            decorator = Substitute.For<IDecorator>();
            interceptor = new DecoratorInterceptor(decorator, method => true);
            target = new TTarget();
            proxy = new ProxyGenerator().CreateClassProxyWithTarget(target, interceptor);
        }

        public abstract Task Success(TTarget target);
        public abstract Task Error(TTarget target);
        public abstract Task VerifyContext(TTarget target);

        [Fact]
        public async Task BeforeExecute_Is_Called() {
            MethodExecutionContext context = null;

            decorator.BeforeExecute(Arg.Do<MethodExecutionContext>(ctx => context = ctx));

            await Success(proxy);

            decorator.Received().BeforeExecute(Arg.Any<MethodExecutionContext>());
        }

        [Fact]
        public void BeforeExecute_MethodExecutionContext_Is_Correct() {
            MethodExecutionContext context = null;
            decorator.BeforeExecute(Arg.Do<MethodExecutionContext>(c => context = c));

            VerifyContext(proxy);

            Assert.Equal(typeof(TTarget), context.TargetType);
            Assert.Equal(target, context.Target);
            Assert.Equal(typeof(TTarget), context.Method.DeclaringType);
            Assert.Equal(new object[] { 42, "Foo" }, context.Arguments);
            Assert.Equal(new[] { typeof(int) }, context.GenericArguments);
        }

        [Fact]
        public async Task AfterExecute_Is_Called() {
            await Success(proxy);

            decorator.Received().AfterExecute(Arg.Any<MethodExecutionContext>());
        }

        [Fact]
        public void AfterExecute_MethodExecutionContext_Is_Correct() {
            MethodExecutionContext context = null;
            decorator.AfterExecute(Arg.Do<MethodExecutionContext>(c => context = c));

            VerifyContext(proxy);

            Assert.Equal(typeof(TTarget), context.TargetType);
            Assert.Equal(target, context.Target);
            Assert.Equal(typeof(TTarget), context.Method.DeclaringType);
            Assert.Equal(new object[] { 42, "Foo" }, context.Arguments);
            Assert.Equal(new[] { typeof(int) }, context.GenericArguments);
        }

        [Fact]
        public async Task OnError_Is_Called() {
            await Error(proxy);

            decorator.Received().OnError(Arg.Any<MethodExecutionContext>(), Arg.Any<InvalidOperationException>());
        }
    }








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





    public sealed class DecoratorInterceptorTests {
        private readonly IDecorator decorator;
        private readonly DecoratorInterceptor interceptor;
        private readonly TestTarget target;
        private readonly TestTarget proxy;

        public class TestTarget {
            public virtual bool ReturnSuccess() {
                return true;
            }

            public virtual async Task SuccessAsync() {
                await Task.Delay(200);
            }

            public virtual async Task<bool> ReturnSuccessAsync() {
                await Task.Delay(200);
                return true;
            }

            public virtual void Error() {
                throw new InvalidOperationException("Error class called");
            }

            public virtual async Task ErrorAsync() {
                await Task.Delay(200);
                throw new InvalidOperationException("Error class called");
            }

            public virtual void TestContext<TFoo>(TFoo foo, string bar) {
            }
        }

        public DecoratorInterceptorTests() {
            decorator = Substitute.For<IDecorator>();
            interceptor = new DecoratorInterceptor(decorator, method => true);
            target = new TestTarget();
            proxy = new ProxyGenerator().CreateClassProxyWithTarget(target, interceptor);
        }

        [Fact]
        public void BeforeExecute_Is_Called() {
            Assert.True(proxy.ReturnSuccess());

            decorator.Received().BeforeExecute(Arg.Any<MethodExecutionContext>());
        }

        [Fact]
        public void AfterExecute_Is_Called() {
            Assert.True(proxy.ReturnSuccess());

            decorator.Received().AfterExecute(Arg.Any<MethodExecutionContext>());
        }

        [Fact]
        public void OnError_Is_Called() {
            Assert.Throws<InvalidOperationException>(proxy.Error);

            decorator.Received().OnError(Arg.Any<MethodExecutionContext>(), Arg.Any<InvalidOperationException>());
        }

        [Fact]
        public async Task BeforeExecute_Is_Called_Async() {
            await proxy.SuccessAsync();

            decorator.Received().BeforeExecute(Arg.Any<MethodExecutionContext>());
        }

        [Fact]
        public async Task AfterExecute_Is_Called_Async() {
            var task = proxy.SuccessAsync();

            decorator.DidNotReceiveWithAnyArgs().AfterExecute(default);

            await task;

            decorator.Received().AfterExecute(Arg.Any<MethodExecutionContext>());
        }

        [Fact]
        public async Task BeforeExecute_Is_Called_Async_With_Return_Value() {
            Assert.True(await proxy.ReturnSuccessAsync());

            decorator.Received().BeforeExecute(Arg.Any<MethodExecutionContext>());
        }

        [Fact]
        public async Task AfterExecute_Is_Called_Async_With_Return_Value() {
            var task = proxy.ReturnSuccessAsync();

            decorator.DidNotReceiveWithAnyArgs().AfterExecute(default);

            Assert.True(await task);

            decorator.Received().AfterExecute(Arg.Any<MethodExecutionContext>());
        }

        [Fact]
        public async Task OnError_Is_Called_Async() {
            await Assert.ThrowsAsync<InvalidOperationException>(proxy.ErrorAsync);

            decorator.Received().OnError(Arg.Any<MethodExecutionContext>(), Arg.Any<InvalidOperationException>());
        }

        [Fact]
        public void MethodExecutionContext_Is_Correct() {
            MethodExecutionContext context = null;
            decorator.BeforeExecute(Arg.Do<MethodExecutionContext>(c => context = c));

            proxy.TestContext(15, "Bar");

            Assert.Equal(typeof(TestTarget), context.TargetType);
            Assert.Equal(target, context.Target);
            Assert.Equal(typeof(TestTarget).GetMethod(nameof(TestTarget.TestContext)).MakeGenericMethod(typeof(int)), context.Method);
            Assert.Equal(new object[] { 15, "Bar" }, context.Arguments);
            Assert.Equal(new[] { typeof(int) }, context.GenericArguments);
        }

        [Fact]
        public void Dont_Decorate_When_Predicate_Is_False() {
            var interceptor = new DecoratorInterceptor(decorator, method => false);
            var proxy = new ProxyGenerator().CreateClassProxyWithTarget(target, interceptor);

            proxy.ReturnSuccess();

            decorator.DidNotReceiveWithAnyArgs().BeforeExecute(default);
        }

        /*
         * TODO
         * Test multiple decorators in async especially
         * Test ServiceCollectionExtensions
         * Test DecoratorOptions (check different overloads of should be called)
         */
    }
}
