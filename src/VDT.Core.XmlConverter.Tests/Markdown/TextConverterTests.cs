using System.IO;
using System.Text;
using System.Xml;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class TextConverterTests {
        [Fact]
        public void Convert() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("\t Test"));
            using var reader = XmlReader.Create(stream, new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Fragment });

            var converter = new TextConverter();

            reader.Read(); // Move to text

            converter.Convert(reader, writer);

            Assert.Equal("Test\r\n", writer.ToString());
        }

        [Theory]
        [InlineData("Foo \\ bar", "Foo \\\\ bar\r\n")]
        [InlineData("Foo ` bar", "Foo \\` bar\r\n")]
        [InlineData("Foo * bar", "Foo \\* bar\r\n")]
        [InlineData("Foo _ bar", "Foo \\_ bar\r\n")]
        [InlineData("Foo { bar", "Foo \\{ bar\r\n")]
        [InlineData("Foo } bar", "Foo \\} bar\r\n")]
        [InlineData("Foo [ bar", "Foo \\[ bar\r\n")]
        [InlineData("Foo ] bar", "Foo \\] bar\r\n")]
        [InlineData("Foo ( bar", "Foo \\( bar\r\n")]
        [InlineData("Foo ) bar", "Foo \\) bar\r\n")]
        [InlineData("Foo # bar", "Foo \\# bar\r\n")]
        [InlineData("Foo + bar", "Foo \\+ bar\r\n")]
        [InlineData("Foo - bar", "Foo \\- bar\r\n")]
        [InlineData("Foo . bar", "Foo \\. bar\r\n")]
        [InlineData("Foo ! bar", "Foo \\! bar\r\n")]
        [InlineData("Foo | bar", "Foo \\| bar\r\n")]
        [InlineData("Foo &lt; bar", "Foo &lt; bar\r\n")]
        [InlineData("Foo &gt; bar", "Foo &gt; bar\r\n")]
        [InlineData("Foo &amp; bar", "Foo &amp; bar\r\n")]
        public void Convert_Escapes_Characters(string xml, string expectedText) {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream, new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Fragment });

            var converter = new TextConverter();

            reader.Read(); // Move to text

            converter.Convert(reader, writer);

            Assert.Equal(expectedText, writer.ToString());
        }
    }
}
