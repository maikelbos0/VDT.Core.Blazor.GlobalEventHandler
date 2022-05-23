using System.Collections.Generic;
using System.IO;
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

            var converter = new TextConverter();
            var nodeData = NodeDataHelper.Create(
                XmlNodeType.Text,
                value: "\t Foo \t",
                isFirstChild: isFirstChild,
                additionalData: new Dictionary<string, object?>() {
                    { nameof(ContentTracker.TrailingNewLineCount), trailingNewLineCount }
                }
            );

            converter.Convert(writer, nodeData);

            Assert.Equal(expectedValue, writer.ToString());
        }

        [Fact]
        public void Convert_Normalizes_Whitespace() {
            using var writer = new StringWriter();

            var converter = new TextConverter();
            var nodeData = NodeDataHelper.Create(
                XmlNodeType.Text,
                value: "Foo \t \r\n \n\r bar \n\t\r baz"
            );

            converter.Convert(writer, nodeData);

            Assert.Equal("Foo bar baz", writer.ToString());
        }

        [Theory]
        [InlineData("Foo();\r\nBar(i * j);", "\r\nFoo();\r\nBar(i * j);")]
        [InlineData("\r\nFoo();\r\nBar(i * j);", "\r\nFoo();\r\nBar(i * j);")]
        public void Convert_Pre(string value, string expectedText) {
            using var writer = new StringWriter();

            var converter = new TextConverter();
            var nodeData = NodeDataHelper.Create(
                XmlNodeType.Text,
                value: value,
                ancestors: new List<ElementData>() { ElementDataHelper.Create("pre") }
            );

            converter.Convert(writer, nodeData);

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
        [InlineData("Foo < bar", "Foo &lt; bar")]
        [InlineData("Foo > bar", "Foo &gt; bar")]
        [InlineData("Foo & bar", "Foo &amp; bar")]
        public void Convert_Escapes_Characters(string value, string expectedText) {
            using var writer = new StringWriter();

            var converter = new TextConverter();
            var nodeData = NodeDataHelper.Create(
                XmlNodeType.Text,
                value: value
            );

            converter.Convert(writer, nodeData);

            Assert.Equal(expectedText, writer.ToString());
        }
    }
}
