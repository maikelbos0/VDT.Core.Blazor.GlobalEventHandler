using System.IO;
using System.Text;
using System.Xml;
using Xunit;

namespace VDT.Core.XmlConverter.Tests {
    public class FormattingNodeConverterTests {
        [Fact]
        public void Convert_Uses_Formatter() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<?name value?>"));
            using var reader = XmlReader.Create(stream);

            var converter = new FormattingNodeConverter((name, value) => $"[{name}]='{value}'", false);

            reader.Read(); // Move to processing instruction

            converter.Convert(reader, writer);

            Assert.Equal("[name]='value'", writer.ToString());
        }

        [Theory]
        [InlineData(true, "Bar &amp; Baz")]
        [InlineData(false, "Bar & Baz")]
        public void Convert_XmlEncodes_Value_When_Enabled(bool xmlEncodeValue, string expectedValue) {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<foo>Bar &amp; Baz</foo>"));
            using var reader = XmlReader.Create(stream);

            var converter = new FormattingNodeConverter((name, value) => value, xmlEncodeValue);

            reader.Read(); // Move to element
            reader.Read(); // Move to text

            converter.Convert(reader, writer);

            Assert.Equal(expectedValue, writer.ToString());
        }
    }
}
