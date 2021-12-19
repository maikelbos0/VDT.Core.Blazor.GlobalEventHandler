using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace VDT.Core.Blazor.Wizard {
    public partial class Wizard : ComponentBase {
        [Parameter] public RenderFragment? TitleContent { get; set; }
        [Parameter] public string ButtonNextText { get; set; } = "Next";
        [Parameter] public string ButtonFinishText { get; set; } = "Finish";
        [Parameter] public RenderFragment? Steps { get; set; }

        private List<WizardStep> StepsInternal { get; set; } = new List<WizardStep>();
        private int? ActiveStepIndex { get; set; } = 0;
        internal WizardStep? ActiveStep => ActiveStepIndex.HasValue && StepsInternal.Count > ActiveStepIndex.Value ? StepsInternal[ActiveStepIndex.Value] : null;
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
