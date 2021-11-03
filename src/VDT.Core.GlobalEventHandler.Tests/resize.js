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

describe('resize', function () {
    it('event handler gets registered', async function () {
        var globaleventhandler = await import('../VDT.Core.GlobalEventHandler/wwwroot/globaleventhandler.js');

        globaleventhandler.register(dotNetObjectReference, win);

        expect(typeof win.eventListeners["resize"]).toEqual('function');
    });
});
