using Castle.DynamicProxy;
using NSubstitute;
using System;
using VDT.Core.DependencyInjection.Decorators;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public sealed class DecoratorInterceptorTests {
        private readonly IDecorator decorator;
        private readonly DecoratorInterceptor interceptor;
        private readonly TestTarget target;
        private readonly TestTarget proxy;

        public class TestTarget {
            public virtual void Success() { }
            public virtual void Error() => throw new InvalidOperationException("Error class called");
            public virtual void TestContext<TFoo>(TFoo foo, string bar) { }
        }

        public DecoratorInterceptorTests() {
            decorator = Substitute.For<IDecorator>();
            interceptor = new DecoratorInterceptor(decorator, method => true);
            target = new TestTarget();
            proxy = new ProxyGenerator().CreateClassProxyWithTarget(target, interceptor);
        }

        [Fact]
        public void BeforeExecute_Is_Called() {
            proxy.Success();

            decorator.Received().BeforeExecute(Arg.Any<MethodExecutionContext>());
        }

        [Fact]
        public void AfterExecute_Is_Called() {
            proxy.Success();

            decorator.Received().AfterExecute(Arg.Any<MethodExecutionContext>());
        }

        [Fact]
        public void OnError_Is_Called() {
            Assert.Throws<InvalidOperationException>(proxy.Error);

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

            proxy.Success();

            decorator.DidNotReceiveWithAnyArgs().BeforeExecute(default);
        }

        /*
         * TODO
         * Async tests with and without return types
         * Test ServiceCollectionExtensions
         * Test DecoratorOptions (check different overloads of should be called)
         */
    }
}
