using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class TextConverterTests {
        [Theory]
        [InlineData(false, false, " Foo ")]
        [InlineData(false, true, "Foo ")]
        [InlineData(true, false, "Foo ")]
        [InlineData(true, true, "Foo ")]
        public void Convert_Trims_As_Needed(bool isFirstChild, bool hasTrailingNewLine, string expectedValue) {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("\t Foo \t"));
            using var reader = XmlReader.Create(stream, new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Fragment });

            var converter = new TextConverter();
            var nodeData = NodeDataHelper.Create(
                XmlNodeType.Text,
                isFirstChild: isFirstChild,
                additionalData: new Dictionary<string, object?>() {
                    { nameof(ContentTracker.HasTrailingNewLine), hasTrailingNewLine }
                }
            );

            reader.Read(); // Move to text

            converter.Convert(reader, writer, nodeData);

            Assert.Equal(expectedValue, writer.ToString());
        }

        [Fact]
        public void Convert_Normalizes_Whitespace() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Foo \t \r\n \n\r bar \n\t\r baz"));
            using var reader = XmlReader.Create(stream, new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Fragment });

            var converter = new TextConverter();

            reader.Read(); // Move to text

            converter.Convert(reader, writer, NodeDataHelper.Create(XmlNodeType.Text));

            Assert.Equal("Foo bar baz", writer.ToString());
        }

        [Theory]
        [InlineData("Foo \\ bar", "Foo \\\\ bar")]
        [InlineData("Foo ` bar", "Foo \\` bar")]
        [InlineData("Foo * bar", "Foo \\* bar")]
        [InlineData("Foo _ bar", "Foo \\_ bar")]
        [InlineData("Foo { bar", "Foo \\{ bar")]
        [InlineData("Foo } bar", "Foo \\} bar")]
        [InlineData("Foo [ bar", "Foo \\[ bar")]
        [InlineData("Foo ] bar", "Foo \\] bar")]
        [InlineData("Foo ( bar", "Foo \\( bar")]
        [InlineData("Foo ) bar", "Foo \\) bar")]
        [InlineData("Foo # bar", "Foo \\# bar")]
        [InlineData("Foo + bar", "Foo \\+ bar")]
        [InlineData("Foo - bar", "Foo \\- bar")]
        [InlineData("Foo . bar", "Foo \\. bar")]
        [InlineData("Foo ! bar", "Foo \\! bar")]
        [InlineData("Foo | bar", "Foo \\| bar")]
        [InlineData("Foo &lt; bar", "Foo &lt; bar")]
        [InlineData("Foo &gt; bar", "Foo &gt; bar")]
        [InlineData("Foo &amp; bar", "Foo &amp; bar")]
        public void Convert_Escapes_Characters(string xml, string expectedText) {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream, new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Fragment });

            var converter = new TextConverter();

            reader.Read(); // Move to text

            converter.Convert(reader, writer, NodeDataHelper.Create(XmlNodeType.Text));

            Assert.Equal(expectedText, writer.ToString());
        }
    }
}
