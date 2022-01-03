using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace VDT.Core.Blazor.Wizard.Tests {
    public class WizardStepTests {
        [Fact]
        public async Task WizardStep_Initialize_Invokes_OnInitialize() {
            WizardStepInitializedEventArgs? arguments = null;
            var step = new WizardStep() {
#pragma warning disable BL0005 // Component parameter should not be set outside of its component.
                OnInitialize = EventCallback.Factory.Create<WizardStepInitializedEventArgs>(this, (args) => arguments = args)
#pragma warning restore BL0005 // Component parameter should not be set outside of its component.
            };

            await step.Initialize();

            Assert.NotNull(arguments);
        }

        [Fact]
        public async Task WizardStep_TryComplete_Invokes_OnTryComplete() {
            WizardStepAttemptedCompleteEventArgs? arguments = null;

            var step = new WizardStep() {
#pragma warning disable BL0005 // Component parameter should not be set outside of its component.
                OnTryComplete = EventCallback.Factory.Create<WizardStepAttemptedCompleteEventArgs>(this, (args) => arguments = args)
#pragma warning restore BL0005 // Component parameter should not be set outside of its component.
            };

            await step.TryComplete();

            Assert.NotNull(arguments);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public async Task WizardStep_TryComplete_Returns_Correct_ShouldComplete(bool isCancelled, bool expectedResult) {
            var step = new WizardStep() {
#pragma warning disable BL0005 // Component parameter should not be set outside of its component.
                OnTryComplete = EventCallback.Factory.Create<WizardStepAttemptedCompleteEventArgs>(this, (args) => args.IsCancelled = isCancelled)
#pragma warning restore BL0005 // Component parameter should not be set outside of its component.
            };

            Assert.Equal(expectedResult, await step.TryComplete());
        }

        [Fact]
        public void WizardStep_IsActive_Is_False_For_No_Parent() {
            var step = new WizardStep() {
                Wizard = null
            };

            Assert.False(step.IsActive);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        public void WizardStep_IsActive_Is_Correct(int activeStepIndex, bool expectedIsActive) {
            var wizard = new Wizard();
            var step = new WizardStep() {
                Wizard = wizard
            };

            var stepsInternal = (List<WizardStep>)typeof(Wizard).GetField("stepsInternal", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(wizard)!;
            stepsInternal.Add(new WizardStep());
            stepsInternal.Add(step);
            typeof(Wizard).GetField("activeStepIndex", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(wizard, activeStepIndex);

            Assert.Equal(expectedIsActive, step.IsActive);
        }
    }
}
