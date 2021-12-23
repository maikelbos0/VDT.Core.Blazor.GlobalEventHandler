using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace VDT.Core.Blazor.Wizard {
    /// <summary>
    /// Component that renders a series of steps to be completed by the user
    /// </summary>
    public partial class Wizard : ComponentBase {
        private List<WizardStep> stepsInternal = new List<WizardStep>();
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

        internal WizardStep? ActiveStep => activeStepIndex.HasValue && stepsInternal.Count > activeStepIndex.Value ? stepsInternal[activeStepIndex.Value] : null;

        internal bool IsLastStep => activeStepIndex.HasValue && activeStepIndex.Value == stepsInternal.Count - 1;

        /// <summary>
        /// Start the wizard if it's not already active
        /// </summary>
        public void Start() {
            if (IsActive) {
                return;
            }

            activeStepIndex = 0;
            StateHasChanged();
        }

        internal void AddStep(WizardStep step) {
            if (!stepsInternal.Contains(step)) {
                stepsInternal.Add(step);
            }
        }

        private void TryCompleteStep() {
            activeStepIndex++;

            if (ActiveStep == null) {
                activeStepIndex = null;
                stepsInternal.Clear();
            }

            StateHasChanged();
        }
    }
}
