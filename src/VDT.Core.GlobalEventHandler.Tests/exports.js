var win = {
    eventListeners: {},

    addEventListener: function (type, listener) {
        this.eventListeners[type] = listener;
    }
};

var dotNetObjectReference = {
    invocations: [],

    invokeMethodAsync: function (handlerReference, eventArgs) {
        invocations.push({
            handlerReference: handlerReference,
            eventArgs: eventArgs
        });
    }
};

describe('globaleventhandler exports function', function () {
    it('register', async function () {
        var globaleventhandler = await import('../VDT.Core.GlobalEventHandler/wwwroot/globaleventhandler.js');

        expect(typeof globaleventhandler.register).toEqual('function');
    });

    it('unregister', async function () {
        var globaleventhandler = await import('../VDT.Core.GlobalEventHandler/wwwroot/globaleventhandler.js');

        expect(typeof globaleventhandler.unregister).toEqual('function');
    });
});
