using System;

namespace VDT.Core.Blazor.GlobalEventHandler {
    public class ScrollEventArgs : EventArgs {
        public double ScrollX { get; protected set; }
        public double ScrollY { get; protected set; }

        public ScrollEventArgs(double scrollX, double scrollY) {
            ScrollX = scrollX;
            ScrollY = scrollY;
        }
    }
}