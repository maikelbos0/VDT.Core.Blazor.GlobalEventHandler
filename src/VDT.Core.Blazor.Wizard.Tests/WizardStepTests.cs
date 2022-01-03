using Microsoft.AspNetCore.Components;
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
    }
}
