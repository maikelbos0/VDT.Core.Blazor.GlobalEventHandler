using System;

namespace VDT.Core.Blazor.GlobalEventHandler {
    /// <summary>
    /// Supplies information about a document scroll event that is being raised
    /// </summary>
    public class ScrollEventArgs : WindowEventArgs {
        /// <summary>
        /// Number of pixels that the document is currently scrolled horizontally
        /// </summary>
        public double ScrollX { get; set; }

        /// <summary>
        /// Number of pixels that the document is currently scrolled vertically
        /// </summary>
        public double ScrollY { get; set; }
    }
}