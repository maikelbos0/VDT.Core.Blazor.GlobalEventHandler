using System.Collections.Generic;
using System.IO;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class HyperlinkConverterTests {
        [Theory]
        [InlineData("a", true)]
        [InlineData("A", true)]
        [InlineData("foo", false)]
        public void IsValidFor(string elementName, bool expectedIsValid) {
            var converter = new HyperlinkConverter();

            Assert.Equal(expectedIsValid, converter.IsValidFor(ElementDataHelper.Create(elementName)));
        }

        [Fact]
        public void RenderStart_Renders_StartOuput() {
            using var writer = new StringWriter();
            var converter = new HyperlinkConverter();

            converter.RenderStart(ElementDataHelper.Create("a"), writer);

            Assert.Equal("[", writer.ToString());
        }

        [Fact]
        public void RenderEnd_Renders_EndOuput() {
            using var writer = new StringWriter();

            var converter = new HyperlinkConverter();
            var elementData = ElementDataHelper.Create(
                "a",
                attributes: new Dictionary<string, string>() {
                    { "href", "https://www.google.com" }
                }
            );

            converter.RenderEnd(elementData, writer);

            Assert.Equal("](https://www.google.com)", writer.ToString());
        }

        [Fact]
        public void RenderEnd_Renders_EndOuput_Without_Url() {
            using var writer = new StringWriter();
            var converter = new HyperlinkConverter();

            converter.RenderEnd(ElementDataHelper.Create("a"), writer);

            Assert.Equal("]()", writer.ToString());
        }
    }
}
