using System.IO;
using System.Xml;
using Xunit;

namespace VDT.Core.XmlConverter.Tests {
    public class FormattingNodeConverterTests {
        [Fact]
        public void Convert_Uses_Formatter() {
            using var writer = new StringWriter();

            var converter = new FormattingNodeConverter((name, value) => $"[{name}]='{value}'", false);

            converter.Convert(writer, NodeDataHelper.Create(XmlNodeType.ProcessingInstruction, "name", "value"));

            Assert.Equal("[name]='value'", writer.ToString());
        }

        [Theory]
        [InlineData(true, "Bar &amp; Baz")]
        [InlineData(false, "Bar & Baz")]
        public void Convert_XmlEncodes_Value_When_Enabled(bool xmlEncodeValue, string expectedValue) {
            using var writer = new StringWriter();

            var converter = new FormattingNodeConverter((name, value) => value, xmlEncodeValue);

            converter.Convert(writer, NodeDataHelper.Create(XmlNodeType.Text, "name", "Bar & Baz"));

            Assert.Equal(expectedValue, writer.ToString());
        }
    }
}
