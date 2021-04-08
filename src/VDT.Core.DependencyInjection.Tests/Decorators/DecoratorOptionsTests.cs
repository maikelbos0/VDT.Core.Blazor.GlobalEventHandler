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

            var policy = Assert.Single(options.Policies);
            Assert.True(policy.Predicate(decoratedMethod));
            Assert.True(policy.Predicate(undecoratedMethod));
        }

        [Fact]
        public void AddDecorator_With_MethodInfo_Works() {
            options.AddDecorator<TestDecorator>(decoratedMethod);

            var policy = Assert.Single(options.Policies);
            Assert.True(policy.Predicate(decoratedMethod));
            Assert.False(policy.Predicate(undecoratedMethod));
        }

        [Fact]
        public void AddDecorator_With_Predicate_Works() {
            options.AddDecorator<TestDecorator>(m => m == decoratedMethod);

            var policy = Assert.Single(options.Policies);
            Assert.True(policy.Predicate(decoratedMethod));
            Assert.False(policy.Predicate(undecoratedMethod));
        }

        [Fact]
        public void AddAttributeDecorators_Works() {
            options.AddAttributeDecorators();

            var policy = Assert.Single(options.Policies);
            Assert.True(policy.Predicate(decoratedMethod));
            Assert.False(policy.Predicate(undecoratedMethod));
        }
    }
}
