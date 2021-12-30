using Microsoft.AspNetCore.Components;

namespace VDT.Core.Blazor.Wizard {
    /// <summary>
    /// Context for render fragments for a wizard layout template
    /// </summary>
    public class WizardLayoutContext {
        private readonly Wizard wizard;

        internal WizardLayoutContext(Wizard wizard) {
            this.wizard = wizard;
        }

        /// <summary>
        /// Renders the wizard title content
        /// </summary>
        public RenderFragment Title => builder => {
            builder.AddContent(1, wizard.TitleContent);
        };

        /// <summary>
        /// Renders the wizard step titles
        /// </summary>
        public RenderFragment StepTitles => builder => {
            foreach (var step in wizard.GetSteps()) {
                builder.OpenElement(1, "div");

                if (step == wizard.ActiveStep) {
                    builder.AddAttribute(2, "class", $"{wizard.StepTitleClass} {wizard.ActiveStepTitleClass}");
                }
                else {
                    builder.AddAttribute(2, "class", $"{wizard.StepTitleClass}");
                }

                builder.AddContent(3, step.Title);
                builder.CloseElement();
            }
        };

        /// <summary>
        /// Renders the wizard cancel, next and finish buttons
        /// </summary>
        public RenderFragment Buttons => builder => {
            builder.AddContent(1, ButtonCancel);
            builder.AddContent(2, ButtonPrevious);
            builder.AddContent(3, ButtonFinish);
            builder.AddContent(4, ButtonNext);
        };

        /// <summary>
        /// Renders the wizard cancel button
        /// </summary>
        public RenderFragment ButtonCancel => builder => {
            if (!wizard.AllowCancel) return;

            builder.OpenElement(1, "button");
            builder.AddAttribute(2, "onclick", EventCallback.Factory.Create(wizard, wizard.Stop));
            builder.AddAttribute(3, "class", $"{wizard.ButtonClass} {wizard.ButtonCancelClass}");
            builder.AddContent(4, wizard.ButtonCancelText);
            builder.CloseElement();
        };

        /// <summary>
        /// Renders the wizard previous button
        /// </summary>
        public RenderFragment ButtonPrevious => builder => {
            if (!wizard.AllowPrevious || wizard.IsFirstStepActive) return;

            builder.OpenElement(1, "button");
            builder.AddAttribute(2, "onclick", EventCallback.Factory.Create(wizard, wizard.GoToPreviousStep));
            builder.AddAttribute(3, "class", $"{wizard.ButtonClass} {wizard.ButtonPreviousClass}");
            builder.AddContent(4, wizard.ButtonPreviousText);
            builder.CloseElement();
        };

        /// <summary>
        /// Renders the wizard next button
        /// </summary>
        public RenderFragment ButtonNext => builder => {
            if (wizard.IsLastStep) return;

            builder.OpenElement(1, "button");
            builder.AddAttribute(2, "onclick", EventCallback.Factory.Create(wizard, wizard.TryCompleteStep));
            builder.AddAttribute(3, "class", $"{wizard.ButtonClass} {wizard.ButtonNextClass}");
            builder.AddContent(4, wizard.ButtonNextText);
            builder.CloseElement();
        };

        /// <summary>
        /// Renders the wizard finish button
        /// </summary>
        public RenderFragment ButtonFinish => builder => {
            if (!wizard.IsLastStep) return;

            builder.OpenElement(1, "button");
            builder.AddAttribute(2, "onclick", EventCallback.Factory.Create(wizard, wizard.TryCompleteStep));
            builder.AddAttribute(3, "class", $"{wizard.ButtonClass} {wizard.ButtonFinishClass}");
            builder.AddContent(4, wizard.ButtonFinishText);
            builder.CloseElement();
        };

        /// <summary>
        /// Renders the wizard active step content
        /// </summary>
        public RenderFragment Content => builder => {
            if (wizard.ActiveStep != null) {
                builder.AddContent(1, wizard.ActiveStep.ChildContent);
            }
        };
    }
}
