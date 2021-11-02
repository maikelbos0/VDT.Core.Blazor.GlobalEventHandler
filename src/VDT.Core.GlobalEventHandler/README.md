# VDT.Core.GlobalEventHandler

Blazor component that allows you to handle global window level events in your application

## Features

- Window-level keyboard events that don't require focus on any element

## Usage

To register event handlers, simply include the GlobalEventHandler component on your page and register your event handler with the event in the component
that you want to handle. The available events are:

- `OnWindowKeyDown` which provides an optional `KeyboardEventArgs` parameter
- `OnWindowKeyUp` which provides an optional `KeyboardEventArgs` parameter
- `OnWindowResize` which provides an optional `ResizeEventArgs` parameter
- `OnWindowClick` which provides an optional `MouseEventArgs` parameter

### Example

```
<GlobalEventHandler OnWindowKeyDown="@OnWindowKeyDown" OnWindowResize="@OnWindowResize" OnWindowClick="@OnWindowClick" />

@if (keyDownEventArgs != null) {
    <h2>Last key down event</h2>

    <ul>
        <li>Alt key: @keyDownEventArgs.AltKey</li>
        <li>Code: @keyDownEventArgs.Code</li>
        <li>Ctrl key: @keyDownEventArgs.CtrlKey</li>
        <li>Key: @keyDownEventArgs.Key</li>
        <li>Location: @keyDownEventArgs.Location</li>
        <li>Meta key: @keyDownEventArgs.MetaKey</li>
        <li>Repeat: @keyDownEventArgs.Repeat</li>
        <li>Shift key: @keyDownEventArgs.ShiftKey</li>
        <li>Type: @keyDownEventArgs.Type</li>
    </ul>
}

@if (resizeEventArgs != null) {
    <h2>Last resize event</h2>

    <ul>
        <li>Width: @resizeEventArgs.Width</li>
        <li>Height: @resizeEventArgs.Height</li>
    </ul>
}

@if (clicked) {
    <h2>Clicked</h2>
}

@code {
    private KeyboardEventArgs? keyDownEventArgs;
    private ResizeEventArgs? resizeEventArgs;
    private bool clicked = false;
    
    // Handlers can be asynchronous ...
    public async Task OnWindowKeyDown(KeyboardEventArgs args) {
        keyDownEventArgs = args;

        await Task.CompletedTask;
    }    

    // ... Or synchronous
    public void OnWindowResize(ResizeEventArgs args) {
        resizeEventArgs = args;
    }

    // EventArgs are optional
    public void OnWindowClick() {
        clicked = true;
    }
}
```
