using Microsoft.AspNetCore.Components;

namespace VDT.Core.Blazor.Wizard {
    /// <summary>
    /// Layout for a wizard
    /// </summary>
    public class WizardLayout {
        private readonly Wizard wizard;

        internal WizardLayout(Wizard wizard) {
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
            var sequence = 0;

            foreach (var step in wizard.GetSteps()) {
                builder.OpenElement(++sequence, "div");

                if (step == wizard.ActiveStep) {
                    builder.AddAttribute(++sequence, "class", $"{wizard.StepTitleClass} {wizard.ActiveStepTitleClass}");
                }
                else {
                    builder.AddAttribute(++sequence, "class", $"{wizard.StepTitleClass}");
                }

                builder.AddContent(++sequence, step.Title);
                builder.CloseElement();
            }
        };

        /// <summary>
        /// Renders the wizard cancel, next and finish buttons
        /// </summary>
        public RenderFragment Buttons => builder => {
            var sequence = 0;

            builder.AddContent(++sequence, ButtonCancel);
            builder.AddContent(++sequence, ButtonFinish);
            builder.AddContent(++sequence, ButtonNext);
        };

        /// <summary>
        /// Renders the wizard cancel button
        /// </summary>
        public RenderFragment ButtonCancel => builder => {
            if (!wizard.ShowCancelButton) return;

            var sequence = 0;

            builder.OpenElement(++sequence, "button");
            builder.AddAttribute(++sequence, "onclick", EventCallback.Factory.Create(wizard, wizard.Stop));
            builder.AddAttribute(++sequence, "class", $"{wizard.ButtonClass} {wizard.ButtonCancelClass}");
            builder.AddContent(++sequence, wizard.ButtonCancelText);
            builder.CloseElement();
        };

        /// <summary>
        /// Renders the wizard next button
        /// </summary>
        public RenderFragment ButtonNext => builder => {
            if (wizard.IsLastStep) return;

            var sequence = 0;

            builder.OpenElement(++sequence, "button");
            builder.AddAttribute(++sequence, "onclick", EventCallback.Factory.Create(wizard, wizard.TryCompleteStep));
            builder.AddAttribute(++sequence, "class", $"{wizard.ButtonClass} {wizard.ButtonNextClass}");
            builder.AddContent(++sequence, wizard.ButtonNextText);
            builder.CloseElement();
        };

        /// <summary>
        /// Renders the wizard finish button
        /// </summary>
        public RenderFragment ButtonFinish => builder => {
            if (!wizard.IsLastStep) return;

            var sequence = 0;

            builder.OpenElement(++sequence, "button");
            builder.AddAttribute(++sequence, "onclick", EventCallback.Factory.Create(wizard, wizard.TryCompleteStep));
            builder.AddAttribute(++sequence, "class", $"{wizard.ButtonClass} {wizard.ButtonFinishClass}");
            builder.AddContent(++sequence, wizard.ButtonFinishText);
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
