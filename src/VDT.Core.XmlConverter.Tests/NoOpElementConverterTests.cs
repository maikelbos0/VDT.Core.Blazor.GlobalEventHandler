using System.Collections.Generic;
using System.IO;
using Xunit;

namespace VDT.Core.XmlConverter.Tests {
    public class NoOpElementConverterTests {
        [Fact]
        public void IsValidFor_Returns_True() {
            var converter = new NoOpElementConverter();

            Assert.True(converter.IsValidFor(ElementDataHelper.Create("bar")));
        }

        [Fact]
        public void RenderStart_Renders_StartOuput() {
            using var writer = new StringWriter();
            var converter = new NoOpElementConverter();

            converter.RenderStart(ElementDataHelper.Create(
                "bar", 
                attributes: new Dictionary<string, string>() {
                    { "baz", "baz" },
                    { "foo", "\"foo\"" },
                    { "quux", "qu&ux" }
                }
            ), writer);

            Assert.Equal("<bar baz=\"baz\" foo=\"&quot;foo&quot;\" quux=\"qu&amp;ux\">", writer.ToString());
        }

        [Fact]
        public void RenderStart_Renders_SelfClosing_StartOuput_IsSelfClosing_Is_True() {
            using var writer = new StringWriter();
            var converter = new NoOpElementConverter();

            converter.RenderStart(ElementDataHelper.Create("bar", isSelfClosing: true), writer);

            Assert.Equal("<bar/>", writer.ToString());
        }

        [Fact]
        public void ShouldRenderContent_Returns_True_When_IsSelfClosing_Is_False() {
            var converter = new NoOpElementConverter();

            Assert.True(converter.ShouldRenderContent(ElementDataHelper.Create("bar")));
        }

        [Fact]
        public void ShouldRenderContent_Returns_False_When_IsSelfClosing_Is_True() {
            var converter = new NoOpElementConverter();

            Assert.False(converter.ShouldRenderContent(ElementDataHelper.Create("bar", isSelfClosing: true)));
        }

        [Fact]
        public void RenderEnd_Renders_EndOuput_When_IsSelfClosing_Is_False() {
            using var writer = new StringWriter();
            var converter = new NoOpElementConverter();

            converter.RenderEnd(ElementDataHelper.Create("bar"), writer);

            Assert.Equal("</bar>", writer.ToString());
        }
    }
}
