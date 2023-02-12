﻿let handlers = {};

function register(dotNetObjectReference) {
    handlers[dotNetObjectReference] = GetEventHandlers(dotNetObjectReference);

    for (const type in handlers[dotNetObjectReference]) {
        window.addEventListener(type, handlers[dotNetObjectReference][type]);
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
        'touchstart': GetTouchEventHandler(dotNetObjectReference, 'InvokeTouchStart'),
        'touchend': GetTouchEventHandler(dotNetObjectReference, 'InvokeTouchEnd'),
        'touchcancel': GetTouchEventHandler(dotNetObjectReference, 'InvokeTouchCancel'),
        'touchmove': GetTouchEventHandler(dotNetObjectReference, 'InvokeTouchMove'),
        'resize': getResizeEventHandler(dotNetObjectReference),
        'scroll': getScrollEventHandler(dotNetObjectReference)
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
            shiftKey: e.shiftKey,
            type: e.type
        });
    }
}

function GetTouchEventHandler(dotNetObjectReference, handlerReference) {
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
    return function () {
        dotNetObjectReference.invokeMethodAsync('InvokeResize', {
            width: window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth,
            height: window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight
        });
    }
}

function getScrollEventHandler(dotNetObjectReference) {
    return function () {
        dotNetObjectReference.invokeMethodAsync('InvokeScroll', {
            scrollX: window.scrollX,
            scrollY: window.scrollY
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