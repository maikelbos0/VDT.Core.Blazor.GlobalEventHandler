let handlers = {};

function register(dotNetObjectReference, win) {
    win = win || window;

    handlers[dotNetObjectReference] = GetEventHandlers(dotNetObjectReference);

    for (const type in handlers[dotNetObjectReference]) {
        win.addEventListener(type, handlers[dotNetObjectReference][type]);
    }
}

function GetEventHandlers(dotNetObjectReference) {
    return {
        'keydown': getKeyboardEventHandler(dotNetObjectReference, 'keydown', 'InvokeKeyDown'),
        'keyup': getKeyboardEventHandler(dotNetObjectReference, 'keyup', 'InvokeKeyUp'),
        'resize': getResizeEventHandler(dotNetObjectReference),
        'click': getMouseEventHandler(dotNetObjectReference, 'click', 'InvokeClick'),
        'mousedown': getMouseEventHandler(dotNetObjectReference, 'mousedown', 'InvokeMouseDown'),
        'mouseup': getMouseEventHandler(dotNetObjectReference, 'mouseup', 'InvokeMouseUp'),
        'mousemove': getMouseEventHandler(dotNetObjectReference, 'mousemove', 'InvokeMouseMove'),
        'contextmenu': getMouseEventHandler(dotNetObjectReference, 'contextmenu', 'InvokeContextMenu'),
        'dblclick': getMouseEventHandler(dotNetObjectReference, 'dblclick', 'InvokeDoubleClick')
    };
}

function getKeyboardEventHandler(dotNetObjectReference, type, handlerReference) {
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

function getMouseEventHandler(dotNetObjectReference, type, handlerReference) {
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

function getResizeEventHandler(dotNetObjectReference) {
    return function () {
        dotNetObjectReference.invokeMethodAsync('InvokeResize', {
            width: window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth,
            height: window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight
        });
    }
}

function unregister(dotNetObjectReference, win) {
    win = win || window;

    for (const type in handlers[dotNetObjectReference]) {
        win.removeEventListener(type, handlers[dotNetObjectReference][type]);
    }

    delete handlers[dotNetObjectReference];
}

export { register, unregister };
