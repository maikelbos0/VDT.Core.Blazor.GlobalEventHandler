using System.Linq;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class ConverterOptionsAssemblerTests {
        [Fact]
        public void SetNodeConverterForNonMarkdownNodeTypes_Sets_CDataConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.SetNodeConverterForNonMarkdownNodeTypes(options);

            Assert.IsType<NodeRemovingConverter>(options.CDataConverter);
        }

        [Fact]
        public void SetNodeConverterForNonMarkdownNodeTypes_Sets_CommentConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.SetNodeConverterForNonMarkdownNodeTypes(options);

            Assert.IsType<NodeRemovingConverter>(options.CommentConverter);
        }

        [Fact]
        public void SetNodeConverterForNonMarkdownNodeTypes_Sets_XmlDeclarationConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.SetNodeConverterForNonMarkdownNodeTypes(options);

            Assert.IsType<NodeRemovingConverter>(options.XmlDeclarationConverter);
        }

        [Fact]
        public void SetNodeConverterForNonMarkdownNodeTypes_Sets_WhitespaceConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.SetNodeConverterForNonMarkdownNodeTypes(options);

            Assert.IsType<NodeRemovingConverter>(options.WhitespaceConverter);
        }

        [Fact]
        public void SetNodeConverterForNonMarkdownNodeTypes_Sets_SignificantWhitespaceConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.SetNodeConverterForNonMarkdownNodeTypes(options);

            Assert.IsType<NodeRemovingConverter>(options.SignificantWhitespaceConverter);
        }

        [Fact]
        public void SetNodeConverterForNonMarkdownNodeTypes_Sets_DocumentTypeConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.SetNodeConverterForNonMarkdownNodeTypes(options);

            Assert.IsType<NodeRemovingConverter>(options.DocumentTypeConverter);
        }

        [Fact]
        public void SetNodeConverterForNonMarkdownNodeTypes_Sets_ProcessingInstructionConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.SetNodeConverterForNonMarkdownNodeTypes(options);

            Assert.IsType<NodeRemovingConverter>(options.ProcessingInstructionConverter);
        }

        [Theory]
        [InlineData("# ", "h1")]
        [InlineData("## ", "h2")]
        [InlineData("### ", "h3")]
        [InlineData("#### ", "h4")]
        [InlineData("##### ", "h5")]
        [InlineData("###### ", "h6")]
        public void AddHeaderConverters_Adds_ElementConverter(string expectedStartOutput, string expectedValidForElementName) {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddHeaderConverters(options);

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
        public void AddLinebreakConverter_Adds_LinebreakConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddLinebreakConverter(options);

            Assert.Single(options.ElementConverters, converter => converter is LinebreakConverter);
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
        
        [Fact]
        public void AddBlockquoteConverter_Adds_BlockquoteConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddBlockquoteConverter(options);

            Assert.Single(options.ElementConverters, converter => converter is BlockquoteConverter);
        }

        [Fact]
        public void AddHyperlinkConverter_Adds_HyperlinkConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddHyperlinkConverter(options);

            Assert.Single(options.ElementConverters, converter => converter is HyperlinkConverter);
        }

        [Fact]
        public void AddImageConverter_Adds_ImageConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddImageConverter(options);

            Assert.Single(options.ElementConverters, converter => converter is ImageConverter);
        }

        [Theory]
        [InlineData("**", "**", "strong", "b")]
        [InlineData("*", "*", "em", "i")]
        public void AddEmphasisConverters_Adds_ElementConverter(string expectedStartOutput, string expectedEndOutput, params string[] expectedValidForElementNames) {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddEmphasisConverters(options);

            Assert.Single(options.ElementConverters, converter => IsInlineElementConverter(converter, expectedStartOutput, expectedEndOutput, expectedValidForElementNames));
        }

        [Fact]
        public void AddInlineCodeConverter_Adds_ElementConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddInlineCodeConverter(options);

            Assert.Single(options.ElementConverters, converter => IsInlineElementConverter(converter, "`", "`", "code", "kbd", "samp", "var"));
        }

        private static bool IsBlockElementConverter(IElementConverter converter, string expectedStartOutput, params string[] expectedValidForElementNames) 
            => converter is BlockElementConverter blockElementConverter
                && blockElementConverter.StartOutput == expectedStartOutput
                && blockElementConverter.ValidForElementNames.SequenceEqual(expectedValidForElementNames);

        private static bool IsInlineElementConverter(IElementConverter converter, string expectedStartOutput, string expectedEndOutput, params string[] expectedValidForElementNames) 
            => converter is InlineElementConverter inlineElementConverter
                && inlineElementConverter.StartOutput == expectedStartOutput
                && inlineElementConverter.EndOutput == expectedEndOutput
                && inlineElementConverter.ValidForElementNames.SequenceEqual(expectedValidForElementNames);

        [Theory]
        [InlineData(UnknownElementHandlingMode.RemoveTags, true)]
        [InlineData(UnknownElementHandlingMode.RemoveElements, false)]
        public void SetDefaultElementConverter_Sets_DefaultElementConverter(UnknownElementHandlingMode unknownElementHandlingMode, bool expectedRenderContent) {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.SetDefaultElementConverter(options, unknownElementHandlingMode);

            Assert.Equal(expectedRenderContent, Assert.IsType<UnknownElementConverter>(options.DefaultElementConverter).RenderContent);
        }

        [Fact]
        public void SetDefaultElementConverter_Sets_DefaultElementConverter_NoOpElementConverter_For_UnknownElementHandlingMode_None() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.SetDefaultElementConverter(options, UnknownElementHandlingMode.None);

            Assert.IsType<NoOpElementConverter>(options.DefaultElementConverter);
        }
    }
}
