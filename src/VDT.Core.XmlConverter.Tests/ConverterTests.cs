using NSubstitute;
using System.IO;
using System.Text;
using System.Xml;
using VDT.Core.XmlConverter.Elements;
using Xunit;

namespace VDT.Core.XmlConverter.Tests {
    public class ConverterTests {
        private const string xml = "<foo bar=\"baz\">Content</foo>";

        [Fact]
        public void ConvertElement_Uses_DefaultElementConverter_When_No_ElementConverters_Are_Specified() {
            using var writer = new StringWriter();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream);

            var defaultElementConverter = Substitute.For<IElementConverter>();
            var converter = new Converter(new ConverterOptions() {
                DefaultElementConverter = defaultElementConverter
            });

            reader.Read();

            converter.ConvertElement(reader, writer);

            defaultElementConverter.Received().ShouldRenderContent(Arg.Is<ElementData>(d => VerifyElementData(d)));
            defaultElementConverter.Received().RenderStart(Arg.Is<ElementData>(d => VerifyElementData(d)), writer);
            defaultElementConverter.Received().RenderEnd(Arg.Is<ElementData>(d => VerifyElementData(d)), writer);

            Assert.Equal(XmlNodeType.EndElement, reader.NodeType);
            Assert.Equal(0, reader.Depth);
        }

        private bool VerifyElementData(ElementData elementData) {
            Assert.Equal("foo", elementData.Name);
            Assert.Single(elementData.Attributes);
            Assert.False(elementData.IsSelfClosing);

            return true;
        }
    }
}
