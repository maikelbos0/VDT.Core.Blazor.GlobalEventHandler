using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace VDT.Core.Blazor.Wizard {
    /// <summary>
    /// Component that renders a series of steps to be completed by the user
    /// </summary>
    public partial class Wizard : ComponentBase {
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

        // TODO maybe refactor these into fields/methods
        private List<WizardStep> StepsInternal { get; set; } = new List<WizardStep>();
        private int? ActiveStepIndex { get; set; } = 0;
        internal WizardStep? ActiveStep => ActiveStepIndex.HasValue && StepsInternal.Count > ActiveStepIndex.Value ? StepsInternal[ActiveStepIndex.Value] : null;

        /// <summary>
        /// Indicates whether or not the wizard is currently active
        /// </summary>
        public bool IsActive => ActiveStepIndex.HasValue;

        internal void AddStep(WizardStep step) {
            if (!StepsInternal.Contains(step)) {
                StepsInternal.Add(step);
            }
        }

        private void TryCompleteStep() {
            ActiveStepIndex++;

            if (ActiveStep == null) {
                ActiveStepIndex = null;
                StepsInternal.Clear();
            }
        }
    }
}
