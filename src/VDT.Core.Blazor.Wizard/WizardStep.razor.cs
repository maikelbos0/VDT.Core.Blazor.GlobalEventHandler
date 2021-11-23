using Microsoft.AspNetCore.Components;

namespace VDT.Core.Blazor.Wizard {
    public partial class WizardStep : ComponentBase {
        [CascadingParameter]
        private Wizard? Parent { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        public bool IsActive => Parent?.ActiveStep == this;

        protected override void OnInitialized() {
            Parent?.AddStep(this);
        }
    }
}
