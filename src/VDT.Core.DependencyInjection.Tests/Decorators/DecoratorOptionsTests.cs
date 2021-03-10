using System;
using System.Linq;
using System.Reflection;
using VDT.Core.DependencyInjection.Decorators;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public sealed class DecoratorOptionsTests {
        private readonly DecoratorOptions options = new DecoratorOptions(typeof(DecoratorOptionsTarget));
        private readonly MethodInfo decoratedMethod = typeof(DecoratorOptionsTarget).GetMethod(nameof(DecoratorOptionsTarget.Decorated), 0, BindingFlags.Public | BindingFlags.Instance);
        private readonly MethodInfo undecoratedMethod = typeof(DecoratorOptionsTarget).GetMethod(nameof(DecoratorOptionsTarget.Undecorated), 0, BindingFlags.Public | BindingFlags.Instance);

        [Fact]
        public void AddDecorator_Without_Predicate_Works() {
            options.AddDecorator<TestDecorator>();

            Assert.Single(options.Policies);
            Assert.True(options.Policies.Single().Predicate(decoratedMethod));
            Assert.True(options.Policies.Single().Predicate(undecoratedMethod));
        }

        [Fact]
        public void AddDecorator_With_MethodInfo_Works() {
            options.AddDecorator<TestDecorator>(decoratedMethod);

            Assert.Single(options.Policies);
            Assert.True(options.Policies.Single().Predicate(decoratedMethod));
            Assert.False(options.Policies.Single().Predicate(undecoratedMethod));
        }

        [Fact]
        public void AddDecorator_With_Predicate_Works() {
            options.AddDecorator<TestDecorator>(m => m == decoratedMethod);

            Assert.Single(options.Policies);
            Assert.True(options.Policies.Single().Predicate(decoratedMethod));
            Assert.False(options.Policies.Single().Predicate(undecoratedMethod));
        }

        [Fact]
        public void AddAttributeDecorators_Works() {
            options.AddAttributeDecorators();

            Assert.Single(options.Policies);
            Assert.True(options.Policies.Single().Predicate(decoratedMethod));
            Assert.False(options.Policies.Single().Predicate(undecoratedMethod));
        }
    }
}
