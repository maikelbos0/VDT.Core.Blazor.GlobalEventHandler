
let handlers = {};

export function register(dotNetObjectReference) {
    let keyDownHandler = function (e) {
        dotNetObjectReference.invokeMethodAsync("OnKeyDown", {
            altKey: e.altKey,
            code: e.code,
            ctrlKey: e.ctrlKey,
            key: e.key,
            location: e.location,
            metaKey: e.metaKey,
            repeat: e.repeat,
            shiftKey: e.shiftKey,
            type: 'keydown'
        });
    }

    handlers[dotNetObjectReference] = keyDownHandler;

    window.addEventListener("keydown", keyDownHandler);
}

export function unregister(dotNetObjectReference) {
    window.removeEventListener("keydown", handlers[dotNetObjectReference]);
    delete handlers[dotNetObjectReference];
}
