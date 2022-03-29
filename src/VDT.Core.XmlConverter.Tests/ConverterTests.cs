using NSubstitute;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using VDT.Core.XmlConverter.Elements;
using Xunit;

namespace VDT.Core.XmlConverter.Tests {
    public class ConverterTests {
        private const string elementXml = "<foo bar=\"baz\">Content</foo>";

        [Fact]
        public void ConvertElement_Leaves_Reader_At_End_Of_Element() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(elementXml));
            using var reader = XmlReader.Create(stream);

            var defaultElementConverter = Substitute.For<IElementConverter>();
            var converter = new Converter(new ConverterOptions() {
                DefaultElementConverter = defaultElementConverter
            });

            reader.Read();

            converter.ConvertElement(reader, writer);

            Assert.Equal(XmlNodeType.EndElement, reader.NodeType);
            Assert.Equal(0, reader.Depth);
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

            reader.Read();

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
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(elementXml));
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

            reader.Read();

            converter.ConvertElement(reader, writer);
                        
            VerifyConverterIsUsed(validElementConverter, writer);
            VerifyConverterIsNotUsed(additionalElementConverter, writer);
            VerifyConverterIsNotUsed(defaultElementConverter, writer);
        }

        [Fact]
        public void ConvertElement_Uses_DefaultElementConverter_When_No_Valid_ElementConverters_Are_Found() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(elementXml));
            using var reader = XmlReader.Create(stream);

            var invalidElementConverter = Substitute.For<IElementConverter>();
            var defaultElementConverter = Substitute.For<IElementConverter>();
            var converter = new Converter(new ConverterOptions() {
                ElementConverters = new List<IElementConverter>() { invalidElementConverter },
                DefaultElementConverter = defaultElementConverter
            });

            invalidElementConverter.IsValidFor(Arg.Any<ElementData>()).Returns(false);

            reader.Read();

            converter.ConvertElement(reader, writer);

            VerifyConverterIsUsed(defaultElementConverter, writer);
            VerifyConverterIsNotUsed(invalidElementConverter, writer);
        }

        [Fact]
        public void ConvertElement_Uses_DefaultElementConverter_When_No_ElementConverters_Are_Specified() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(elementXml));
            using var reader = XmlReader.Create(stream);

            var defaultElementConverter = Substitute.For<IElementConverter>();
            var converter = new Converter(new ConverterOptions() {
                DefaultElementConverter = defaultElementConverter
            });

            reader.Read();

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
