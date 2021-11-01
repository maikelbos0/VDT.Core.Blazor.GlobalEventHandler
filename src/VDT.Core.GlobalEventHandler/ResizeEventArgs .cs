using System;

namespace VDT.Core.GlobalEventHandler {
    public class ResizeEventArgs : EventArgs {
        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public ResizeEventArgs (int width, int height) {
            Width = width;
            Height = height;
        }
    }
}
