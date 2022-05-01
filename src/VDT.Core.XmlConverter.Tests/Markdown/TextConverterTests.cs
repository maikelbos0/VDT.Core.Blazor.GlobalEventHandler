using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class TextConverterTests {
        [Theory]
        [InlineData(false, 0, " Foo ")]
        [InlineData(false, 1, "Foo ")]
        [InlineData(true, 0, "Foo ")]
        [InlineData(true, 1, "Foo ")]
        public void Convert_Trims_As_Needed(bool isFirstChild, int trailingNewLineCount, string expectedValue) {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("\t Foo \t"));
            using var reader = XmlReader.Create(stream, new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Fragment });

            var converter = new TextConverter();
            var nodeData = NodeDataHelper.Create(
                XmlNodeType.Text,
                isFirstChild: isFirstChild,
                additionalData: new Dictionary<string, object?>() {
                    { nameof(ContentTracker.TrailingNewLineCount), trailingNewLineCount }
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
        [InlineData("\r\n\tFoo\\(\\);\r\n\tBar\\(\\);", "Foo();\r\nBar();", "pre", "li")]
        [InlineData("\r\nFoo\\(\\);\r\nBar\\(\\);", "Foo();\r\nBar();", "pre")]
        [InlineData("\r\nFoo\\(\\);\r\nBar\\(\\);", "\r\nFoo();\r\nBar();", "pre")]
        public void Convert_Indents_Without_Normalization_In_Pre(string expectedText, string xml, params string[] ancestorElementNames) {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream, new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Fragment });

            var converter = new TextConverter();
            var nodeData = NodeDataHelper.Create(
                XmlNodeType.Text,
                ancestorElementNames.Select(n => ElementDataHelper.Create(n))
            );

            reader.Read(); // Move to text

            converter.Convert(reader, writer, nodeData);

            Assert.Equal(expectedText, writer.ToString());
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
