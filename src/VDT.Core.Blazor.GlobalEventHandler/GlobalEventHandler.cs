using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace VDT.Core.Blazor.GlobalEventHandler {
    /// <summary>
    /// Allows components to subscribe to window-level javascript events
    /// </summary>
    public class GlobalEventHandler : ComponentBase, IAsyncDisposable {
        internal const string ModuleLocation = "./_content/VDT.Core.Blazor.GlobalEventHandler/globaleventhandler.561587e4a9.js";

        private IJSObjectReference? moduleReference;
        private DotNetObjectReference<GlobalEventHandler>? dotNetObjectReference;

        [Inject] internal IJSRuntime JSRuntime { get; set; } = null!;

        /// <summary>
        /// A callback that will be invoked when a key is pressed anywhere in the window
        /// </summary>
        [Parameter] public EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }

        /// <summary>
        /// A callback that will be invoked when a pressed key is released anywhere in the window
        /// </summary>
        [Parameter] public EventCallback<KeyboardEventArgs> OnKeyUp { get; set; }

        /// <summary>
        /// A callback that will be invoked when the window is resized
        /// </summary>
        [Parameter] public EventCallback<ResizeEventArgs> OnResize { get; set; }

        /// <summary>
        /// A callback that will be invoked when a mouse button is clicked anywhere in the window
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        /// <summary>
        /// A callback that will be invoked when a mouse button is pressed anywhere in the window
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnMouseDown { get; set; }

        /// <summary>
        /// A callback that will be invoked when a pressed mouse button is released anywhere in the window
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnMouseUp { get; set; }

        /// <summary>
        /// A callback that will be invoked when the mouse is moved anywhere in the window
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnMouseMove { get; set; }

        /// <summary>
        /// A callback that will be invoked when a context menu is requested anywhere in the window; normally by using the right mouse button
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnContextMenu { get; set; }

        /// <summary>
        /// A callback that will be invoked when a mouse button is double clicked anywhere in the window
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnDoubleClick { get; set; }

        /// <summary>
        /// A callback that will be invoked when the document is scrolled
        /// </summary>
        [Parameter] public EventCallback<ScrollEventArgs> OnScroll { get; set; }

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
        /// Invoke the callback for the resize event
        /// </summary>
        /// <param name="args">Resize event information</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        [JSInvokable] public async Task InvokeResize(ResizeEventArgs args) => await OnResize.InvokeAsync(args);

        /// <summary>
        /// Invoke the callback for the click event
        /// </summary>
        /// <param name="args">Mouse event information</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        [JSInvokable] public async Task InvokeClick(MouseEventArgs args) => await OnClick.InvokeAsync(args);

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
        /// Invoke the callback for the scroll event
        /// </summary>
        /// <param name="args">Scroll event information</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        [JSInvokable] public async Task InvokeScroll(ScrollEventArgs args) => await OnScroll.InvokeAsync(args);

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
