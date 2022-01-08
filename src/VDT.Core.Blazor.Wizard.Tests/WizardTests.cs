using Microsoft.AspNetCore.Components;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace VDT.Core.Blazor.Wizard.Tests {
    public class WizardTests {
        /*
         * TODO
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

        [Fact]
        public async Task Wizard_Stop_Works() {
            WizardStoppedEventArgs? arguments = null;
            var wizard = new Wizard() {
#pragma warning disable BL0005 // Component parameter should not be set outside of its component.
                OnStop = EventCallback.Factory.Create<WizardStoppedEventArgs>(this, args => arguments = args)
#pragma warning restore BL0005 // Component parameter should not be set outside of its component.
            };

            typeof(Wizard).GetField("activeStepIndex", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(wizard, 2);

            await wizard.Stop();

            Assert.Null(typeof(Wizard).GetField("activeStepIndex", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(wizard));
            Assert.NotNull(arguments);
        }

        [Fact]
        public async Task Wizard_Stop_Does_Nothing_When_Inactive() {
            WizardStoppedEventArgs? arguments = null;
            var wizard = new Wizard() {
#pragma warning disable BL0005 // Component parameter should not be set outside of its component.
                OnStop = EventCallback.Factory.Create<WizardStoppedEventArgs>(this, args => arguments = args)
#pragma warning restore BL0005 // Component parameter should not be set outside of its component.
            };

            await wizard.Stop();

            Assert.Null(arguments);
        }
    }
}
