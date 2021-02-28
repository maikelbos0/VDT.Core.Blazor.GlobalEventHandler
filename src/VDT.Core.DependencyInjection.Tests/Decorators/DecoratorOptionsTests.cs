using System;
using System.Linq;
using System.Reflection;
using VDT.Core.DependencyInjection.Decorators;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public sealed class DecoratorOptionsTests {
        private readonly DecoratorOptions<DecoratorOptionsTarget> options = new DecoratorOptions<DecoratorOptionsTarget>();
        private readonly MethodInfo decoratedMethod = typeof(DecoratorOptionsTarget).GetMethod(nameof(DecoratorOptionsTarget.Decorated)) ?? throw new InvalidOperationException($"Method '{nameof(DecoratorOptionsTarget)}.{nameof(DecoratorOptionsTarget.Decorated)}' was not found.");
        private readonly MethodInfo undecoratedMethod = typeof(DecoratorOptionsTarget).GetMethod(nameof(DecoratorOptionsTarget.Undecorated)) ?? throw new InvalidOperationException($"Method '{nameof(DecoratorOptionsTarget)}.{nameof(DecoratorOptionsTarget.Undecorated)}' was not found.");

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
    }
}
