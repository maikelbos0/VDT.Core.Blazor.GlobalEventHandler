using System.Reflection;
using VDT.Core.DependencyInjection.Decorators;
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

            Assert.Equal(binding.ServiceMethod, typeof(IDecoratorBindingTarget).GetMethod(nameof(IDecoratorBindingTarget.Method), 0, BindingFlags.Public | BindingFlags.Instance));
        }

        [Fact]
        public void ServiceMethod_Resolves_Correctly_To_Interface() {
            var binding = new DecoratorBinding(
                typeof(DecoratorBindingTarget).GetMethod(nameof(DecoratorBindingTarget.Method), 0, BindingFlags.Public | BindingFlags.Instance),
                typeof(IDecoratorBindingTarget),
                null!
            );

            Assert.Equal(binding.ServiceMethod, typeof(IDecoratorBindingTarget).GetMethod(nameof(IDecoratorBindingTarget.Method), 0, BindingFlags.Public | BindingFlags.Instance));
        }

        [Fact]
        public void ServiceMethod_Resolves_Correctly_To_Abstract_Base_Class() {
            var binding = new DecoratorBinding(
                typeof(DecoratorBindingTarget).GetMethod(nameof(DecoratorBindingTarget.Method), 0, BindingFlags.Public | BindingFlags.Instance),
                typeof(DecoratorBindingTargetAbstractBase),
                null!
            );

            Assert.Equal(binding.ServiceMethod, typeof(DecoratorBindingTargetAbstractBase).GetMethod(nameof(DecoratorBindingTargetAbstractBase.Method), 0, BindingFlags.Public | BindingFlags.Instance));
        }

        [Fact]
        public void ServiceMethod_Resolves_Correctly_To_Base_Class() {
            var binding = new DecoratorBinding(
                typeof(DecoratorBindingTarget).GetMethod(nameof(DecoratorBindingTarget.Method), 0, BindingFlags.Public | BindingFlags.Instance),
                typeof(DecoratorBindingTargetBase),
                null!
            );

            Assert.Equal(binding.ServiceMethod, typeof(DecoratorBindingTargetBase).GetMethod(nameof(DecoratorBindingTargetBase.Method), 0, BindingFlags.Public | BindingFlags.Instance));
        }
    }

    public interface IDecoratorBindingTarget {
        void Method();
    }

    public abstract class DecoratorBindingTargetAbstractBase {
        public abstract void Method();
    }

    public class DecoratorBindingTargetBase : DecoratorBindingTargetAbstractBase {
        public override void Method() { }
    }

    public class DecoratorBindingTarget : DecoratorBindingTargetBase, IDecoratorBindingTarget {
        public override void Method() { }
    }
}
