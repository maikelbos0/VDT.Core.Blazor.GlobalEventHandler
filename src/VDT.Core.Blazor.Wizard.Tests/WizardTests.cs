using Microsoft.AspNetCore.Components;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace VDT.Core.Blazor.Wizard.Tests {
    public class WizardTests {
        /*
         * TODO
         * Stop
         * AddStep
         * GetSteps
         * GoToPreviousStep
         * TryCompleteStep
         * BuildRenderTree?
         * ActiveStep: index check, bounds check
         * IsFirstStepActive
         * IsLastStepActive
         */

        [Fact]
        public async Task Wizard_Start_Works() {
            WizardStartedEventArgs? arguments = null;
            var wizard = new Wizard() {
#pragma warning disable BL0005 // Component parameter should not be set outside of its component.
                OnStart = EventCallback.Factory.Create<WizardStartedEventArgs>(this, args => arguments = args)
#pragma warning restore BL0005 // Component parameter should not be set outside of its component.
            };
            
            await wizard.Start();

            Assert.Equal(0, typeof(Wizard).GetField("activeStepIndex", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(wizard));
            Assert.NotNull(arguments);
        }

        [Fact]
        public async Task Wizard_Start_Does_Nothing_When_Active() {
            WizardStartedEventArgs? arguments = null;
            var wizard = new Wizard() {
#pragma warning disable BL0005 // Component parameter should not be set outside of its component.
                OnStart = EventCallback.Factory.Create<WizardStartedEventArgs>(this, args => arguments = args)
#pragma warning restore BL0005 // Component parameter should not be set outside of its component.
            };

            typeof(Wizard).GetField("activeStepIndex", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(wizard, 2);

            await wizard.Start();

            Assert.Equal(2, typeof(Wizard).GetField("activeStepIndex", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(wizard));
            Assert.Null(arguments);
        }
    }
}
