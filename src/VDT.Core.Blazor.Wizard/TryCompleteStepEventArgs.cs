using System;

namespace VDT.Core.Blazor.Wizard {
    public class TryCompleteStepEventArgs : EventArgs {
        public bool IsCancelled { get; set; }
    }
}
