using Bunit;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;
using Xunit;

namespace VDT.Core.GlobalEventHandler.Tests {
    public class GlobalEventHandlerTests {
        [Fact]
        public async Task GlobalEventHandler_InvokeKeyDown_Invokes_OnKeyDown_Handler() {
            using var context = new TestContext();

            context.JSInterop.SetupModule("./_content/VDT.Core.GlobalEventHandler/globaleventhandler.js").SetupVoid("register", _ => true);

            KeyboardEventArgs expected = new KeyboardEventArgs();
            KeyboardEventArgs actual = null!;

            var handler = context.RenderComponent<GlobalEventHandler>(parameters => parameters.Add(p => p.OnKeyDown, (args) => actual = args));

            await handler.Instance.InvokeKeyDown(expected);

            Assert.Equal(expected, actual);
        }
    }
}
