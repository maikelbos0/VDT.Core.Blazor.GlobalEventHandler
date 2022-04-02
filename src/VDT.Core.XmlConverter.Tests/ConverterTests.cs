using NSubstitute;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using VDT.Core.XmlConverter.Elements;
using VDT.Core.XmlConverter.Nodes;
using Xunit;

namespace VDT.Core.XmlConverter.Tests {
    public class ConverterTests {
        [Fact]
        public void Convert() {
            const string xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<!DOCTYPE foo [ <!ENTITY val ""An value&amp;""> ]>
<?processing instruction?>
<foo bar=""bar &amp; baz"">
    Some content &amp; some more
    <node/>
    <node xml:space=""preserve"">     </node>
    <!-- comment -->
    Content
    <![CDATA[data]]>
</foo>";

            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream, new XmlReaderSettings() { DtdProcessing = DtdProcessing.Parse });

            var converter = new Converter();

            converter.Convert(reader, writer);

            Assert.Equal(xml, writer.ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void ConvertNode_Converts_Element() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<foo bar=\"baz\"></foo>"));
            using var reader = XmlReader.Create(stream);

            var elementConverter = Substitute.For<IElementConverter>();
            var converter = new Converter(new ConverterOptions() {
                DefaultElementConverter = elementConverter
            });

            reader.Read(); // Move to element

            converter.ConvertNode(reader, writer);

            VerifyConverterIsUsed(elementConverter, writer);
        }

        [Fact]
        public void ConvertNode_Converts_Text() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<foo bar=\"baz\">Content</foo>"));
            using var reader = XmlReader.Create(stream);

            var textConverter = Substitute.For<INodeConverter>();
            var converter = new Converter(new ConverterOptions() {
                TextConverter = textConverter
            });

            reader.Read(); // Move to element
            reader.Read(); // Move to text

            converter.ConvertNode(reader, writer);

            textConverter.Received().Convert(reader, writer);
        }

        [Fact]
        public void ConvertNode_Converts_CData() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<foo bar=\"baz\"><![CDATA[Content]]></foo>"));
            using var reader = XmlReader.Create(stream);

            var cDataConverter = Substitute.For<INodeConverter>();
            var converter = new Converter(new ConverterOptions() {
                CDataConverter = cDataConverter
            });

            reader.Read(); // Move to element
            reader.Read(); // Move to CData

            converter.ConvertNode(reader, writer);

            cDataConverter.Received().Convert(reader, writer);
        }

        [Fact]
        public void ConvertNode_Converts_Comment() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<foo bar=\"baz\"><!-- Test --></foo>"));
            using var reader = XmlReader.Create(stream);

            var commentConverter = Substitute.For<INodeConverter>();
            var converter = new Converter(new ConverterOptions() {
                CommentConverter = commentConverter
            });

            reader.Read(); // Move to element
            reader.Read(); // Move to comment

            converter.ConvertNode(reader, writer);

            commentConverter.Received().Convert(reader, writer);
        }

        [Fact]
        public void ConvertNode_Converts_XmlDeclaration() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<?xml version=\"1.0\" encoding=\"UTF-8\"?><foo bar=\"baz\"/>"));
            using var reader = XmlReader.Create(stream);

            var xmlDeclarationConverter = Substitute.For<INodeConverter>();
            var converter = new Converter(new ConverterOptions() {
                XmlDeclarationConverter = xmlDeclarationConverter
            });

            reader.Read(); // Move to xml declaration

            converter.ConvertNode(reader, writer);

            xmlDeclarationConverter.Received().Convert(reader, writer);
        }

        [Fact]
        public void ConvertNode_Converts_Whitespace() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<foo bar=\"baz\"><quux/>    <quux/></foo>"));
            using var reader = XmlReader.Create(stream);

            var whitespaceConverter = Substitute.For<INodeConverter>();
            var converter = new Converter(new ConverterOptions() {
                WhitespaceConverter = whitespaceConverter
            });

            reader.Read(); // Move to element
            reader.Read(); // Move to child element
            reader.Read(); // Move to whitespace

            converter.ConvertNode(reader, writer);

            whitespaceConverter.Received().Convert(reader, writer);
        }

        [Fact]
        public void ConvertNode_Converts_SignificantWhitespace() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<foo bar=\"baz\" xml:space=\"preserve\"><quux/>    <quux/></foo>"));
            using var reader = XmlReader.Create(stream);

            var significantWhitespaceConverter = Substitute.For<INodeConverter>();
            var converter = new Converter(new ConverterOptions() {
                SignificantWhitespaceConverter = significantWhitespaceConverter
            });

            reader.Read(); // Move to element
            reader.Read(); // Move to child element
            reader.Read(); // Move to significant whitespace

            converter.ConvertNode(reader, writer);

            significantWhitespaceConverter.Received().Convert(reader, writer);
        }

        [Fact]
        public void ConvertNode_Converts_DocumentType() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<!DOCTYPE foo [ <!ENTITY val \"bar\"> ]><foo/>"));
            using var reader = XmlReader.Create(stream, new XmlReaderSettings() { DtdProcessing = DtdProcessing.Parse });

            var documentTypeConverter = Substitute.For<INodeConverter>();
            var converter = new Converter(new ConverterOptions() {
                DocumentTypeConverter = documentTypeConverter
            });

            reader.Read(); // Move to document type

            converter.ConvertNode(reader, writer);

            documentTypeConverter.Received().Convert(reader, writer);
        }

        [Fact]
        public void ConvertNode_Converts_ProcessingInstruction() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<?xml-stylesheet type=\"text/xsl\" href=\"style.xsl\"?><foo/>"));
            using var reader = XmlReader.Create(stream);

            var processingInstructionConverter = Substitute.For<INodeConverter>();
            var converter = new Converter(new ConverterOptions() {
                ProcessingInstructionConverter = processingInstructionConverter
            });

            reader.Read(); // Move to processing instruction

            converter.ConvertNode(reader, writer);

            processingInstructionConverter.Received().Convert(reader, writer);
        }

        [Fact]
        public void ConvertNode_Throws_Exception_For_EndElement() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<foo bar=\"baz\"></foo>"));
            using var reader = XmlReader.Create(stream);

            var converter = new Converter();

            reader.Read(); // Move to element
            reader.Read(); // Move to end element

            var exception = Assert.Throws<UnexpectedNodeTypeException>(() => converter.ConvertNode(reader, writer));
            
            Assert.Equal(XmlNodeType.EndElement, exception.NodeType);
            Assert.Equal("Node type 'EndElement' was not handled by ConvertElement; ensure reader is in correct position before calling Convert", exception.Message);
        }

        [Fact]
        public void ConvertNode_Throws_Exception_For_Attribute() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<foo bar=\"baz\" />"));
            using var reader = XmlReader.Create(stream);

            var converter = new Converter();

            reader.Read(); // Move to element
            reader.MoveToAttribute(0);

            var exception = Assert.Throws<UnexpectedNodeTypeException>(() => converter.ConvertNode(reader, writer));

            Assert.Equal(XmlNodeType.Attribute, exception.NodeType);
            Assert.Equal("Node type 'Attribute' was not handled by ConvertElement; ensure reader is in correct position before calling Convert", exception.Message);
        }

        [Theory]
        [InlineData("<foo bar=\"baz\">Content</foo>", XmlNodeType.EndElement, 0)]
        [InlineData("<foo bar=\"baz\"></foo>", XmlNodeType.EndElement, 0)]
        [InlineData("<foo bar=\"baz\"/>", XmlNodeType.Element, 0)]
        public void ConvertElement_Leaves_Reader_At_End_Of_Element(string xml, XmlNodeType expectedNodeType, int expectedDepth) {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream);

            var defaultElementConverter = Substitute.For<IElementConverter>();
            var converter = new Converter(new ConverterOptions() {
                DefaultElementConverter = defaultElementConverter
            });

            reader.Read(); // Move to element

            converter.ConvertElement(reader, writer);

            Assert.Equal(expectedNodeType, reader.NodeType);
            Assert.Equal(expectedDepth, reader.Depth);
        }

        [Theory]
        [InlineData("<foo bar=\"baz\">Content</foo>", "foo", 1, false)]
        [InlineData("<foo bar=\"baz\"/>", "foo", 1, true)]
        [InlineData("<foo>Content</foo>", "foo", 0, false)]
        [InlineData("<foo/>", "foo", 0, true)]
        public void ConvertElement_ElementData_Is_Parsed_Correctly(string xml, string expectedName, int expectedAttributeCount, bool expectedIsSelfClosing) {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream);

            var defaultElementConverter = Substitute.For<IElementConverter>();
            var converter = new Converter(new ConverterOptions() {
                DefaultElementConverter = defaultElementConverter
            });

            reader.Read(); // Move to element

            converter.ConvertElement(reader, writer);

            defaultElementConverter.Received().ShouldRenderContent(Arg.Is<ElementData>(d => VerifyElementData(d, expectedName, expectedAttributeCount, expectedIsSelfClosing)));
            defaultElementConverter.Received().RenderStart(Arg.Is<ElementData>(d => VerifyElementData(d, expectedName, expectedAttributeCount, expectedIsSelfClosing)), writer);
            defaultElementConverter.Received().RenderEnd(Arg.Is<ElementData>(d => VerifyElementData(d, expectedName, expectedAttributeCount, expectedIsSelfClosing)), writer);
        }

        private bool VerifyElementData(ElementData elementData, string expectedName, int expectedAttributeCount, bool expectedIsSelfClosing) {
            Assert.Equal(expectedName, elementData.Name);
            Assert.Equal(expectedAttributeCount, elementData.Attributes.Count);
            Assert.Equal(expectedIsSelfClosing, elementData.IsSelfClosing);

            return true;
        }

        [Fact]
        public void ConvertElement_Uses_First_Valid_ElementConverter() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<foo bar=\"baz\">Content</foo>"));
            using var reader = XmlReader.Create(stream);

            var defaultElementConverter = Substitute.For<IElementConverter>();
            var validElementConverter = Substitute.For<IElementConverter>();
            var additionalElementConverter = Substitute.For<IElementConverter>();
            var converter = new Converter(new ConverterOptions() {
                ElementConverters = new List<IElementConverter>() { validElementConverter, additionalElementConverter },
                DefaultElementConverter = defaultElementConverter
            });

            validElementConverter.IsValidFor(Arg.Any<ElementData>()).Returns(true);
            additionalElementConverter.IsValidFor(Arg.Any<ElementData>()).Returns(true);

            reader.Read(); // Move to element

            converter.ConvertElement(reader, writer);
                        
            VerifyConverterIsUsed(validElementConverter, writer);
            VerifyConverterIsNotUsed(additionalElementConverter, writer);
            VerifyConverterIsNotUsed(defaultElementConverter, writer);
        }

        [Fact]
        public void ConvertElement_Uses_DefaultElementConverter_When_No_Valid_ElementConverters_Are_Found() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<foo bar=\"baz\">Content</foo>"));
            using var reader = XmlReader.Create(stream);

            var invalidElementConverter = Substitute.For<IElementConverter>();
            var defaultElementConverter = Substitute.For<IElementConverter>();
            var converter = new Converter(new ConverterOptions() {
                ElementConverters = new List<IElementConverter>() { invalidElementConverter },
                DefaultElementConverter = defaultElementConverter
            });

            invalidElementConverter.IsValidFor(Arg.Any<ElementData>()).Returns(false);

            reader.Read(); // Move to element

            converter.ConvertElement(reader, writer);

            VerifyConverterIsUsed(defaultElementConverter, writer);
            VerifyConverterIsNotUsed(invalidElementConverter, writer);
        }

        [Fact]
        public void ConvertElement_Uses_DefaultElementConverter_When_No_ElementConverters_Are_Specified() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<foo bar=\"baz\">Content</foo>"));
            using var reader = XmlReader.Create(stream);

            var defaultElementConverter = Substitute.For<IElementConverter>();
            var converter = new Converter(new ConverterOptions() {
                DefaultElementConverter = defaultElementConverter
            });

            reader.Read(); // Move to element

            converter.ConvertElement(reader, writer);

            VerifyConverterIsUsed(defaultElementConverter, writer);
        }

        private void VerifyConverterIsUsed(IElementConverter elementConverter, TextWriter writer) {
            elementConverter.Received().ShouldRenderContent(Arg.Any<ElementData>());
            elementConverter.Received().RenderStart(Arg.Any<ElementData>(), writer);
            elementConverter.Received().RenderEnd(Arg.Any<ElementData>(), writer);
        }

        private void VerifyConverterIsNotUsed(IElementConverter elementConverter, TextWriter writer) {
            elementConverter.DidNotReceiveWithAnyArgs().ShouldRenderContent(default!);
            elementConverter.DidNotReceiveWithAnyArgs().RenderStart(default!, default!);
            elementConverter.DidNotReceiveWithAnyArgs().RenderEnd(default!, default!);
        }
    }
}
