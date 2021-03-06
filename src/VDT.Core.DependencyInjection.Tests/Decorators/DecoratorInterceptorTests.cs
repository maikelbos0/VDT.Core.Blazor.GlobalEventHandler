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
        private bool shouldIntercept = true;

        public DecoratorInterceptorTests() {
            decorator = Substitute.For<IDecorator>();
            interceptor = new DecoratorInterceptor(decorator, method => shouldIntercept);
            target = new TTarget();
            proxy = new ProxyGenerator().CreateClassProxyWithTarget(target, interceptor);
        }

        public abstract Task Success(TTarget target);
        public abstract Task Error(TTarget target);
        public abstract Task VerifyContext(TTarget target);

        [Fact]
        public async Task BeforeExecute_Is_Called() {
            await Success(proxy);

            decorator.Received().BeforeExecute(Arg.Any<MethodExecutionContext>());
        }

        [Fact]
        public async Task BeforeExecute_MethodExecutionContext_Is_Correct() {
            MethodExecutionContext context = null!;
            decorator.BeforeExecute(Arg.Do<MethodExecutionContext>(c => context = c));

            await VerifyContext(proxy);

            Assert.NotNull(context);
            Assert.Equal(typeof(TTarget), context.TargetType);
            Assert.Equal(target, context.Target);
            Assert.Equal(typeof(TTarget), context.Method.DeclaringType);
            Assert.Equal(new object[] { 42, "Foo" }, context.Arguments);
            Assert.Equal(new[] { typeof(int) }, context.GenericArguments);
        }

        [Fact]
        public async Task BeforeExecute_Is_Not_Called_When_Predicate_Is_False() {
            shouldIntercept = false;

            await Success(proxy);

            decorator.DidNotReceiveWithAnyArgs().BeforeExecute(Arg.Any<MethodExecutionContext>());
        }

        [Fact]
        public async Task AfterExecute_Is_Called() {
            await Success(proxy);

            decorator.Received().AfterExecute(Arg.Any<MethodExecutionContext>());
        }

        [Fact]
        public async Task AfterExecute_MethodExecutionContext_Is_Correct() {
            MethodExecutionContext context = null!;
            decorator.AfterExecute(Arg.Do<MethodExecutionContext>(c => context = c));

            await VerifyContext(proxy);

            Assert.NotNull(context);
            Assert.Equal(typeof(TTarget), context.TargetType);
            Assert.Equal(target, context.Target);
            Assert.Equal(typeof(TTarget), context.Method.DeclaringType);
            Assert.Equal(new object[] { 42, "Foo" }, context.Arguments);
            Assert.Equal(new[] { typeof(int) }, context.GenericArguments);
        }

        [Fact]
        public async Task AfterExecute_Is_Not_Called_When_Predicate_Is_False() {
            shouldIntercept = false;

            await Success(proxy);

            decorator.DidNotReceiveWithAnyArgs().AfterExecute(Arg.Any<MethodExecutionContext>());
        }

        [Fact]
        public async Task OnError_Is_Called() {
            await Error(proxy);

            decorator.Received().OnError(Arg.Any<MethodExecutionContext>(), Arg.Any<InvalidOperationException>());
        }

        [Fact]
        public async Task OnError_Is_Not_Called_When_Predicate_Is_False() {
            shouldIntercept = false;

            await Error(proxy);

            decorator.DidNotReceiveWithAnyArgs().OnError(Arg.Any<MethodExecutionContext>(), Arg.Any<Exception>());
        }
    }

    /*
     * TODO
     * Add XML comments
     * Add attribute-based registration
     */
}
