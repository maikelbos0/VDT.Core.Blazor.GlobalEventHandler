using System.IO;
using System.Text;
using System.Xml;
using VDT.Core.XmlConverter.Nodes;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Nodes {
    public class NodeValueConverterTests {
        [Theory]
        [InlineData("<foo>Content</foo>", false, "Content")]
        [InlineData("<foo>Content &amp; this</foo>", false, "Content & this")]
        [InlineData("<foo>Content</foo>", true, "Content")]
        [InlineData("<foo>Content &amp; this</foo>", true, "Content &amp; this")]
        public void Convert_Writes_Value_Correctly(string xml, bool xmlEncode, string expectedValue) {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream);

            var converter = new NodeValueConverter(xmlEncode);

            reader.Read(); // Move to element
            reader.Read(); // Move to text

            converter.Convert(reader, writer);
            Assert.Equal(expectedValue, writer.ToString());
        }
    }
}
