using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace VDT.Core.Blazor.Wizard {
    /// <summary>
    /// Step as part of a wizard
    /// </summary>
    public partial class WizardStep : ComponentBase {
        [CascadingParameter] private Wizard? Parent { get; set; }

        /// <summary>
        /// Content of a wizard step such as an explanation or an <see cref="EditForm"/>
        /// </summary>
        [Parameter] public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// Indicates whether or not the wizard step is currently active
        /// </summary>
        public bool IsActive => Parent?.ActiveStep == this;

        /// <inheritdoc/>
        protected override void OnInitialized() {
            Parent?.AddStep(this);
        }
    }
}
