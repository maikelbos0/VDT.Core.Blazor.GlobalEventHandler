
let handlers = {};

export function register(dotNetObjectReference) {    
    handlers[dotNetObjectReference] = GetEventHandlers(dotNetObjectReference);

    for (const type in handlers[dotNetObjectReference]) {
        window.addEventListener(type, handlers[dotNetObjectReference][type]);
    }
}

function GetEventHandlers(dotNetObjectReference) {
    return {
        'keydown': GetKeyboardEventHandler(dotNetObjectReference, 'keydown', 'OnKeyDown'),
        'keyup': GetKeyboardEventHandler(dotNetObjectReference, 'keyup', 'OnKeyUp')
    };
}

function GetKeyboardEventHandler(dotNetObjectReference, type, handlerReference) {
    return function (e) {
        dotNetObjectReference.invokeMethodAsync(handlerReference, {
            altKey: e.altKey,
            code: e.code,
            ctrlKey: e.ctrlKey,
            key: e.key,
            location: e.location,
            metaKey: e.metaKey,
            repeat: e.repeat,
            shiftKey: e.shiftKey,
            type: type
        });
    }
}

export function unregister(dotNetObjectReference) {
    for (const type in handlers[dotNetObjectReference]) {
        window.addEventListener(type, handlers[dotNetObjectReference][type]);
    }

    delete handlers[dotNetObjectReference];
}
