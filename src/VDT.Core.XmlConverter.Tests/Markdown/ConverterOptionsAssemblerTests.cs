using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class ConverterOptionsAssemblerTests {
        [Fact]
        public void SetNodeRemovingConverterForNonMarkdownNodeTypes_Sets_Non_Markdown_Node_Converters() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.SetNodeRemovingConverterForNonMarkdownNodeTypes(options);

            Assert.IsType<NodeRemovingConverter>(options.CDataConverter);
            Assert.IsType<NodeRemovingConverter>(options.CommentConverter);
            Assert.IsType<NodeRemovingConverter>(options.XmlDeclarationConverter);
            Assert.IsType<NodeRemovingConverter>(options.WhitespaceConverter);
            Assert.IsType<NodeRemovingConverter>(options.SignificantWhitespaceConverter);
            Assert.IsType<NodeRemovingConverter>(options.DocumentTypeConverter);
            Assert.IsType<NodeRemovingConverter>(options.ProcessingInstructionConverter);
        }
    }
}
