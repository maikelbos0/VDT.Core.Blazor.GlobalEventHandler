describe('keydown', function () {
    it('event handler gets registered', async function () {
        var globaleventhandler = await _helpers.getGlobaleventhandler();

        globaleventhandler.register(_helpers.dotNetObjectReference, _helpers.window);

        expect(typeof _helpers.window.eventListeners['keydown']).toEqual('function');
    });

    it('event handler gets unregistered', async function () {
        var globaleventhandler = await _helpers.getGlobaleventhandler();

        globaleventhandler.register(_helpers.dotNetObjectReference, _helpers.window);
        globaleventhandler.unregister(_helpers.dotNetObjectReference, _helpers.window);

        expect(_helpers.window.eventListeners['keydown']).toBeUndefined();
    });

    it('event invokes handler', async function () {
        var globaleventhandler = await _helpers.getGlobaleventhandler();

        globaleventhandler.register(_helpers.dotNetObjectReference, _helpers.window);

        _helpers.window.eventListeners['keydown'](new KeyboardEvent(null, {
            altKey: true,
            code: 'Digit1',
            ctrlKey: true,
            key: '1',
            location: 0,
            metaKey: true,
            repeat: true,
            shiftKey: true
        }));

        expect(_helpers.dotNetObjectReference.invocations.length).toEqual(1);
        expect(_helpers.dotNetObjectReference.invocations[0].handlerReference).toEqual('InvokeKeyDown');
        expect(_helpers.dotNetObjectReference.invocations[0].eventArgs).toEqual({
            altKey: true,
            code: 'Digit1',
            ctrlKey: true,
            key: '1',
            location: 0,
            metaKey: true,
            repeat: true,
            shiftKey: true,
            type: 'keydown'
        });
    });
});
