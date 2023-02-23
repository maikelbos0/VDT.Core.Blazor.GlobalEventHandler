﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace VDT.Core.Blazor.GlobalEventHandler {
    /// <summary>
    /// Allows components to subscribe to window-level javascript events
    /// </summary>
    public class GlobalEventHandler : ComponentBase, IAsyncDisposable {
        internal const string ModuleLocation = "./_content/VDT.Core.Blazor.GlobalEventHandler/globaleventhandler.ecf9518373.js";

        private IJSObjectReference? moduleReference;
        private DotNetObjectReference<GlobalEventHandler>? dotNetObjectReference;

        [Inject] internal IJSRuntime JSRuntime { get; set; } = null!;

        /// <summary>
        /// A callback that will be invoked when the user presses a key on the keyboard
        /// </summary>
        [Parameter] public EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }

        /// <summary>
        /// A callback that will be invoked when the user releases a key on the keyboard
        /// </summary>
        [Parameter] public EventCallback<KeyboardEventArgs> OnKeyUp { get; set; }

        /// <summary>
        /// A callback that will be invoked when the user clicks anywhere in the document
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        /// <summary>
        /// A callback that will be invoked when the user presses the right mouse button anywhere in the document
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnContextMenu { get; set; }

        /// <summary>
        /// A callback that will be invoked when the user double clicks anywhere in the document
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnDoubleClick { get; set; }

        /// <summary>
        /// A callback that will be invoked when the user presses a mouse button anywhere in the document
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnMouseDown { get; set; }
        
        /// <summary>
        /// A callback that will be invoked when the user releases a mouse button anywhere in the document
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnMouseUp { get; set; }

        /// <summary>
        /// A callback that will be invoked when the user moves the mouse anywhere in the document
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnMouseMove { get; set; }

        /// <summary>
        /// A callback that will be invoked when one or more touch points are placed on the touch surface
        /// </summary>
        [Parameter] public EventCallback<TouchEventArgs> OnTouchStart { get; set; }

        /// <summary>
        /// A callback that will be invoked when one or more touch points are removed from the touch surface
        /// </summary>
        [Parameter] public EventCallback<TouchEventArgs> OnTouchEnd { get; set; }

        /// <summary>
        /// A callback that will be invoked when one or more touch points have been disrupted in an implementation-specific manner
        /// </summary>
        [Parameter] public EventCallback<TouchEventArgs> OnTouchCancel { get; set; }

        /// <summary>
        /// A callback that will be invoked when one or more touch points are moved along the touch surface
        /// </summary>
        [Parameter] public EventCallback<TouchEventArgs> OnTouchMove { get; set; }

        /// <summary>
        /// A callback that will be invoked when the browser window has been resized
        /// </summary>
        [Parameter] public EventCallback<ResizeEventArgs> OnResize { get; set; }

        /// <summary>
        /// A callback that will be invoked when the browser window content has been scrolled
        /// </summary>
        [Parameter] public EventCallback<ScrollEventArgs> OnScroll { get; set; }

        /// <summary>
        /// A callback that will be invoked when the browser loses access to the network and is unable to access networked resources
        /// </summary>
        [Parameter] public EventCallback<WindowEventArgs> OnOffline { get; set; }

        /// <summary>
        /// A callback that will be invoked when the browser gains access to the network and might be able to access networked resources
        /// </summary>
        [Parameter] public EventCallback<WindowEventArgs> OnOnline { get; set; }

        /// <summary>
        /// Invoke the callback for the key down event
        /// </summary>
        /// <param name="args">Keyboard event information</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        [JSInvokable] public async Task InvokeKeyDown(KeyboardEventArgs args) => await OnKeyDown.InvokeAsync(args);

        /// <summary>
        /// Invoke the callback for the key up event
        /// </summary>
        /// <param name="args">Keyboard event information</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        [JSInvokable] public async Task InvokeKeyUp(KeyboardEventArgs args) => await OnKeyUp.InvokeAsync(args);

        /// <summary>
        /// Invoke the callback for the click event
        /// </summary>
        /// <param name="args">Mouse event information</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        [JSInvokable] public async Task InvokeClick(MouseEventArgs args) => await OnClick.InvokeAsync(args);

        /// <summary>
        /// Invoke the callback for the context menu event
        /// </summary>
        /// <param name="args">Mouse event information</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        [JSInvokable] public async Task InvokeContextMenu(MouseEventArgs args) => await OnContextMenu.InvokeAsync(args);

        /// <summary>
        /// Invoke the callback for the double click event
        /// </summary>
        /// <param name="args">Mouse event information</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        [JSInvokable] public async Task InvokeDoubleClick(MouseEventArgs args) => await OnDoubleClick.InvokeAsync(args);

        /// <summary>
        /// Invoke the callback for the mouse down event
        /// </summary>
        /// <param name="args">Mouse event information</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        [JSInvokable] public async Task InvokeMouseDown(MouseEventArgs args) => await OnMouseDown.InvokeAsync(args);

        /// <summary>
        /// Invoke the callback for the mouse up event
        /// </summary>
        /// <param name="args">Mouse event information</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        [JSInvokable] public async Task InvokeMouseUp(MouseEventArgs args) => await OnMouseUp.InvokeAsync(args);

        /// <summary>
        /// Invoke the callback for the mouse move event
        /// </summary>
        /// <param name="args">Mouse event information</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        [JSInvokable] public async Task InvokeMouseMove(MouseEventArgs args) => await OnMouseMove.InvokeAsync(args);

        /// <summary>
        /// Invoke the callback for the touch start event
        /// </summary>
        /// <param name="args">Touch event information</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        [JSInvokable] public async Task InvokeTouchStart(TouchEventArgs args) => await OnTouchStart.InvokeAsync(args);

        /// <summary>
        /// Invoke the callback for the touch end event
        /// </summary>
        /// <param name="args">Touch event information</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        [JSInvokable] public async Task InvokeTouchEnd(TouchEventArgs args) => await OnTouchEnd.InvokeAsync(args);

        /// <summary>
        /// Invoke the callback for the touch cancel event
        /// </summary>
        /// <param name="args">Touch event information</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        [JSInvokable] public async Task InvokeTouchCancel(TouchEventArgs args) => await OnTouchCancel.InvokeAsync(args);

        /// <summary>
        /// Invoke the callback for the touch move event
        /// </summary>
        /// <param name="args">Touch event information</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        [JSInvokable] public async Task InvokeTouchMove(TouchEventArgs args) => await OnTouchMove.InvokeAsync(args);

        /// <summary>
        /// Invoke the callback for the resize event
        /// </summary>
        /// <param name="args">Resize event information</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        [JSInvokable] public async Task InvokeResize(ResizeEventArgs args) => await OnResize.InvokeAsync(args);

        /// <summary>
        /// Invoke the callback for the scroll event
        /// </summary>
        /// <param name="args">Scroll event information</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        [JSInvokable] public async Task InvokeScroll(ScrollEventArgs args) => await OnScroll.InvokeAsync(args);

        /// <summary>
        /// Invoke the callback for the offline event
        /// </summary>
        /// <param name="args">Event information</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        [JSInvokable] public async Task InvokeOffline(WindowEventArgs args) => await OnOffline.InvokeAsync(args);

        /// <summary>
        /// Invoke the callback for the online event
        /// </summary>
        /// <param name="args">Event information</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        [JSInvokable] public async Task InvokeOnline(WindowEventArgs args) => await OnOnline.InvokeAsync(args);

        /// <inheritdoc/>
        protected override bool ShouldRender() => false;

        /// <inheritdoc/>
        protected override async Task OnAfterRenderAsync(bool firstRender) {
            if (firstRender) {
                moduleReference = await JSRuntime.InvokeAsync<IJSObjectReference>("import", ModuleLocation);
                dotNetObjectReference = DotNetObjectReference.Create(this);

                await moduleReference.InvokeVoidAsync("register", dotNetObjectReference);
            }
        }

        /// <inheritdoc/>
        public async ValueTask DisposeAsync() {
            if (moduleReference != null) {
                await moduleReference.InvokeVoidAsync("unregister", dotNetObjectReference);
                await moduleReference.DisposeAsync();
            }

            dotNetObjectReference?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
