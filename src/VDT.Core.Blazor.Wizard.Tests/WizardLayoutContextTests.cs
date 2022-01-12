using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using Xunit;

namespace VDT.Core.Blazor.Wizard.Tests {
    public class WizardLayoutContextTests {
        // These tests are likely fragile; if they break when updating Microsoft.AspNetCore.Components it may be a a good idea to wrap the RenderTreeBuilder.
        // This would allow mocking as well, which could lead to clearer tests.

#pragma warning disable BL0006 // Do not use RenderTree types
        public class TestContext {
            public static TestContext CreateTestContext(Wizard wizard, Func<WizardLayoutContext, RenderFragment> getRenderMethod) {
                var context = new WizardLayoutContext(wizard);
                var builder = new RenderTreeBuilder();

                getRenderMethod(context)(builder);

                return new TestContext(context, builder.GetFrames());
            }

            private readonly WizardLayoutContext context;
            private readonly ArrayRange<RenderTreeFrame> frames;

            public TestContext(WizardLayoutContext context, ArrayRange<RenderTreeFrame> frames) {
                this.context = context;
                this.frames = frames;
            }

            public void AssertFrameCount(int expectedFrameCount) {
                Assert.Equal(expectedFrameCount, frames.Count);
            }

            public void AssertComponent<TComponent>(int index) {
                Assert.Equal(RenderTreeFrameType.Component, frames.Array[index].FrameType);
                Assert.Equal(typeof(TComponent), frames.Array[index].ComponentType);
            }

            public void AssertAttribute(int index, string attributeName, Func<WizardLayoutContext, object> getAttributeValue) {
                AssertAttribute(index, attributeName, getAttributeValue(context));
            }

            public void AssertAttribute(int index, string attributeName, object attributeValue) {
                Assert.Equal(RenderTreeFrameType.Attribute, frames.Array[index].FrameType);
                Assert.Equal(attributeName, frames.Array[index].AttributeName);
                Assert.Equal(attributeValue, frames.Array[index].AttributeValue);
            }
        }
#pragma warning restore BL0006 // Do not use RenderTree types

        [Fact]
        public void WizardLayoutContext_Wizard_Renders_When_Active() {
            var wizard = new Wizard() {
                ActiveStepIndex = 0
            };
            var context = TestContext.CreateTestContext(wizard, c => c.Wizard);

            context.AssertFrameCount(3);
            context.AssertComponent<CascadingValue<Wizard>>(0);
            context.AssertAttribute(1, "Value", wizard);
            context.AssertAttribute(2, "ChildContent", c => c.WizardContent);
        }

        [Fact]
        public void WizardLayoutContext_Wizard_Does_Not_Render_When_Active() {
            var wizard = new Wizard();
            var context = TestContext.CreateTestContext(wizard, c => c.Wizard);

            context.AssertFrameCount(0);
        }
    }
}
