using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Xunit;

namespace VDT.Core.XmlConverter.Tests {
    public class ConversionDataTests {
        [Theory]
        [InlineData("<!-- Test -->", XmlNodeType.Comment)]
        [InlineData("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", XmlNodeType.XmlDeclaration)]
        [InlineData("<?xml-stylesheet type=\"text/xsl\" href=\"style.xsl\"?>", XmlNodeType.ProcessingInstruction)]
        public void ReadNode_Sets_CurrentNodeData_To_NodeData_If_Not_Element(string xml, XmlNodeType expectedNodeType) {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream);

            var data = new ConversionData();

            reader.Read(); // Move to node
            data.AdditionalData["test"] = "test";

            data.ReadNode(reader);

            var nodeData = Assert.IsType<NodeData>(data.CurrentNodeData);

            Assert.Equal(expectedNodeType, nodeData.NodeType);
            Assert.True(nodeData.IsFirstChild);
            Assert.True(nodeData.AdditionalData.ContainsKey("test"));
            Assert.Equal("test", nodeData.AdditionalData["test"]);
        }

        [Theory]
        [InlineData("<foo bar=\"baz\">Content</foo>", "foo", 1, false)]
        [InlineData("<foo bar=\"baz\"/>", "foo", 1, true)]
        [InlineData("<foo>Content</foo>", "foo", 0, false)]
        [InlineData("<foo/>", "foo", 0, true)]
        public void ReadNode_Sets_CurrentElementData_To_ElementData_If_Element(string xml, string expectedName, int expectedAttributeCount, bool expectedIsSelfClosing) {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream);

            var data = new ConversionData();

            reader.Read(); // Move to element
            data.AdditionalData["test"] = "test";

            data.ReadNode(reader);

            var elementData = Assert.IsType<ElementData>(data.CurrentNodeData);

            Assert.Equal(expectedName, elementData.Name);
            Assert.Equal(expectedAttributeCount, elementData.Attributes.Count);
            Assert.Equal(expectedIsSelfClosing, elementData.IsSelfClosing);
            Assert.True(elementData.IsFirstChild);
            Assert.True(elementData.AdditionalData.ContainsKey("test"));
            Assert.Equal("test", elementData.AdditionalData["test"]);
        }

        [Fact]
        public void ReadNode_Sets_IsFirstChild_True_For_First_Node() {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<foo/><foo/>"));
            using var reader = XmlReader.Create(stream, new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Fragment });

            var data = new ConversionData();

            data.ReadNode(reader); // First element

            Assert.True(data.CurrentNodeData.IsFirstChild);
        }

        [Fact]
        public void ReadNode_Sets_IsFirstChild_False_For_Not_First_Node() {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<foo/><foo/>"));
            using var reader = XmlReader.Create(stream, new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Fragment });

            var data = new ConversionData();

            ReadNextElement(reader, data); // First element
            ReadNextElement(reader, data); // Next element

            Assert.False(data.CurrentNodeData.IsFirstChild);
        }

        [Fact]
        public void ReadNode_Sets_IsFirstChild_True_For_First_Child() {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<foo><bar/><bar/></foo>"));
            using var reader = XmlReader.Create(stream, new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Fragment });

            var data = new ConversionData();

            ReadNextElement(reader, data); // Parent element
            ReadNextElement(reader, data); // First child element

            Assert.True(data.CurrentNodeData.IsFirstChild);
        }

        [Fact]
        public void ReadNode_Sets_IsFirstChild_False_For_Not_First_Child() {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<foo><bar/><bar/></foo>"));
            using var reader = XmlReader.Create(stream, new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Fragment });

            var data = new ConversionData();

            ReadNextElement(reader, data); // Parent element
            ReadNextElement(reader, data); // First child element
            ReadNextElement(reader, data); // Next child element

            Assert.False(data.CurrentNodeData.IsFirstChild);
        }

        [Theory]
        [InlineData("<foo><bar><baz/></bar></foo>")]
        [InlineData("<foo><bar>Content</bar></foo>")]
        public void ReadNode_Adds_Ancestors_When_Depth_Increases(string xml) {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream);

            var data = new ConversionData();
            var ancestors = new Stack<ElementData>();

            ancestors.Push(ReadNextElement(reader, data)); // Grandparent
            ancestors.Push(ReadNextElement(reader, data)); // Parent

            reader.Read(); // move to inner node

            data.ReadNode(reader);

            Assert.Equal(ancestors, data.Ancestors);
            Assert.Equal(ancestors, data.CurrentNodeData.Ancestors);
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

            Assert.Equal(ancestor, Assert.Single(data.Ancestors));

            ReadNextElement(reader, data); // Grandparent sibling foo

            Assert.Empty(data.Ancestors);
        }

        [Fact]
        public void ReadNode_Leaves_Ancestor_When_Depth_Is_Unchanged() {
            const string xml = "<foo><bar/><bar/></foo>";

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream);

            var data = new ConversionData();

            var ancestor = ReadNextElement(reader, data); // Grandparent foo

            ReadNextElement(reader, data); // Bar 1

            Assert.Equal(ancestor, Assert.Single(data.Ancestors));

            ReadNextElement(reader, data); // Bar 2

            Assert.Equal(ancestor, Assert.Single(data.Ancestors));
        }

        private ElementData ReadNextElement(XmlReader reader, ConversionData data) {
            reader.Read();

            while (reader.NodeType == XmlNodeType.EndElement) {
                reader.Read();
            }

            data.ReadNode(reader);

            return Assert.IsType<ElementData>(data.CurrentNodeData);
        }
    }
}
