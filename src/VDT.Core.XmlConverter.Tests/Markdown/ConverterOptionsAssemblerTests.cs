using System.Collections.Generic;
using System.Linq;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class ConverterOptionsAssemblerTests {
        [Fact]
        public void SetTextConverter_Sets_TextConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.SetTextConverter(options, CharacterEscapeMode.ElementConverterBased, new HashSet<ElementConverterTarget>(), new Dictionary<char, string>());

            Assert.IsType<TextConverter>(options.TextConverter);
        }

        [Fact]
        public void SetTextConverter_Sets_TextConverter_CharacterEscapes_For_CharacterEscapeMode_Full() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();
            var customCharacterEscapes = new Dictionary<char, string>() {
                { '>', ">" },
                { '?', "\\?" }
            };

            assembler.SetTextConverter(options, CharacterEscapeMode.Full, new HashSet<ElementConverterTarget>(), customCharacterEscapes);

            var textConverter = Assert.IsType<TextConverter>(options.TextConverter);

            Assert.Equal(new Dictionary<char, string>() {
                { '\\', "\\\\" },
                { '`', "\\`" },
                { '_', "\\_" },
                { '*', "\\*" },
                { '-', "\\-" },
                { '.', "\\." },
                { '!', "\\!" },
                { '+', "\\+" },
                { '#', "\\#" },
                { '(', "\\(" },
                { ')', "\\)" },
                { '[', "\\[" },
                { ']', "\\]" },
                { '{', "\\{" },
                { '}', "\\}" },
                { '~', "\\~" },
                { '=', "\\=" },
                { '^', "\\^" },
                { '<', "&lt;" },
                { '>', ">" },
                { '&', "&amp;" },
                { '?', "\\?" }
            }, textConverter.CharacterEscapes);
        }

        [Theory]
        [InlineData(ElementConverterTarget.Heading, '\\', '#')]
        [InlineData(ElementConverterTarget.Paragraph)]
        [InlineData(ElementConverterTarget.Linebreak)]
        [InlineData(ElementConverterTarget.ListItem, '\\', '*', '+', '-', '.')]
        [InlineData(ElementConverterTarget.HorizontalRule, '\\', '-')]
        [InlineData(ElementConverterTarget.Blockquote)]
        [InlineData(ElementConverterTarget.Pre, '\\', '`')]
        [InlineData(ElementConverterTarget.Hyperlink, '\\', '(', ')', '[', ']')]
        [InlineData(ElementConverterTarget.Image, '\\', '!', '(', ')', '[', ']')]
        [InlineData(ElementConverterTarget.Important, '\\', '*', '_')]
        [InlineData(ElementConverterTarget.Emphasis, '\\', '*', '_')]
        [InlineData(ElementConverterTarget.InlineCode, '\\', '`')]
        [InlineData(ElementConverterTarget.Strikethrough, '\\', '~')]
        [InlineData(ElementConverterTarget.Highlight, '\\', '=')]
        [InlineData(ElementConverterTarget.Subscript, '\\', '~')]
        [InlineData(ElementConverterTarget.Superscript, '\\', '^')]
        public void SetTextConverter_Sets_TextConverter_CharacterEscapes_For_CharacterEscapeMode_ElementConverterBased(ElementConverterTarget elementConverterTarget, params char[] expectedCharacters) {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();
            var customCharacterEscapes = new Dictionary<char, string>() {
                { '>', ">" },
                { '?', "\\?" }
            };
            var expectedEscapedCharacters = new HashSet<char>(expectedCharacters.Concat(new[] { '<', '>', '&', '?' }));

            assembler.SetTextConverter(options, CharacterEscapeMode.ElementConverterBased, new HashSet<ElementConverterTarget>() { elementConverterTarget }, customCharacterEscapes);

            var textConverter = Assert.IsType<TextConverter>(options.TextConverter);

            Assert.Equal(expectedEscapedCharacters, textConverter.CharacterEscapes.Keys.ToHashSet());
            Assert.Equal(">", textConverter.CharacterEscapes['>']);
        }

        [Fact]
        public void SetTextConverter_Succeeds_For_CharacterEscapeMode_ElementConverterBased_With_Double_Character() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();
            var targets = new HashSet<ElementConverterTarget>() { 
                ElementConverterTarget.ListItem,
                ElementConverterTarget.HorizontalRule
            };

            assembler.SetTextConverter(options, CharacterEscapeMode.ElementConverterBased, targets, new Dictionary<char, string>());

            var textConverter = Assert.IsType<TextConverter>(options.TextConverter);

            Assert.Equal(new HashSet<char>() { '<', '>', '&', '\\', '*', '+', '-', '.' }, textConverter.CharacterEscapes.Keys.ToHashSet());
        }

        [Fact]
        public void SetTextConverter_Sets_TextConverter_CharacterEscapes_For_CharacterEscapeMode_Custom() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();
            var customCharacterEscapes = new Dictionary<char, string>() {
                { '>', ">" },
                { '?', "\\?" }
            };

            assembler.SetTextConverter(options, CharacterEscapeMode.Custom, new HashSet<ElementConverterTarget>(), customCharacterEscapes);

            var textConverter = Assert.IsType<TextConverter>(options.TextConverter);

            Assert.Equal(new Dictionary<char, string>() {
                { '<', "&lt;" },
                { '>', ">" },
                { '&', "&amp;" },
                { '?', "\\?" }
            }, textConverter.CharacterEscapes);
        }

        [Fact]
        public void SetTextConverter_Sets_TextConverter_CharacterEscapes_For_CharacterEscapeMode_CustomOnly() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();
            var customCharacterEscapes = new Dictionary<char, string>() {
                { '?', "\\?" }
            };

            assembler.SetTextConverter(options, CharacterEscapeMode.CustomOnly, new HashSet<ElementConverterTarget>(), customCharacterEscapes);

            var textConverter = Assert.IsType<TextConverter>(options.TextConverter);

            Assert.Equal(new Dictionary<char, string>() {
                { '?', "\\?" }
            }, textConverter.CharacterEscapes);
        }

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
        public void AddHeadingConverters_Adds_ElementConverter(string expectedStartOutput, string expectedValidForElementName) {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddHeadingConverters(options);

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
        public void AddListItemConverters_Adds_OrderedListItemConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddListItemConverters(options);

            Assert.Single(options.ElementConverters, converter => converter is OrderedListItemConverter);
        }

        [Fact]
        public void AddListItemConverters_Adds_UnorderedListItemConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddListItemConverters(options);

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
        public void AddPreConverters_Inserts_PreContentConverter_As_First_Converter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddPreConverters(options, PreConversionMode.Fenced);

            Assert.IsType<PreContentConverter>(options.ElementConverters.FirstOrDefault());
        }

        [Fact]
        public void AddPreConverters_Adds_FencedPreConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddPreConverters(options, PreConversionMode.Fenced);

            Assert.Single(options.ElementConverters, converter => converter is FencedPreConverter);
        }

        [Fact]
        public void AddPreConverters_Adds_IndentedPreConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddPreConverters(options, PreConversionMode.Indented);

            Assert.Single(options.ElementConverters, converter => converter is IndentedPreConverter);
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

        [Fact]
        public void AddImportantConverter_Adds_ElementConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddImportantConverter(options);

            Assert.Single(options.ElementConverters, converter => IsInlineElementConverter(converter, "**", "**", "strong", "b"));
        }

        [Fact]
        public void AddEmphasisConverter_Adds_ElementConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddEmphasisConverter(options);

            Assert.Single(options.ElementConverters, converter => IsInlineElementConverter(converter, "*", "*", "em", "i"));
        }

        [Fact]
        public void AddInlineCodeConverter_Adds_ElementConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddInlineCodeConverter(options);

            Assert.Single(options.ElementConverters, converter => IsInlineElementConverter(converter, "`", "`", "code", "kbd", "samp", "var"));
        }

        [Fact]
        public void AddStrikethroughConverter_Adds_ElementConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddStrikethroughConverter(options);

            Assert.Single(options.ElementConverters, converter => IsInlineElementConverter(converter, "~~", "~~", "del"));
        }

        [Fact]
        public void AddHighlightConverter_Adds_ElementConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddHighlightConverter(options);

            Assert.Single(options.ElementConverters, converter => IsInlineElementConverter(converter, "==", "==", "mark"));
        }

        [Fact]
        public void AddSubscriptConverter_Adds_ElementConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddSubscriptConverter(options);

            Assert.Single(options.ElementConverters, converter => IsInlineElementConverter(converter, "~", "~", "sub"));
        }

        [Fact]
        public void AddSuperscriptConverter_Adds_ElementConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddSuperscriptConverter(options);

            Assert.Single(options.ElementConverters, converter => IsInlineElementConverter(converter, "^", "^", "sup"));
        }

        [Fact]
        public void AddTagRemovingElementConverter_Adds_TagRemovingElementConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddTagRemovingElementConverter(options, new HashSet<string>() { "html", "body", "ul", "ol", "menu", "div", "span" });

            Assert.Equal(new[] { "html", "body", "ul", "ol", "menu", "div", "span" }, Assert.IsType<TagRemovingElementConverter>(Assert.Single(options.ElementConverters)).ValidForElementNames);
        }

        [Fact]
        public void AddTagRemovingElementConverter_Adds_Nothing_For_Empty_Hashset() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddTagRemovingElementConverter(options, new HashSet<string>());

            Assert.Empty(options.ElementConverters);
        }

        [Fact]
        public void AddElementRemovingConverter_Adds_ElementRemovingConverter() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddElementRemovingConverter(options, new HashSet<string>() { "script", "style", "head", "frame", "meta", "iframe", "frameset" });

            Assert.Equal(new[] { "script", "style", "head", "frame", "meta", "iframe", "frameset" }, Assert.IsType<ElementRemovingConverter>(Assert.Single(options.ElementConverters)).ValidForElementNames);
        }

        [Fact]
        public void AddElementRemovingElementConverter_Adds_Nothing_For_Empty_Hashset() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.AddElementRemovingConverter(options, new HashSet<string>());

            Assert.Empty(options.ElementConverters);
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

        [Fact]
        public void SetDefaultElementConverter_Sets_DefaultElementConverter_NoOpElementConverter_For_UnknownElementHandlingMode_None() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.SetDefaultElementConverter(options, UnknownElementHandlingMode.None);

            Assert.IsType<NoOpElementConverter>(options.DefaultElementConverter);
        }

        [Fact]
        public void SetDefaultElementConverter_Sets_DefaultElementConverter_TagRemovingElementConverter_For_UnknownElementHandlingMode_RemoveTags() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.SetDefaultElementConverter(options, UnknownElementHandlingMode.RemoveTags);

            Assert.IsType<TagRemovingElementConverter>(options.DefaultElementConverter);
        }

        [Fact]
        public void SetDefaultElementConverter_Sets_DefaultElementConverter_ElementRemovingElementConverter_For_UnknownElementHandlingMode_RemoveElements() {
            var options = new ConverterOptions();
            var assembler = new ConverterOptionsAssembler();

            assembler.SetDefaultElementConverter(options, UnknownElementHandlingMode.RemoveElements);

            Assert.IsType<ElementRemovingConverter>(options.DefaultElementConverter);
        }
    }
}
