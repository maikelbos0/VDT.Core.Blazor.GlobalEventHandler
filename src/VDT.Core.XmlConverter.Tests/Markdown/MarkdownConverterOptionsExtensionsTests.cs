using System.IO;
using System.Text;
using System.Xml;
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
        public void UseMarkdown_Converts_Inline_Markup(string xml, string expectedMarkdown) {
            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            Assert.Equal(expectedMarkdown, converter.Convert(xml));
        }

        [Theory]
        [InlineData("Linebreak<br/>", "Linebreak  \r\n")]
        [InlineData("<p>Paragraph</p>", "Paragraph\r\n\r\n")]
        public void UseMarkdown_Converts_Newlines(string xml, string expectedMarkdown) {
            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            Assert.Equal(expectedMarkdown, converter.Convert(xml));
        }

        [Fact]
        public void UseMarkdown_Removes_All_Unneeded_Whitespace() {
            const string xml = "<p xml:space=\"preserve\">\t Test \t</p>\r\n\t <p> Test \t </p>";

            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            Assert.Equal("Test\r\n\r\nTest\r\n\r\n", converter.Convert(xml));
        }

        [Theory]
        [InlineData("<?xml-stylesheet type=\"text/xsl\" href=\"style.xsl\"?>Test", "Test")]
        [InlineData("<?xml version=\"1.0\" encoding=\"UTF-8\"?>Test", "Test")]
        [InlineData("<![CDATA[Content]]>Test", "Test")]
        public void UseMarkdown_Removes_All_Unconvertible_Node_Types(string xml, string expectedMarkdown) {
            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            Assert.Equal(expectedMarkdown, converter.Convert(xml));
        }

        [Fact]
        public void UseMarkdown_Removes_Document_Type_Declarations() {
            const string xml = "<!DOCTYPE foo [ <!ENTITY val \"bar\"> ]><p>Test</p>";

            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream, new XmlReaderSettings() { DtdProcessing = DtdProcessing.Parse });

            Assert.Equal("Test\r\n\r\n", converter.Convert(reader));
        }
    }
}
