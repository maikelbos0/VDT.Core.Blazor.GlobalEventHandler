using System;

namespace VDT.Core.Blazor.Wizard {
    /// <summary>
    /// Supplies information about a wizard step attempting to complete event that is being raised
    /// </summary>
    public class TryCompleteStepEventArgs : EventArgs {
        /// <summary>
        /// Indicates if completion of the step should be cancelled
        /// </summary>
        public bool IsCancelled { get; set; }

        // TODO pass marker to step somehow?
    }
}
