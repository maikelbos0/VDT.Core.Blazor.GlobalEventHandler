describe('scroll', function () {
    it('event handler gets registered', async function () {
        var globaleventhandler = await _helpers.getGlobaleventhandler();

        globaleventhandler.register(_helpers.dotNetObjectReference, _helpers.window);

        expect(typeof _helpers.window.eventListeners['scroll']).toEqual('function');
    });

    it('event handler gets unregistered', async function () {
        var globaleventhandler = await _helpers.getGlobaleventhandler();

        globaleventhandler.register(_helpers.dotNetObjectReference, _helpers.window);
        globaleventhandler.unregister(_helpers.dotNetObjectReference, _helpers.window);

        expect(_helpers.window.eventListeners['scroll']).toBeUndefined();
    });

    it('event invokes handler', async function () {
        var globaleventhandler = await _helpers.getGlobaleventhandler();

        globaleventhandler.register(_helpers.dotNetObjectReference, _helpers.window);

        _helpers.window.eventListeners['scroll']();

        expect(_helpers.dotNetObjectReference.invocations.length).toEqual(1);
        expect(_helpers.dotNetObjectReference.invocations[0].handlerReference).toEqual('InvokeScroll');
        expect(_helpers.dotNetObjectReference.invocations[0].eventArgs).toEqual({
            scrollX: 0,
            scrollY: 0
        });
    });
});
