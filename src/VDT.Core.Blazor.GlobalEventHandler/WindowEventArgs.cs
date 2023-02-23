using System;

namespace VDT.Core.Blazor.GlobalEventHandler {
    /// <summary>
    /// Supplies information about a window event that is being raised
    /// </summary>
    public class WindowEventArgs : EventArgs {
        /// <summary>
        /// Type of the event
        /// </summary>
        public string Type { get; set; } = default!;
    }
}
