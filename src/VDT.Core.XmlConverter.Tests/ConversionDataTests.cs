using System.Collections.Generic;
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
        [InlineData("<!-- Test -->", XmlNodeType.Comment)]
        [InlineData("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", XmlNodeType.XmlDeclaration)]
        [InlineData("<?xml-stylesheet type=\"text/xsl\" href=\"style.xsl\"?>", XmlNodeType.ProcessingInstruction)]
        public void ReadNode_Sets_CurrentNodeData_If_Not_Element(string xml, XmlNodeType expectedNodeType) {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream);

            var data = new ConversionData();

            reader.Read(); // Move to node

            data.ReadNode(reader);

            Assert.NotNull(data.CurrentNodeData);
            Assert.Equal(expectedNodeType, data.CurrentNodeData?.NodeType);

            Assert.Null(data.CurrentElementData);
        }

        [Theory]
        [InlineData("<foo bar=\"baz\">Content</foo>", "foo", 1, false)]
        [InlineData("<foo bar=\"baz\"/>", "foo", 1, true)]
        [InlineData("<foo>Content</foo>", "foo", 0, false)]
        [InlineData("<foo/>", "foo", 0, true)]
        public void ReadNode_Sets_CurrentElementData_If_Element(string xml, string expectedName, int expectedAttributeCount, bool expectedIsSelfClosing) {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream);

            var data = new ConversionData();

            reader.Read(); // Move to element

            data.ReadNode(reader);

            Assert.NotNull(data.CurrentElementData);
            Assert.Equal(expectedName, data.CurrentElementData?.Name);
            Assert.Equal(expectedAttributeCount, data.CurrentElementData?.Attributes.Count);
            Assert.Equal(expectedIsSelfClosing, data.CurrentElementData?.IsSelfClosing);

            Assert.Null(data.CurrentNodeData);
        }

        [Theory]
        [InlineData("<foo><bar><baz/></bar></foo>", true)]
        [InlineData("<foo><bar>Content</bar></foo>", false)]
        public void ReadNode_Adds_Ancestors_When_Depth_Increases(string xml, bool isElement) {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream);

            var data = new ConversionData();
            var ancestors = new Stack<ElementData>();

            ancestors.Push(ReadNextElement(reader, data)); // Grandparent
            ancestors.Push(ReadNextElement(reader, data)); // Parent

            reader.Read(); // move to inner node

            data.ReadNode(reader);

            Assert.Equal(ancestors, data.ElementAncestors);

            if (isElement) {
                Assert.Equal(ancestors, data.CurrentElementData?.Ancestors);
            }
            else {
                Assert.Equal(ancestors, data.CurrentNodeData?.Ancestors);
            }
        }

        [Fact]
        public void ReadNode_Removes_Ancestor_When_Depth_Decreases() {
            const string xml = "<foo><bar><baz/></bar><bar/></foo><foo/>";

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream, new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Fragment });

            var data = new ConversionData();

            var ancestor = ReadNextElement(reader, data); // Grandparent foo

            ReadNextElement(reader, data); // Parent bar
            ReadNextElement(reader, data); // Child baz
            ReadNextElement(reader, data); // Parent sibling bar

            Assert.Equal(ancestor, Assert.Single(data.ElementAncestors));

            ReadNextElement(reader, data); // Grandparent sibling foo

            Assert.Empty(data.ElementAncestors);
        }

        [Fact]
        public void ReadNode_Leaves_Ancestor_When_Depth_Is_Unchanged() {
            const string xml = "<foo><bar/><bar/></foo>";

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream);

            var data = new ConversionData();

            var ancestor = ReadNextElement(reader, data); // Grandparent foo

            ReadNextElement(reader, data); // Bar 1

            Assert.Equal(ancestor, Assert.Single(data.ElementAncestors));

            ReadNextElement(reader, data); // Bar 2

            Assert.Equal(ancestor, Assert.Single(data.ElementAncestors));
        }

        private ElementData ReadNextElement(XmlReader reader, ConversionData data) {
            reader.Read();

            while (reader.NodeType == XmlNodeType.EndElement) {
                reader.Read();
            }

            data.ReadNode(reader);
            Assert.NotNull(data.CurrentElementData);

            return data.CurrentElementData!;
        }
    }
}
