# VDT.Core.GlobalEventHandler

Blazor component that allows you to handle global window level events in your application

## Features

- Window-level keyboard events that don't require focus on any element

## Usage

To register event handlers, simply include the GlobalEventHandler component on your page and register your event handler with the event in the component
that you want to handle. The available events are:

- `OnWindowKeyDown`
- `OnWindowKeyUp`

### Example

```
<GlobalEventHandler OnWindowKeyDown="@OnWindowKeyDown" />

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

@code {
    private KeyboardEventArgs? keyDownEventArgs;

    public Task OnWindowKeyDown(KeyboardEventArgs args) {
        keyDownEventArgs = args;

        return Task.CompletedTask;
    }
}
```
