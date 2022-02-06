using System.Reflection;
using VDT.Core.DependencyInjection.Decorators;
using VDT.Core.DependencyInjection.Tests.Decorators.Targets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public class DecoratorBindingTests {
        [Fact]
        public void ServiceMethod_Resolves_Correctly_To_Self() {
            var binding = new DecoratorBinding(
                typeof(IDecoratorBindingTarget).GetMethod(nameof(IDecoratorBindingTarget.Method), 0, BindingFlags.Public | BindingFlags.Instance),
                typeof(IDecoratorBindingTarget), 
                null!
            );

            Assert.Equal(binding.GetServiceMethod(), typeof(IDecoratorBindingTarget).GetMethod(nameof(IDecoratorBindingTarget.Method), 0, BindingFlags.Public | BindingFlags.Instance));
        }

        [Fact]
        public void ServiceMethod_Resolves_Correctly_To_Interface() {
            var binding = new DecoratorBinding(
                typeof(DecoratorBindingTarget).GetMethod(nameof(DecoratorBindingTarget.Method), 0, BindingFlags.Public | BindingFlags.Instance),
                typeof(IDecoratorBindingTarget),
                null!
            );

            Assert.Equal(binding.GetServiceMethod(), typeof(IDecoratorBindingTarget).GetMethod(nameof(IDecoratorBindingTarget.Method), 0, BindingFlags.Public | BindingFlags.Instance));
        }

        [Fact]
        public void ServiceMethod_Resolves_Correctly_To_Null_When_Missing_On_Interface() {
            var binding = new DecoratorBinding(
                typeof(DecoratorBindingTarget).GetMethod(nameof(DecoratorBindingTarget.ImplementationMethod), 0, BindingFlags.Public | BindingFlags.Instance),
                typeof(IDecoratorBindingTarget),
                null!
            );

            Assert.Null(binding.GetServiceMethod());
        }

        [Fact]
        public void ServiceMethod_Resolves_Correctly_To_Abstract_Base_Class() {
            var binding = new DecoratorBinding(
                typeof(DecoratorBindingTarget).GetMethod(nameof(DecoratorBindingTarget.Method), 0, BindingFlags.Public | BindingFlags.Instance),
                typeof(DecoratorBindingTargetAbstractBase),
                null!
            );

            Assert.Equal(binding.GetServiceMethod(), typeof(DecoratorBindingTargetAbstractBase).GetMethod(nameof(DecoratorBindingTargetAbstractBase.Method), 0, BindingFlags.Public | BindingFlags.Instance));
        }

        [Fact]
        public void ServiceMethod_Resolves_Correctly_To_Null_When_Missing_On_Abstract_Base_Class() {
            var binding = new DecoratorBinding(
                typeof(DecoratorBindingTarget).GetMethod(nameof(DecoratorBindingTarget.ImplementationMethod), 0, BindingFlags.Public | BindingFlags.Instance),
                typeof(DecoratorBindingTargetAbstractBase),
                null!
            );

            Assert.Null(binding.GetServiceMethod());
        }

        [Fact]
        public void ServiceMethod_Resolves_Correctly_To_Base_Class() {
            var binding = new DecoratorBinding(
                typeof(DecoratorBindingTarget).GetMethod(nameof(DecoratorBindingTarget.Method), 0, BindingFlags.Public | BindingFlags.Instance),
                typeof(DecoratorBindingTargetBase),
                null!
            );

            Assert.Equal(binding.GetServiceMethod(), typeof(DecoratorBindingTargetBase).GetMethod(nameof(DecoratorBindingTargetBase.Method), 0, BindingFlags.Public | BindingFlags.Instance));
        }

        [Fact]
        public void ServiceMethod_Resolves_Correctly_To_Null_When_Missing_On_Base_Class() {
            var binding = new DecoratorBinding(
                typeof(DecoratorBindingTarget).GetMethod(nameof(DecoratorBindingTarget.ImplementationMethod), 0, BindingFlags.Public | BindingFlags.Instance),
                typeof(DecoratorBindingTargetBase),
                null!
            );

            Assert.Null(binding.GetServiceMethod());
        }
    }
}
