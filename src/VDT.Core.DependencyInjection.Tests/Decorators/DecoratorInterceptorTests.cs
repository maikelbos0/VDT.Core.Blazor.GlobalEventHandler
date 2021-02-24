using Castle.DynamicProxy;
using NSubstitute;
using System;
using VDT.Core.DependencyInjection.Decorators;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public sealed class DecoratorInterceptorTests {
        public class Test {
            public virtual void Success() { }
            public virtual void Error() => throw new InvalidOperationException("Error class called");
        }

        [Fact]
        public void BeforeExecute_Is_Called() {
            var decorator = Substitute.For<IDecorator>();
            var interceptor = new DecoratorInterceptor(decorator, method => true);
            var target = new Test();
            var proxy = new ProxyGenerator().CreateClassProxyWithTarget(target, interceptor);

            proxy.Success();

            decorator.Received().BeforeExecute(Arg.Is<MethodExecutionContext>(context => AssertValidMethodExecutionContext(context)));
        }

        [Fact]
        public void AfterExecute_Is_Called() {
            throw new System.NotImplementedException();
        }

        [Fact]
        public void OnError_Is_Called() {
            throw new System.NotImplementedException();
        }

        private bool AssertValidMethodExecutionContext(MethodExecutionContext context) {


            return true;
        }

        /*
         * TODO
         * Check should be called
         * Async tests with and without return types
         * Check if MethodExecutionContext is filled
         * Check if Exception is filled
         * Test ServiceCollectionExtensions
         * Test DecoratorOptions (check different overloads of should be called)
         */
    }
}
