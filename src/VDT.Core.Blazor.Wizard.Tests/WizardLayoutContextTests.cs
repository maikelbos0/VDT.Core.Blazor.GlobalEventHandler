using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using Xunit;

namespace VDT.Core.Blazor.Wizard.Tests {
    public class WizardLayoutContextTests {
        // These tests are likely fragile; if they break when updating Microsoft.AspNetCore.Components it may be a a good idea to wrap the RenderTreeBuilder.
        // This would allow mocking as well, which could lead to clearer tests.

#pragma warning disable BL0006 // Do not use RenderTree types
        [Fact]
        public void WizardLayoutContext_Wizard_Renders_When_Active() {
            var wizard = new Wizard() {
                ActiveStepIndex = 0
            };
            var context = new WizardLayoutContext(wizard);
            var builder = new RenderTreeBuilder();

            context.Wizard(builder);

            var frames = builder.GetFrames();

            Assert.Equal(3, frames.Count);

            Assert.Equal(RenderTreeFrameType.Component, frames.Array[0].FrameType);
            Assert.Equal(typeof(CascadingValue<Wizard>), frames.Array[0].ComponentType);

            Assert.Equal(RenderTreeFrameType.Attribute, frames.Array[1].FrameType);
            Assert.Equal(wizard, frames.Array[1].AttributeValue);

            Assert.Equal(RenderTreeFrameType.Attribute, frames.Array[2].FrameType);
            Assert.Equal(context.WizardContent, frames.Array[2].AttributeValue);
        }

        [Fact]
        public void WizardLayoutContext_Wizard_Does_Not_Render_When_Active() {
            var wizard = new Wizard();
            var context = new WizardLayoutContext(wizard);
            var builder = new RenderTreeBuilder();

            context.Wizard(builder);

            var frames = builder.GetFrames();

            Assert.Equal(0, frames.Count);
        }
#pragma warning restore BL0006 // Do not use RenderTree types
    }
}
