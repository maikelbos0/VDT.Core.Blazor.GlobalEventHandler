using System.IO;
using System.Text;
using System.Xml;
using Xunit;

namespace VDT.Core.XmlConverter.Tests {
    public class XmlReaderExtensionsTests {
        [Fact]
        public void GetAttributes_Returns_Element_Attributes_When_Present() {
            const string xml = "<foo bar=\"bar\" baz=\"quux quux\">Content</foo>";

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream);
                        
            reader.Read(); // Move to element

            var attributes = reader.GetAttributes();

            Assert.Equal(2, attributes.Count);
            Assert.Equal("bar", attributes["bar"]);
            Assert.Equal("quux quux", attributes["baz"]);
        }

        [Fact]
        public void GetAttributes_Returns_Empty_Dictionary_For_Element_Without_Attributes() {
            const string xml = "<foo>Content</foo>";

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream);

            reader.Read(); // Move to element

            Assert.Empty(reader.GetAttributes());
        }

        [Fact]
        public void GetAttributes_Returns_Empty_Dictionary_For_Other_NodeType() {
            const string xml = "<foo bar=\"bar\">Content</foo>";

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream);

            reader.Read(); // Move to element            
            reader.Read(); // Move to text

            Assert.Empty(reader.GetAttributes());
        }
    }
}
