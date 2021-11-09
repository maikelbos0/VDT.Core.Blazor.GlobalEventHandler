describe('dblclick', function () {
    it('event handler gets registered', async function () {
        var globaleventhandler = await _helpers.getGlobaleventhandler();

        globaleventhandler.register(_helpers.dotNetObjectReference, _helpers.window);

        expect(typeof _helpers.window.eventListeners['dblclick']).toEqual('function');
    });

    it('event handler gets unregistered', async function () {
        var globaleventhandler = await _helpers.getGlobaleventhandler();

        globaleventhandler.register(_helpers.dotNetObjectReference, _helpers.window);
        globaleventhandler.unregister(_helpers.dotNetObjectReference, _helpers.window);

        expect(_helpers.window.eventListeners['dblclick']).toBeUndefined();
    });

    it('event invokes handler', async function () {
        var globaleventhandler = await _helpers.getGlobaleventhandler();

        globaleventhandler.register(_helpers.dotNetObjectReference, _helpers.window);

        _helpers.window.eventListeners['dblclick'](new MouseEvent(null, {
            altKey: true,
            button: 0,
            buttons: 0,
            clientX: 10,
            clientY: 20,
            ctrlKey: true,
            detail: 2,
            metaKey: true,
            screenX: 30,
            screenY: 40,
            shiftKey: true
        }));

        expect(_helpers.dotNetObjectReference.invocations.length).toEqual(1);
        expect(_helpers.dotNetObjectReference.invocations[0].handlerReference).toEqual('InvokeDoubleClick');
        expect(_helpers.dotNetObjectReference.invocations[0].eventArgs).toEqual({
            altKey: true,
            button: 0,
            buttons: 0,
            clientX: 10,
            clientY: 20,
            ctrlKey: true,
            detail: 2,
            metaKey: true,
            offsetX: 10,
            offsetY: 20,
            screenX: 30,
            screenY: 40,
            shiftKey: true,
            type: 'dblclick'
        });
    });
});
