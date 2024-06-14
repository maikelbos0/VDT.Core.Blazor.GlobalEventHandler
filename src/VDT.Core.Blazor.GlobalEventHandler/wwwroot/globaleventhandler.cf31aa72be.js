let handlers = {};

function register(dotNetObjectReference) {
    handlers[dotNetObjectReference._id] = GetEventHandlers(dotNetObjectReference);

    for (const type in handlers[dotNetObjectReference._id]) {
        window.addEventListener(type, handlers[dotNetObjectReference._id][type]);
    }
}

function GetEventHandlers(dotNetObjectReference) {
    return {
        'keydown': getKeyboardEventHandler(dotNetObjectReference, 'InvokeKeyDown'),
        'keyup': getKeyboardEventHandler(dotNetObjectReference, 'InvokeKeyUp'),
        'click': getMouseEventHandler(dotNetObjectReference, 'InvokeClick'),
        'contextmenu': getMouseEventHandler(dotNetObjectReference, 'InvokeContextMenu'),
        'dblclick': getMouseEventHandler(dotNetObjectReference, 'InvokeDoubleClick'),
        'mousedown': getMouseEventHandler(dotNetObjectReference, 'InvokeMouseDown'),
        'mouseup': getMouseEventHandler(dotNetObjectReference, 'InvokeMouseUp'),
        'mousemove': getMouseEventHandler(dotNetObjectReference, 'InvokeMouseMove'),
        'touchstart': getTouchEventHandler(dotNetObjectReference, 'InvokeTouchStart'),
        'touchend': getTouchEventHandler(dotNetObjectReference, 'InvokeTouchEnd'),
        'touchcancel': getTouchEventHandler(dotNetObjectReference, 'InvokeTouchCancel'),
        'touchmove': getTouchEventHandler(dotNetObjectReference, 'InvokeTouchMove'),
        'resize': getResizeEventHandler(dotNetObjectReference),
        'scroll': getScrollEventHandler(dotNetObjectReference),
        'offline': getOnlineStatusEventHandler(dotNetObjectReference, 'InvokeOffline'),
        'online': getOnlineStatusEventHandler(dotNetObjectReference, 'InvokeOnline')
    };
}

function getKeyboardEventHandler(dotNetObjectReference, handlerReference) {
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
            type: e.type
        });
    }
}

function getMouseEventHandler(dotNetObjectReference, handlerReference) {
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
            pageX: e.pageX,
            pageY: e.pageY,
            shiftKey: e.shiftKey,
            type: e.type
        });
    }
}

function getTouchEventHandler(dotNetObjectReference, handlerReference) {
    return function (e) {
        dotNetObjectReference.invokeMethodAsync(handlerReference, {
            altKey: e.altKey,
            changedTouches: Array.from(e.changedTouches).map(MapTouchPoint),
            ctrlKey: e.ctrlKey,
            detail: e.detail,
            shiftKey: e.shiftKey,
            targetTouches: Array.from(e.targetTouches).map(MapTouchPoint),
            touches: Array.from(e.touches).map(MapTouchPoint),
            type: e.type
        });
    }

    function MapTouchPoint(touchPoint) {
        return {
            identifier: touchPoint.identifier,
            screenX: touchPoint.screenX,
            screenY: touchPoint.screenY,
            clientX: touchPoint.clientX,
            clientY: touchPoint.clientY,
            pageX: touchPoint.pageX,
            pageY: touchPoint.pageY,
        };
    }
}

function getResizeEventHandler(dotNetObjectReference) {
    return function (e) {
        dotNetObjectReference.invokeMethodAsync('InvokeResize', {
            width: window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth,
            height: window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight,
            type: e.type
        });
    }
}

function getScrollEventHandler(dotNetObjectReference) {
    return function (e) {
        dotNetObjectReference.invokeMethodAsync('InvokeScroll', {
            scrollX: window.scrollX,
            scrollY: window.scrollY,
            type: e.type
        });
    }
}

function getOnlineStatusEventHandler(dotNetObjectReference, handlerReference) {
    return function (e) {
        dotNetObjectReference.invokeMethodAsync(handlerReference, {
            type: e.type
        });
    }
}

function unregister(dotNetObjectReference) {
    for (const type in handlers[dotNetObjectReference._id]) {
        window.removeEventListener(type, handlers[dotNetObjectReference._id][type]);
    }

    delete handlers[dotNetObjectReference._id];
}

export { register, unregister };
