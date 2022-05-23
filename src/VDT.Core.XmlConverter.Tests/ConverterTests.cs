using NSubstitute;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Xunit;

namespace VDT.Core.XmlConverter.Tests {
    public class ConverterTests {
        [Fact]
        public void Convert_String() {
            const string xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
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

            var converter = new Converter();

            Assert.Equal(xml, converter.Convert(xml), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Convert_Converts_Fragment() {
            const string xml = @"Some content<node/>Content";

            using var writer = new StringWriter();

            var converter = new Converter();

            Assert.Equal(xml, converter.Convert(xml), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Convert_Converts_Empty_Fragment() {
            const string xml = @"";

            using var writer = new StringWriter();

            var converter = new Converter();

            Assert.Equal(xml, converter.Convert(xml), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Convert_Stream() {
            const string xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
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

            var converter = new Converter();

            Assert.Equal(xml, converter.Convert(stream), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Convert_XmlReader() {
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

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream, new XmlReaderSettings() { DtdProcessing = DtdProcessing.Parse });

            var converter = new Converter();

            Assert.Equal(xml, converter.Convert(reader), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Convert_Throws_Exception_For_Partially_Read_XmlReader() {
            const string xml = "<foo bar=\"baz\">Content</foo>";

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream, new XmlReaderSettings() { DtdProcessing = DtdProcessing.Parse });

            var converter = new Converter();

            reader.Read(); // Move to element

            var exception = Assert.Throws<UnexpectedNodeTypeException>(() => converter.Convert(reader));

            Assert.Equal(XmlNodeType.Element, exception.NodeType);
            Assert.Equal("Node type 'Element' was not expected; ensure reader is in starting position before calling Convert", exception.Message);
        }

        [Fact]
        public void ConvertNode_Converts_Node() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<foo bar=\"baz\">Content</foo>"));
            using var reader = XmlReader.Create(stream);

            var data = ConversionDataHelper.Create(ElementDataHelper.Create("foo"));
            var textConverter = Substitute.For<INodeConverter>();
            var converter = new Converter(new ConverterOptions() {
                TextConverter = textConverter
            });

            reader.Read(); // Move to element
            reader.Read(); // Move to text

            converter.ConvertNode(reader, writer, data);

            textConverter.Received().Convert(writer, Assert.IsType<NodeData>(data.CurrentNodeData));
        }

        [Fact]
        public void ConvertNode_Converts_Element() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<foo bar=\"baz\"></foo>"));
            using var reader = XmlReader.Create(stream);

            var data = new ConversionData();
            var elementConverter = Substitute.For<IElementConverter>();
            var converter = new Converter(new ConverterOptions() {
                DefaultElementConverter = elementConverter
            });

            reader.Read(); // Move to element

            converter.ConvertNode(reader, writer, data);

            VerifyConverterIsUsed(elementConverter, writer);
        }

        [Fact]
        public void ConvertNode_Converts_Text() {
            using var writer = Substitute.For<TextWriter>();
            using var reader = Substitute.For<XmlReader>();

            var data = NodeDataHelper.Create(XmlNodeType.Text);
            var textConverter = Substitute.For<INodeConverter>();
            var converter = new Converter(new ConverterOptions() {
                TextConverter = textConverter
            });

            converter.ConvertNode(reader, writer, data);

            textConverter.Received().Convert(writer, data);
        }

        [Fact]
        public void ConvertNode_Converts_CData() {
            using var writer = Substitute.For<TextWriter>();
            using var reader = Substitute.For<XmlReader>();

            var data = NodeDataHelper.Create(XmlNodeType.CDATA);
            var cDataConverter = Substitute.For<INodeConverter>();
            var converter = new Converter(new ConverterOptions() {
                CDataConverter = cDataConverter
            });

            converter.ConvertNode(reader, writer, data);

            cDataConverter.Received().Convert(writer, data);
        }

        [Fact]
        public void ConvertNode_Converts_Comment() {
            using var writer = Substitute.For<TextWriter>();
            using var reader = Substitute.For<XmlReader>();

            var data = NodeDataHelper.Create(XmlNodeType.Comment);
            var commentConverter = Substitute.For<INodeConverter>();
            var converter = new Converter(new ConverterOptions() {
                CommentConverter = commentConverter
            });

            converter.ConvertNode(reader, writer, data);

            commentConverter.Received().Convert(writer, data);
        }

        [Fact]
        public void ConvertNode_Converts_XmlDeclaration() {
            using var writer = Substitute.For<TextWriter>();
            using var reader = Substitute.For<XmlReader>();

            var data = NodeDataHelper.Create(XmlNodeType.XmlDeclaration);
            var xmlDeclarationConverter = Substitute.For<INodeConverter>();
            var converter = new Converter(new ConverterOptions() {
                XmlDeclarationConverter = xmlDeclarationConverter
            });

            converter.ConvertNode(reader, writer, data);

            xmlDeclarationConverter.Received().Convert(writer, data);
        }

        [Fact]
        public void ConvertNode_Converts_Whitespace() {
            using var writer = Substitute.For<TextWriter>();
            using var reader = Substitute.For<XmlReader>();

            var data = NodeDataHelper.Create(XmlNodeType.Whitespace);
            var whitespaceConverter = Substitute.For<INodeConverter>();
            var converter = new Converter(new ConverterOptions() {
                WhitespaceConverter = whitespaceConverter
            });

            converter.ConvertNode(reader, writer, data);

            whitespaceConverter.Received().Convert(writer, data);
        }

        [Fact]
        public void ConvertNode_Converts_SignificantWhitespace() {
            using var writer = Substitute.For<TextWriter>();
            using var reader = Substitute.For<XmlReader>();

            var data = NodeDataHelper.Create(XmlNodeType.SignificantWhitespace);
            var significantWhitespaceConverter = Substitute.For<INodeConverter>();
            var converter = new Converter(new ConverterOptions() {
                SignificantWhitespaceConverter = significantWhitespaceConverter
            });

            converter.ConvertNode(reader, writer, data);

            significantWhitespaceConverter.Received().Convert(writer, data);
        }

        [Fact]
        public void ConvertNode_Converts_DocumentType() {
            using var writer = Substitute.For<TextWriter>();
            using var reader = Substitute.For<XmlReader>();

            var data = NodeDataHelper.Create(XmlNodeType.DocumentType);
            var documentTypeConverter = Substitute.For<INodeConverter>();
            var converter = new Converter(new ConverterOptions() {
                DocumentTypeConverter = documentTypeConverter
            });

            converter.ConvertNode(reader, writer, data);

            documentTypeConverter.Received().Convert(writer, data);
        }

        [Fact]
        public void ConvertNode_Converts_ProcessingInstruction() {
            using var writer = Substitute.For<TextWriter>();
            using var reader = Substitute.For<XmlReader>();

            var data = NodeDataHelper.Create(XmlNodeType.ProcessingInstruction);
            var processingInstructionConverter = Substitute.For<INodeConverter>();
            var converter = new Converter(new ConverterOptions() {
                ProcessingInstructionConverter = processingInstructionConverter
            });

            converter.ConvertNode(reader, writer, data);

            processingInstructionConverter.Received().Convert(writer, data);
        }

        [Theory]
        [InlineData(XmlNodeType.EndElement)]
        [InlineData(XmlNodeType.Attribute)]
        public void ConvertNode_Throws_Exception_For_Unexpected_NodeType(XmlNodeType nodeType) {
            using var writer = Substitute.For<TextWriter>();
            using var reader = Substitute.For<XmlReader>();

            var data = NodeDataHelper.Create(nodeType);
            var converter = new Converter();

            var exception = Assert.Throws<UnexpectedNodeTypeException>(() => converter.ConvertNode(reader, writer, data));

            Assert.Equal(nodeType, exception.NodeType);
            Assert.Equal($"Node type '{nodeType}' was not expected; ensure reader is in starting position before calling Convert", exception.Message);
        }

        [Theory]
        [InlineData("<foo bar=\"baz\">Content</foo>", XmlNodeType.EndElement)]
        [InlineData("<foo bar=\"baz\"></foo>", XmlNodeType.EndElement)]
        [InlineData("<foo bar=\"baz\"/>", XmlNodeType.Element)]
        public void ConvertElement_Leaves_Reader_At_End_Of_Element(string xml, XmlNodeType expectedNodeType) {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream);

            var data = new ConversionData();
            var defaultElementConverter = Substitute.For<IElementConverter>();
            var converter = new Converter(new ConverterOptions() {
                DefaultElementConverter = defaultElementConverter
            });

            reader.Read(); // Move to element
            data.ReadNode(reader); // Read element

            converter.ConvertElement(reader, writer, data, Assert.IsType<ElementData>(data.CurrentNodeData));

            Assert.Equal(expectedNodeType, reader.NodeType);
        }

        [Fact]
        public void ConvertElement_Uses_First_Valid_ElementConverter() {
            using var writer = Substitute.For<TextWriter>();
            using var reader = Substitute.For<XmlReader>();

            var elementData = ElementDataHelper.Create("foo");
            var data = ConversionDataHelper.Create(elementData);
            var defaultElementConverter = Substitute.For<IElementConverter>();
            var validElementConverter = Substitute.For<IElementConverter>();
            var additionalElementConverter = Substitute.For<IElementConverter>();
            var converter = new Converter(new ConverterOptions() {
                ElementConverters = new List<IElementConverter>() { validElementConverter, additionalElementConverter },
                DefaultElementConverter = defaultElementConverter
            });

            validElementConverter.IsValidFor(Arg.Any<ElementData>()).Returns(true);
            additionalElementConverter.IsValidFor(Arg.Any<ElementData>()).Returns(true);

            converter.ConvertElement(reader, writer, data, elementData);

            VerifyConverterIsUsed(validElementConverter, writer);
            VerifyConverterIsNotUsed(additionalElementConverter, writer);
            VerifyConverterIsNotUsed(defaultElementConverter, writer);
        }

        [Fact]
        public void ConvertElement_Uses_DefaultElementConverter_When_No_Valid_ElementConverters_Are_Found() {
            using var writer = Substitute.For<TextWriter>();
            using var reader = Substitute.For<XmlReader>();

            var elementData = ElementDataHelper.Create("foo");
            var data = ConversionDataHelper.Create(elementData);
            var invalidElementConverter = Substitute.For<IElementConverter>();
            var defaultElementConverter = Substitute.For<IElementConverter>();
            var converter = new Converter(new ConverterOptions() {
                ElementConverters = new List<IElementConverter>() { invalidElementConverter },
                DefaultElementConverter = defaultElementConverter
            });

            invalidElementConverter.IsValidFor(Arg.Any<ElementData>()).Returns(false);

            converter.ConvertElement(reader, writer, data, Assert.IsType<ElementData>(data.CurrentNodeData));

            VerifyConverterIsUsed(defaultElementConverter, writer);
            VerifyConverterIsNotUsed(invalidElementConverter, writer);
        }

        [Fact]
        public void ConvertElement_Uses_DefaultElementConverter_When_No_ElementConverters_Are_Specified() {
            using var writer = Substitute.For<TextWriter>();
            using var reader = Substitute.For<XmlReader>();

            var elementData = ElementDataHelper.Create("foo");
            var data = ConversionDataHelper.Create(elementData);
            var defaultElementConverter = Substitute.For<IElementConverter>();
            var converter = new Converter(new ConverterOptions() {
                DefaultElementConverter = defaultElementConverter
            });

            converter.ConvertElement(reader, writer, data, Assert.IsType<ElementData>(data.CurrentNodeData));

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
