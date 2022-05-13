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
        public void RenderStart() {
            using var writer = new StringWriter();
            var converter = new HyperlinkConverter();

            converter.RenderStart(ElementDataHelper.Create("a"), writer);

            Assert.Equal("[", writer.ToString());
        }

        [Theory]
        [InlineData(null, null, "]()")]
        [InlineData("https://www.google.com", null, "](https://www.google.com)")]
        [InlineData("https://www.google.com", "Google.com", "](https://www.google.com \"Google.com\")")]
        public void RenderEnd(string href, string title, string expectedOutput) {
            using var writer = new StringWriter();

            var converter = new HyperlinkConverter();
            var attributes = new Dictionary<string, string>();

            if (href != null) {
                attributes["href"] = href;
            }

            if (title != null) {
                attributes["title"] = title;
            }

            converter.RenderEnd(ElementDataHelper.Create("a", attributes: attributes), writer);

            Assert.Equal(expectedOutput, writer.ToString());
        }

        [Fact]
        public void RenderEnd_Without_Url() {
            using var writer = new StringWriter();
            var converter = new HyperlinkConverter();

            converter.RenderEnd(ElementDataHelper.Create("a"), writer);

            Assert.Equal("]()", writer.ToString());
        }
    }
}
