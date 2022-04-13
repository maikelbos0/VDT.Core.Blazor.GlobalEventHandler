using System.IO;
using System.Text;
using System.Xml;
using Xunit;

namespace VDT.Core.XmlConverter.Tests {
    public class ConversionDataTests {
        [Theory]
        [InlineData("<foo/>", XmlNodeType.Element)]
        [InlineData("<!-- Test -->", XmlNodeType.Comment)]
        [InlineData("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", XmlNodeType.XmlDeclaration)]
        [InlineData("<?xml-stylesheet type=\"text/xsl\" href=\"style.xsl\"?>", XmlNodeType.ProcessingInstruction)]
        public void ReadNode_Sets_NodeType(string xml, XmlNodeType expectedNodeType) {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream);

            var data = new ConversionData();

            reader.Read(); // Move to node

            data.ReadNode(reader);

            Assert.Equal(expectedNodeType, data.CurrentNodeType);
        }

        [Theory]
        [InlineData("<!-- Test -->")]
        [InlineData("<?xml version=\"1.0\" encoding=\"UTF-8\"?>")]
        [InlineData("<?xml-stylesheet type=\"text/xsl\" href=\"style.xsl\"?>")]
        public void ReadNode_Clears_CurrentElement_If_Needed(string xml) {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream);

            var data = new ConversionData();

            reader.Read(); // Move to node

            data.ReadNode(reader);

            Assert.Null(data.CurrentElement);
        }

        [Theory]
        [InlineData("<foo bar=\"baz\">Content</foo>", "foo", 1, false)]
        [InlineData("<foo bar=\"baz\"/>", "foo", 1, true)]
        [InlineData("<foo>Content</foo>", "foo", 0, false)]
        [InlineData("<foo/>", "foo", 0, true)]
        public void ReadNode_Sets_ElementData(string xml, string expectedName, int expectedAttributeCount, bool expectedIsSelfClosing) {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream);

            var data = new ConversionData();

            reader.Read(); // Move to element

            data.ReadNode(reader);

            Assert.NotNull(data.CurrentElement);
            Assert.Equal(expectedName, data.CurrentElement?.Name);
            Assert.Equal(expectedAttributeCount, data.CurrentElement?.Attributes.Count);
            Assert.Equal(expectedIsSelfClosing, data.CurrentElement?.IsSelfClosing);
        }
    }
}
