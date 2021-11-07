using Bunit;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;
using Xunit;

namespace VDT.Core.GlobalEventHandler.Tests {
    public class GlobalEventHandlerTests {
        [Fact]
        public async Task GlobalEventHandler_InvokeKeyDown_Invokes_OnKeyDown_Handler() {
            KeyboardEventArgs expected = new KeyboardEventArgs();
            KeyboardEventArgs actual = null!;

            using var context = GetTestContext();
            var handler = context.RenderComponent<GlobalEventHandler>(parameters => parameters.Add(p => p.OnKeyDown, (args) => actual = args));

            await handler.Instance.InvokeKeyDown(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeKeyUp_Invokes_OnKeyUp_Handler() {
            KeyboardEventArgs expected = new KeyboardEventArgs();
            KeyboardEventArgs actual = null!;

            using var context = GetTestContext();
            var handler = context.RenderComponent<GlobalEventHandler>(parameters => parameters.Add(p => p.OnKeyUp, (args) => actual = args));

            await handler.Instance.InvokeKeyUp(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeResize_Invokes_OnResize_Handler() {
            ResizeEventArgs expected = new ResizeEventArgs(0, 0);
            ResizeEventArgs actual = null!;

            using var context = GetTestContext();
            var handler = context.RenderComponent<GlobalEventHandler>(parameters => parameters.Add(p => p.OnResize, (args) => actual = args));

            await handler.Instance.InvokeResize(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeClick_Invokes_OnClick_Handler() {
            MouseEventArgs expected = new MouseEventArgs();
            MouseEventArgs actual = null!;

            using var context = GetTestContext();
            var handler = context.RenderComponent<GlobalEventHandler>(parameters => parameters.Add(p => p.OnClick, (args) => actual = args));

            await handler.Instance.InvokeClick(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeMouseDown_Invokes_OnMouseDown_Handler() {
            MouseEventArgs expected = new MouseEventArgs();
            MouseEventArgs actual = null!;

            using var context = GetTestContext();
            var handler = context.RenderComponent<GlobalEventHandler>(parameters => parameters.Add(p => p.OnMouseDown, (args) => actual = args));

            await handler.Instance.InvokeMouseDown(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeMouseUp_Invokes_OnMouseUp_Handler() {
            MouseEventArgs expected = new MouseEventArgs();
            MouseEventArgs actual = null!;

            using var context = GetTestContext();
            var handler = context.RenderComponent<GlobalEventHandler>(parameters => parameters.Add(p => p.OnMouseUp, (args) => actual = args));

            await handler.Instance.InvokeMouseUp(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeMouseMove_Invokes_OnMouseMove_Handler() {
            MouseEventArgs expected = new MouseEventArgs();
            MouseEventArgs actual = null!;

            using var context = GetTestContext();
            var handler = context.RenderComponent<GlobalEventHandler>(parameters => parameters.Add(p => p.OnMouseMove, (args) => actual = args));

            await handler.Instance.InvokeMouseMove(expected);

            Assert.Equal(expected, actual);
        }

        private TestContext GetTestContext() {
            const string moduleLocation = "./_content/VDT.Core.GlobalEventHandler/globaleventhandler.js";
            var context = new TestContext();

            context.JSInterop.SetupModule(moduleLocation).SetupVoid("register", _ => true);

            return context;
        }
    }
}
