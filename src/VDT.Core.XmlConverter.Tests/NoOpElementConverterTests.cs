using System.Collections.Generic;
using System.IO;
using Xunit;

namespace VDT.Core.XmlConverter.Tests {
    public class NoOpElementConverterTests {
        [Fact]
        public void IsValidFor() {
            var converter = new NoOpElementConverter();

            Assert.True(converter.IsValidFor(ElementDataHelper.Create("bar")));
        }

        [Theory]
        [InlineData(false, "<bar baz=\"baz\" foo=\"&quot;foo&quot;\" quux=\"qu&amp;ux\">")]
        [InlineData(true, "<bar baz=\"baz\" foo=\"&quot;foo&quot;\" quux=\"qu&amp;ux\"/>")]
        public void RenderStart(bool isSelfClosing, string expectedOutput) {
            using var writer = new StringWriter();
            var converter = new NoOpElementConverter();

            converter.RenderStart(ElementDataHelper.Create(
                "bar",
                attributes: new Dictionary<string, string>() {
                    { "baz", "baz" },
                    { "foo", "\"foo\"" },
                    { "quux", "qu&ux" }
                },
                isSelfClosing: isSelfClosing
            ), writer);

            Assert.Equal(expectedOutput, writer.ToString());
        }

        [Theory]
        [InlineData(false, true)]
        [InlineData(true, false)]
        public void ShouldRenderContent(bool isSelfClosing, bool expectedShouldRenderContent) {
            var converter = new NoOpElementConverter();

            Assert.Equal(expectedShouldRenderContent, converter.ShouldRenderContent(ElementDataHelper.Create("bar", isSelfClosing: isSelfClosing)));
        }

        [Theory]
        [InlineData(false, "</bar>")]
        [InlineData(true, "")]
        public void RenderEnd_Renders_EndOuput_When_IsSelfClosing_Is_False(bool isSelfClosing, string expectedOutput) {
            using var writer = new StringWriter();
            var converter = new NoOpElementConverter();

            converter.RenderEnd(ElementDataHelper.Create("bar", isSelfClosing: isSelfClosing), writer);

            Assert.Equal(expectedOutput, writer.ToString());
        }
    }
}
