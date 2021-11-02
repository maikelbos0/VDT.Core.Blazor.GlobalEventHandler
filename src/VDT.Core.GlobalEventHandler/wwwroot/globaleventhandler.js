
let handlers = {};

function register(dotNetObjectReference) {    
    handlers[dotNetObjectReference] = GetEventHandlers(dotNetObjectReference);

    for (const type in handlers[dotNetObjectReference]) {
        window.addEventListener(type, handlers[dotNetObjectReference][type]);
    }
}

function GetEventHandlers(dotNetObjectReference) {
    return {
        'keydown': GetKeyboardEventHandler(dotNetObjectReference, 'keydown', 'OnKeyDown'),
        'keyup': GetKeyboardEventHandler(dotNetObjectReference, 'keyup', 'OnKeyUp'),
        'click': GetMouseEventHandler(dotNetObjectReference, 'click', 'OnClick'),
        'resize': GetResizeEventHandler(dotNetObjectReference)
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

function GetMouseEventHandler(dotNetObjectReference, type, handlerReference) {
    return function (e) {
        dotNetObjectReference.invokeMethodAsync(handlerReference, {
            altKey: e.altKey,
            button: e.button,
            buttons: e.buttons,
            clientX: e.clientX,
            clientY: e.clientY,
            ctrlKey: e.ctrlKey,
            detail: e.detail,
            metaKey: e.metaKey,
            offsetX: e.offsetX,
            offsetY: e.offsetY,
            screenX: e.screenX,
            screenY: e.screenY,
            shiftKey: e.shiftKey,
            type: type
        });
    }
}

function GetResizeEventHandler(dotNetObjectReference) {
    return function () {
        dotNetObjectReference.invokeMethodAsync('OnResize', {
            width: window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth,
            height: window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight
        });
    }
}

function unregister(dotNetObjectReference) {
    for (const type in handlers[dotNetObjectReference]) {
        window.removeEventListener(type, handlers[dotNetObjectReference][type]);
    }

    delete handlers[dotNetObjectReference];
}

export { register, unregister };
