using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VDT.Core.Blazor.Wizard {
    /// <summary>
    /// Component that renders a series of steps to be completed by the user
    /// </summary>
    public partial class Wizard : ComponentBase {
        private readonly List<WizardStep> stepsInternal = new();
        private int? activeStepIndex;

        /// <summary>
        /// Wizard title content
        /// </summary>
        [Parameter] public RenderFragment? TitleContent { get; set; }

        /// <summary>
        /// Text that is displayed on the button for the next step
        /// </summary>
        [Parameter] public string ButtonNextText { get; set; } = "Next";

        /// <summary>
        /// Text that is displayed on the button for completing the wizard
        /// </summary>
        [Parameter] public string ButtonFinishText { get; set; } = "Finish";

        /// <summary>
        /// Content containing the wizard steps in order; any additional content will also be displayed
        /// </summary>
        [Parameter] public RenderFragment? Steps { get; set; }

        /// <summary>
        /// Indicates whether or not the wizard is currently active
        /// </summary>
        public bool IsActive => activeStepIndex.HasValue;

        /// <summary>
        /// A callback that will be invoked when this wizard is started
        /// </summary>
        [Parameter]
        public EventCallback<WizardStartedEventArgs> OnStart { get; set; }

        /// <summary>
        /// A callback that will be invoked when this wizard is stopped
        /// </summary>
        [Parameter]
        public EventCallback<WizardStoppedEventArgs> OnStop { get; set; }

        /// <summary>
        /// A callback that will be invoked when this wizard is finished; all steps of the wizard have been completed
        /// </summary>
        [Parameter]
        public EventCallback<WizardFinishedEventArgs> OnFinish { get; set; }

        internal WizardStep? ActiveStep => activeStepIndex.HasValue && stepsInternal.Count > activeStepIndex.Value ? stepsInternal[activeStepIndex.Value] : null;

        internal bool IsLastStep => activeStepIndex.HasValue && activeStepIndex.Value == stepsInternal.Count - 1;

        /// <summary>
        /// Open the wizard at the first step if it's not currently active
        /// </summary>
        public async Task Start() {
            if (IsActive) {
                return;
            }

            activeStepIndex = 0;
            await OnStart.InvokeAsync(new WizardStartedEventArgs());
            StateHasChanged();
        }

        /// <summary>
        /// Close and reset the wizard if it's currently active
        /// </summary>
        public async Task Stop() {
            if (!IsActive) {
                return;
            }

            Reset();
            await OnStop.InvokeAsync(new WizardStoppedEventArgs());
        }

        internal async Task AddStep(WizardStep step) {
            if (!stepsInternal.Contains(step)) {
                stepsInternal.Add(step);

                if (stepsInternal.Count == 1) {
                    await ActiveStep!.Initialize();
                }
            }
        }

        private async Task TryCompleteStep() {
            if (await ActiveStep!.TryComplete()) {
                activeStepIndex++;

                if (ActiveStep == null) {
                    Reset();
                    await OnFinish.InvokeAsync(new WizardFinishedEventArgs());
                }
                else {
                    await ActiveStep!.Initialize();
                }
            }
        }

        private void Reset() {
            activeStepIndex = null;
            stepsInternal.Clear();
            StateHasChanged();
        }
    }
}
