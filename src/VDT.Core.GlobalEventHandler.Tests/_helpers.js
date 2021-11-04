var _helpers = {
    getGlobaleventhandler: async function () {
        return await import('../VDT.Core.GlobalEventHandler/wwwroot/globaleventhandler.js');
    },

    window: {
        eventListeners: {},

        addEventListener: function (type, listener) {
            this.eventListeners[type] = listener;
        },

        removeEventListener: function (type) {
            delete this.eventListeners[type];
        }
    },

    dotNetObjectReference: {
        invocations: [],

        invokeMethodAsync: function (handlerReference, eventArgs) {
            invocations.push({
                handlerReference: handlerReference,
                eventArgs: eventArgs
            });
        }
    }
};