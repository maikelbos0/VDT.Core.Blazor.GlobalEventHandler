using System;

namespace VDT.Core.Blazor.GlobalEventHandler {
    /// <summary>
    /// Supplies information about a window resize event that is being raised
    /// </summary>
    public class ResizeEventArgs : EventArgs {
        /// <summary>
        /// Number of pixels that the window is wide after resizing
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Number of pixels that the window is high after resizing
        /// </summary>
        public int Height { get; set; }
    }
}
