describe('globaleventhandler', function () {
    it('register exists', async function () {
        var globaleventhandler = await _helpers.getGlobaleventhandler();

        expect(typeof globaleventhandler.register).toEqual('function');
    });

    it('unregister exists', async function () {
        var globaleventhandler = await _helpers.getGlobaleventhandler();

        expect(typeof globaleventhandler.unregister).toEqual('function');
    });

    it('register succeeds', async function () {
        var globaleventhandler = await _helpers.getGlobaleventhandler();

        expect(function () {
            globaleventhandler.register(_helpers.dotNetObjectReference, _helpers.window);
        }).not.toThrow();
    });

    it('unregister succeeds', async function () {
        var globaleventhandler = await _helpers.getGlobaleventhandler();

        globaleventhandler.register(_helpers.dotNetObjectReference, _helpers.window);

        expect(function () {
            globaleventhandler.unregister(_helpers.dotNetObjectReference, _helpers.window);
        }).not.toThrow();
    });
});
