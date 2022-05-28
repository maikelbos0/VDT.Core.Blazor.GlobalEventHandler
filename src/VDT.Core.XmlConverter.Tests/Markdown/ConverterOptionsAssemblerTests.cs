using System.Linq;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class ConverterOptionsAssemblerTests {
        [Fact]
        public void SetNodeRemovingConverterForNonMarkdownNodeTypes_Sets_CDataConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.SetNodeRemovingConverterForNonMarkdownNodeTypes(options);

            Assert.IsType<NodeRemovingConverter>(options.CDataConverter);
        }

        [Fact]
        public void SetNodeRemovingConverterForNonMarkdownNodeTypes_Sets_CommentConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.SetNodeRemovingConverterForNonMarkdownNodeTypes(options);

            Assert.IsType<NodeRemovingConverter>(options.CommentConverter);
        }

        [Fact]
        public void SetNodeRemovingConverterForNonMarkdownNodeTypes_Sets_XmlDeclarationConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.SetNodeRemovingConverterForNonMarkdownNodeTypes(options);

            Assert.IsType<NodeRemovingConverter>(options.XmlDeclarationConverter);
        }

        [Fact]
        public void SetNodeRemovingConverterForNonMarkdownNodeTypes_Sets_WhitespaceConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.SetNodeRemovingConverterForNonMarkdownNodeTypes(options);

            Assert.IsType<NodeRemovingConverter>(options.WhitespaceConverter);
        }

        [Fact]
        public void SetNodeRemovingConverterForNonMarkdownNodeTypes_Sets_SignificantWhitespaceConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.SetNodeRemovingConverterForNonMarkdownNodeTypes(options);

            Assert.IsType<NodeRemovingConverter>(options.SignificantWhitespaceConverter);
        }

        [Fact]
        public void SetNodeRemovingConverterForNonMarkdownNodeTypes_Sets_DocumentTypeConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.SetNodeRemovingConverterForNonMarkdownNodeTypes(options);

            Assert.IsType<NodeRemovingConverter>(options.DocumentTypeConverter);
        }

        [Fact]
        public void SetNodeRemovingConverterForNonMarkdownNodeTypes_Sets_ProcessingInstructionConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.SetNodeRemovingConverterForNonMarkdownNodeTypes(options);

            Assert.IsType<NodeRemovingConverter>(options.ProcessingInstructionConverter);
        }

        [Theory]
        [InlineData("# ", "h1")]
        [InlineData("## ", "h2")]
        [InlineData("### ", "h3")]
        [InlineData("#### ", "h4")]
        [InlineData("##### ", "h5")]
        [InlineData("###### ", "h6")]
        public void AddHeaderElementConverters_Adds_BlockElementConverter(string expectedStartOutput, string expectedValidForElementName) {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddHeaderElementConverters(options);

            Assert.Single(options.ElementConverters, converter => IsBlockElementConverter(converter, expectedStartOutput, expectedValidForElementName));
        }

        [Fact]
        public void AddParagraphConverter_Adds_ParagraphConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddParagraphConverter(options);

            Assert.Single(options.ElementConverters, converter => converter is ParagraphConverter);
        }

        [Fact]
        public void AddListItemElementConverters_Adds_OrderedListItemConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddListItemElementConverters(options);

            Assert.Single(options.ElementConverters, converter => converter is OrderedListItemConverter);
        }

        [Fact]
        public void AddListItemElementConverters_Adds_UnorderedListItemConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddListItemElementConverters(options);

            Assert.Single(options.ElementConverters, converter => converter is UnorderedListItemConverter);
        }
        
        [Fact]
        public void AddHorizontalRuleConverter_Adds_BlockElementConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddHorizontalRuleConverter(options);

            Assert.Single(options.ElementConverters, converter => IsBlockElementConverter(converter, "---", "hr"));
        }

        private bool IsBlockElementConverter(IElementConverter converter, string expectedStartOutput, params string[] expectedValidForElementNames) 
            => converter is BlockElementConverter blockElementConverter
                && blockElementConverter.StartOutput == expectedStartOutput
                && blockElementConverter.ValidForElementNames.SequenceEqual(expectedValidForElementNames);
    }
}
