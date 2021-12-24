using System;

namespace VDT.Core.Blazor.Wizard {
    /// <summary>
    /// Supplies information about a wizard attempting to complete a step event that is being raised
    /// </summary>
    public class TryCompleteWizardStepEventArgs : EventArgs {
        /// <summary>
        /// Indicates if completion of the step should be cancelled
        /// </summary>
        public bool IsCancelled { get; set; }

        // TODO pass marker to step somehow?
    }
}
