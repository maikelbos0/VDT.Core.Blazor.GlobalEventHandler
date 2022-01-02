using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VDT.Core.Blazor.Wizard {
    /// <summary>
    /// Component that renders a series of steps to be completed by the user
    /// </summary>
    public class Wizard : ComponentBase {
        private readonly List<WizardStep> stepsInternal = new();
        private readonly WizardLayoutContext layoutContext;
        private int? activeStepIndex;

        /// <summary>
        /// Constructs an instance of a wizard
        /// </summary>
        public Wizard() {
            layoutContext = new WizardLayoutContext(this);
        }

        /// <summary>
        /// CSS class to apply to the wizard container; this value will not be used if a layout is specified
        /// </summary>
        [Parameter] public string? ContainerClass { get; set; }

        /// <summary>
        /// CSS class to apply to the wizard title section; this value will not be used if a layout is specified
        /// </summary>
        [Parameter] public string? TitleContainerClass { get; set; }

        /// <summary>
        /// Wizard title content
        /// </summary>
        [Parameter] public RenderFragment? TitleContent { get; set; }

        /// <summary>
        /// CSS class to apply to the wizard step title section; this value will not be used if a layout is specified
        /// </summary>
        [Parameter] public string? StepTitleContainerClass { get; set; }

        /// <summary>
        /// CSS class to apply to the wizard step titles
        /// </summary>
        [Parameter] public string? StepTitleClass { get; set; }

        /// <summary>
        /// CSS class to apply to the title of the active wizard step
        /// </summary>
        [Parameter] public string? ActiveStepTitleClass { get; set; }

        /// <summary>
        /// CSS class to apply to the button section; this value will not be used if a layout is specified
        /// </summary>
        [Parameter] public string? ButtonContainerClass { get; set; }

        /// <summary>
        /// CSS class to apply to the buttons
        /// </summary>
        [Parameter] public string? ButtonClass { get; set; }

        /// <summary>
        /// Show a button for stopping the wizard when unfinished
        /// </summary>
        [Parameter] public bool AllowCancel { get; set; }

        /// <summary>
        /// CSS class to apply to the button for stopping the wizad
        /// </summary>
        [Parameter] public string? ButtonCancelClass { get; set; }

        /// <summary>
        /// Text that is displayed on the button for stopping the wizard
        /// </summary>
        [Parameter] public string ButtonCancelText { get; set; } = "Cancel";

        /// <summary>
        /// Show a button for navigating to the previous step
        /// </summary>
        [Parameter] public bool AllowPrevious { get; set; }

        /// <summary>
        /// CSS class to apply to the button for the previous step
        /// </summary>
        [Parameter] public string? ButtonPreviousClass { get; set; }

        /// <summary>
        /// Text that is displayed on the button for the previous step
        /// </summary>
        [Parameter] public string ButtonPreviousText { get; set; } = "Previous";

        /// <summary>
        /// CSS class to apply to the button for the next step
        /// </summary>
        [Parameter] public string? ButtonNextClass { get; set; }

        /// <summary>
        /// Text that is displayed on the button for the next step
        /// </summary>
        [Parameter] public string ButtonNextText { get; set; } = "Next";

        /// <summary>
        /// CSS class to apply to the button for completing the wizard
        /// </summary>
        [Parameter] public string? ButtonFinishClass { get; set; }

        /// <summary>
        /// Text that is displayed on the button for completing the wizard
        /// </summary>
        [Parameter] public string ButtonFinishText { get; set; } = "Finish";

        /// <summary>
        /// CSS class to apply to the active step content section; this value will not be used if a layout is specified
        /// </summary>
        [Parameter] public string? ContentContainerClass { get; set; }

        /// <summary>
        /// Content containing the wizard steps in order
        /// </summary>
        [Parameter] public RenderFragment? Steps { get; set; }

        /// <summary>
        /// Layout for the wizard; if left empty a default layout will be used
        /// </summary>
        [Parameter] public RenderFragment<WizardLayoutContext>? Layout { get; set; }

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

        internal bool IsFirstStepActive => activeStepIndex.HasValue && activeStepIndex.Value == 0;

        internal bool IsLastStepActive => activeStepIndex.HasValue && activeStepIndex.Value == stepsInternal.Count - 1;

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

        internal IReadOnlyList<WizardStep> GetSteps() => stepsInternal.AsReadOnly();

        internal async Task GoToPreviousStep() {
            activeStepIndex--;
            await ActiveStep!.Initialize();
        }

        internal async Task TryCompleteStep() {
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

        /// <inheritdoc/>
        protected override void BuildRenderTree(RenderTreeBuilder builder) {
            if (IsActive) {
                builder.OpenComponent<CascadingValue<Wizard>>(1);
                builder.AddAttribute(2, "Value", this);
                builder.AddAttribute(3, "ChildContent", layoutContext.Wizard);
                builder.CloseComponent();
            }
        }
    }
}
