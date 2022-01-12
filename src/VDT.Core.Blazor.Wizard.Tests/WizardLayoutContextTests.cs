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

            public void AssertComponent<TComponent>(int index, int innerFrameCount) {
                Assert.Equal(RenderTreeFrameType.Component, frames.Array[index].FrameType);
                Assert.Equal(typeof(TComponent), frames.Array[index].ComponentType);
                Assert.Equal(innerFrameCount, frames.Array[index].ComponentSubtreeLength);
            }

            public void AssertAttribute(int index, string attributeName, Func<WizardLayoutContext, object> getAttributeValue) {
                AssertAttribute(index, attributeName, getAttributeValue(context));
            }

            public void AssertAttribute(int index, string attributeName, object attributeValue) {
                Assert.Equal(RenderTreeFrameType.Attribute, frames.Array[index].FrameType);
                Assert.Equal(attributeName, frames.Array[index].AttributeName);
                Assert.Equal(attributeValue, frames.Array[index].AttributeValue);
            }

            public void AssertElement(int index, string elementName, int innerFrameCount) {
                Assert.Equal(RenderTreeFrameType.Element, frames.Array[index].FrameType);
                Assert.Equal(elementName, frames.Array[index].ElementName);
                Assert.Equal(innerFrameCount, frames.Array[index].ElementSubtreeLength);
            }

            public void AssertContent(int index, string content) {
                Assert.Equal(RenderTreeFrameType.Text, frames.Array[index].FrameType);
                Assert.Equal(content, frames.Array[index].TextContent);
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
            context.AssertComponent<CascadingValue<Wizard>>(0, 3);
            context.AssertAttribute(1, "Value", wizard);
            context.AssertAttribute(2, "ChildContent", c => c.WizardContent);
        }

        [Fact]
        public void WizardLayoutContext_Wizard_Does_Not_Render_When_Active() {
            var wizard = new Wizard();
            var context = TestContext.CreateTestContext(wizard, c => c.Wizard);

            context.AssertFrameCount(0);
        }

        [Fact]
        public void WizardLayoutContext_WizardContent_Renders_DefaultLayout_Correctly() {
            var wizard = new Wizard() {
                Steps = builder => builder.AddContent(1, "Step")
            };
            var context = TestContext.CreateTestContext(wizard, c => c.WizardContent);

            context.AssertFrameCount(20);
            context.AssertContent(1, "Step");
        }

        [Fact]
        public void WizardLayoutContext_WizardContent_Renders_Layout_Correctly() {
            var wizard = new Wizard() {
                Steps = builder => builder.AddContent(1, "Step"),
                Layout = context => builder => builder.AddContent(1, "Test")
            };
            var context = TestContext.CreateTestContext(wizard, c => c.WizardContent);

            context.AssertFrameCount(4);
            context.AssertContent(1, "Step");
            context.AssertContent(3, "Test");
        }

        [Fact]
        public void WizardLayoutContext_DefaultLayout_Renders_Correctly() {
            var wizard = new Wizard() {
                ActiveStepIndex = 0,
                ContainerClass = "container",
                TitleContainerClass = "title-container",
                TitleContent = builder => builder.AddContent(1, "<h1>Title</h1>"),
                StepTitleContainerClass = "step-title-container",
                ButtonContainerClass = "button-container",
                ContentContainerClass = "content-container"
            };
            wizard.StepsInternal.Add(new WizardStep() { 
                Title = "Step",
                ChildContent = builder => builder.AddContent(1, "Step content")
            });
            var context = TestContext.CreateTestContext(wizard, c => c.DefaultLayout);

            context.AssertFrameCount(29);
            context.AssertElement(0, "div", 29);
            context.AssertAttribute(1, "class", "container");

            context.AssertElement(2, "div", 5);
            context.AssertAttribute(3, "class", "title-container");
            context.AssertContent(6, "<h1>Title</h1>");

            context.AssertElement(7, "div", 6);
            context.AssertAttribute(8, "class", "step-title-container");
            context.AssertContent(12, "Step");

            context.AssertElement(13, "div", 11);
            context.AssertAttribute(14, "class", "button-container");
            context.AssertContent(23, "Finish");

            context.AssertElement(24, "div", 5);
            context.AssertAttribute(25, "class", "content-container");
            context.AssertContent(28, "Step content");
        }
    }
}
