using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class MarkdownConverterOptionsExtensionsTests {
        [Theory]
        [InlineData("<h1>Heading 1</h1>", "# Heading 1\r\n\r\n")]
        [InlineData("<h2>Heading 2</h2>", "## Heading 2\r\n\r\n")]
        [InlineData("<h3>Heading 3</h3>", "### Heading 3\r\n\r\n")]
        [InlineData("<h4>Heading 4</h4>", "#### Heading 4\r\n\r\n")]
        [InlineData("<h5>Heading 5</h5>", "##### Heading 5\r\n\r\n")]
        [InlineData("<h6>Heading 6</h6>", "###### Heading 6\r\n\r\n")]
        public void UseMarkdown_Converts_Header(string xml, string expectedMarkdown) {
            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            Assert.Equal(expectedMarkdown, converter.Convert(xml));
        }

        [Theory]
        [InlineData("<strong>Bold</strong>", "**Bold**")]
        [InlineData("<b>Bold</b>", "**Bold**")]
        [InlineData("<em>Italic</em>", "*Italic*")]
        [InlineData("<i>Italic</i>", "*Italic*")]
        public void UseMarkdown_Converts_Emphasis(string xml, string expectedMarkdown) {
            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            Assert.Equal(expectedMarkdown, converter.Convert(xml));
        }
    }
}
