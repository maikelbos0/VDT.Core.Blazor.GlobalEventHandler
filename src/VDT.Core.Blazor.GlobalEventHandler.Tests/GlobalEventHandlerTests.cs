using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using NSubstitute;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace VDT.Core.Blazor.GlobalEventHandler.Tests {
    public class GlobalEventHandlerTests {
        private class TestGlobalEventHandler : GlobalEventHandler {
            public new bool ShouldRender() => base.ShouldRender();
            public new async Task OnAfterRenderAsync(bool firstRender) => await base.OnAfterRenderAsync(firstRender);
        }

        [Fact]
        public void GlobalEventHandler_ShouldRender_Returns_False() {
            var subject = new TestGlobalEventHandler();

            Assert.False(subject.ShouldRender());
        }

        [Fact]
        public async Task GlobalEventHandler_OnAfterRenderAsync_Calls_JS_Module_Register_On_First_Render() {
            var runtime = Substitute.For<IJSRuntime>();
            var module = Substitute.For<IJSObjectReference>();
            runtime.InvokeAsync<IJSObjectReference>("import", new object[] { GlobalEventHandler.ModuleLocation }).ReturnsForAnyArgs(module);

            var subject = new TestGlobalEventHandler {
                JSRuntime = runtime
            };

            await subject.OnAfterRenderAsync(true);

            await module.Received().InvokeVoidAsync("register", Arg.Is<object[]>(args => Assert.Single(args) is DotNetObjectReference<GlobalEventHandler>));
        }

        [Fact]
        public async Task GlobalEventHandler_OnAfterRenderAsync_Does_Not_Call_JS_Module_Register_After_First_Render() {
            var runtime = Substitute.For<IJSRuntime>();
            var module = Substitute.For<IJSObjectReference>();
            runtime.InvokeAsync<IJSObjectReference>("import", new object[] { GlobalEventHandler.ModuleLocation }).ReturnsForAnyArgs(module);

            var subject = new TestGlobalEventHandler {
                JSRuntime = runtime
            };

            await subject.OnAfterRenderAsync(false);

            await module.DidNotReceiveWithAnyArgs().InvokeVoidAsync(Arg.Any<string>(), Arg.Any<object[]>());
            Assert.Fail("");
        }

        [Fact]
        public async Task GlobalEventHandler_DisposeAsync_Calls_JS_Module_Unregister() {
            var runtime = Substitute.For<IJSRuntime>();
            var module = Substitute.For<IJSObjectReference>();
            runtime.InvokeAsync<IJSObjectReference>("import", new object[] { GlobalEventHandler.ModuleLocation }).ReturnsForAnyArgs(module);

            await using (var subject = new TestGlobalEventHandler()) {
                subject.JSRuntime = runtime;
                await subject.OnAfterRenderAsync(true);
            }

            await module.Received().InvokeVoidAsync("unregister", Arg.Is<object[]>(args => Assert.Single(args) is DotNetObjectReference<GlobalEventHandler>));
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeKeyDown_Invokes_OnKeyDown_Handler() {
            KeyboardEventArgs expected = new();
            KeyboardEventArgs? actual = null;
            var subject = new GlobalEventHandler() {
                OnKeyDown = EventCallback.Factory.Create<KeyboardEventArgs>(this, (args) => actual = args)
            };

            await subject.InvokeKeyDown(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeKeyUp_Invokes_OnKeyUp_Handler() {
            KeyboardEventArgs expected = new();
            KeyboardEventArgs? actual = null;
            var subject = new GlobalEventHandler() {
                OnKeyUp = EventCallback.Factory.Create<KeyboardEventArgs>(this, (args) => actual = args)
            };

            await subject.InvokeKeyUp(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeClick_Invokes_OnClick_Handler() {
            MouseEventArgs expected = new();
            MouseEventArgs? actual = null;
            var subject = new GlobalEventHandler() {
                OnClick = EventCallback.Factory.Create<MouseEventArgs>(this, (args) => actual = args)
            };

            await subject.InvokeClick(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeContextMenu_Invokes_OnContextMenu_Handler() {
            MouseEventArgs expected = new();
            MouseEventArgs? actual = null;
            var subject = new GlobalEventHandler() {
                OnContextMenu = EventCallback.Factory.Create<MouseEventArgs>(this, (args) => actual = args)
            };

            await subject.InvokeContextMenu(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeDoubleClick_Invokes_OnDoubleClick_Handler() {
            MouseEventArgs expected = new();
            MouseEventArgs? actual = null;
            var subject = new GlobalEventHandler() {
                OnDoubleClick = EventCallback.Factory.Create<MouseEventArgs>(this, (args) => actual = args)
            };

            await subject.InvokeDoubleClick(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeMouseDown_Invokes_OnMouseDown_Handler() {
            MouseEventArgs expected = new();
            MouseEventArgs? actual = null;
            var subject = new GlobalEventHandler() {
                OnMouseDown = EventCallback.Factory.Create<MouseEventArgs>(this, (args) => actual = args)
            };

            await subject.InvokeMouseDown(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeMouseUp_Invokes_OnMouseUp_Handler() {
            MouseEventArgs expected = new();
            MouseEventArgs? actual = null;
            var subject = new GlobalEventHandler() {
                OnMouseUp = EventCallback.Factory.Create<MouseEventArgs>(this, (args) => actual = args)
            };

            await subject.InvokeMouseUp(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeMouseMove_Invokes_OnMouseMove_Handler() {
            MouseEventArgs expected = new();
            MouseEventArgs? actual = null;
            var subject = new GlobalEventHandler() {
                OnMouseMove = EventCallback.Factory.Create<MouseEventArgs>(this, (args) => actual = args)
            };

            await subject.InvokeMouseMove(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeTouchStart_Invokes_OnTouchStart_Handler() {
            TouchEventArgs expected = new();
            TouchEventArgs? actual = null;
            var subject = new GlobalEventHandler() {
                OnTouchStart = EventCallback.Factory.Create<TouchEventArgs>(this, (args) => actual = args)
            };

            await subject.InvokeTouchStart(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeTouchEnd_Invokes_OnTouchEnd_Handler() {
            TouchEventArgs expected = new();
            TouchEventArgs? actual = null;
            var subject = new GlobalEventHandler() {
                OnTouchEnd = EventCallback.Factory.Create<TouchEventArgs>(this, (args) => actual = args)
            };

            await subject.InvokeTouchEnd(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeTouchCancel_Invokes_OnTouchCancel_Handler() {
            TouchEventArgs expected = new();
            TouchEventArgs? actual = null;
            var subject = new GlobalEventHandler() {
                OnTouchCancel = EventCallback.Factory.Create<TouchEventArgs>(this, (args) => actual = args)
            };

            await subject.InvokeTouchCancel(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeTouchMove_Invokes_OnTouchMove_Handler() {
            TouchEventArgs expected = new();
            TouchEventArgs? actual = null;
            var subject = new GlobalEventHandler() {
                OnTouchMove = EventCallback.Factory.Create<TouchEventArgs>(this, (args) => actual = args)
            };

            await subject.InvokeTouchMove(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeResize_Invokes_OnResize_Handler() {
            ResizeEventArgs expected = new();
            ResizeEventArgs? actual = null;
            var subject = new GlobalEventHandler() {
                OnResize = EventCallback.Factory.Create<ResizeEventArgs>(this, (args) => actual = args)
            };

            await subject.InvokeResize(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeScroll_Invokes_OnScroll_Handler() {
            ScrollEventArgs expected = new();
            ScrollEventArgs? actual = null;
            var subject = new GlobalEventHandler() {
                OnScroll = EventCallback.Factory.Create<ScrollEventArgs>(this, (args) => actual = args)
            };

            await subject.InvokeScroll(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GlobalEventHandler_ModuleLocation_Is_Correct() {
            var fileName = Path.GetFileName(GlobalEventHandler.ModuleLocation);

            // TODO: find a more reliable way to get the location of the javascript module
            var expectedFilePath = Directory.GetFiles(Path.Combine("..", "..", "..", "..", "VDT.Core.Blazor.GlobalEventHandler", "wwwroot"), "globaleventhandler.*.js").Single();
            var expectedFileName = Path.GetFileName(expectedFilePath);

            Assert.Equal(expectedFileName, fileName);
        }

        [Fact]
        public void GlobalEventHandler_Module_Has_Correct_Fingerprint() {
            using var sha256 = SHA256.Create();

            // TODO: find a more reliable way to get the location of the javascript module
            var filePath = Directory.GetFiles(Path.Combine("..", "..", "..", "..", "VDT.Core.Blazor.GlobalEventHandler", "wwwroot"), "globaleventhandler.*.js").Single();
            var fingerprintFinder = new Regex("globaleventhandler\\.([a-f0-9]+)\\.js$", RegexOptions.IgnoreCase);
            var fingerprint = fingerprintFinder.Match(filePath).Groups[1].Value;
            var fileContents = File.ReadAllBytes(filePath).Where(b => b != '\r').ToArray(); // Normalize newlines between Windows and Linux
            var expectedFingerprint = string.Join("", sha256.ComputeHash(fileContents).Take(5).Select(b => b.ToString("x2")));

            Assert.Equal(expectedFingerprint, fingerprint);
        }
    }
}
