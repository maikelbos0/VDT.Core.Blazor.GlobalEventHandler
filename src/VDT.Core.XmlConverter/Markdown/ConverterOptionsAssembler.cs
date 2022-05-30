using System;

namespace VDT.Core.XmlConverter.Markdown {
    internal class ConverterOptionsAssembler : IConverterOptionsAssembler {
        public void SetNodeConverterForNonMarkdownNodeTypes(ConverterOptions options) {
            options.CDataConverter = new NodeRemovingConverter();
            options.CommentConverter = new NodeRemovingConverter();
            options.DocumentTypeConverter = new NodeRemovingConverter();
            options.ProcessingInstructionConverter = new NodeRemovingConverter();
            options.XmlDeclarationConverter = new NodeRemovingConverter();
            options.SignificantWhitespaceConverter = new NodeRemovingConverter();
            options.WhitespaceConverter = new NodeRemovingConverter();
        }

        public void AddHeaderConverters(ConverterOptions options) {
            options.ElementConverters.Add(new BlockElementConverter("# ", "h1"));
            options.ElementConverters.Add(new BlockElementConverter("## ", "h2"));
            options.ElementConverters.Add(new BlockElementConverter("### ", "h3"));
            options.ElementConverters.Add(new BlockElementConverter("#### ", "h4"));
            options.ElementConverters.Add(new BlockElementConverter("##### ", "h5"));
            options.ElementConverters.Add(new BlockElementConverter("###### ", "h6"));
        }

        public void AddParagraphConverter(ConverterOptions options) {
            options.ElementConverters.Add(new ParagraphConverter());
        }

        public void AddLinebreakConverter(ConverterOptions options) {
            options.ElementConverters.Add(new LinebreakConverter());
        }

        public void AddListItemElementConverters(ConverterOptions options) {
            options.ElementConverters.Add(new OrderedListItemConverter());
            options.ElementConverters.Add(new UnorderedListItemConverter());
        }

        public void AddHorizontalRuleConverter(ConverterOptions options) {
            options.ElementConverters.Add(new BlockElementConverter("---", "hr"));
        }

        public void AddBlockquoteConverter(ConverterOptions options) {
            options.ElementConverters.Add(new BlockquoteConverter());
        }

        public void AddHyperlinkConverter(ConverterOptions options) {
            options.ElementConverters.Add(new HyperlinkConverter());
        }

        public void AddImageConverter(ConverterOptions options) {
            options.ElementConverters.Add(new ImageConverter());
        }

        public void AddEmphasisConverters(ConverterOptions options) {
            options.ElementConverters.Add(new InlineElementConverter("**", "**", "strong", "b"));
            options.ElementConverters.Add(new InlineElementConverter("*", "*", "em", "i"));
        }

        public void AddInlineCodeConverter(ConverterOptions options) {
            options.ElementConverters.Add(new InlineElementConverter("`", "`", "code", "kbd", "samp", "var"));
        }

        public void SetDefaultElementConverter(ConverterOptions options, UnknownElementHandlingMode unknownElementHandlingMode) {
            options.DefaultElementConverter = unknownElementHandlingMode switch {
                UnknownElementHandlingMode.None => new NoOpElementConverter(),
                UnknownElementHandlingMode.RemoveTags => new UnknownElementConverter(true),
                UnknownElementHandlingMode.RemoveElements => new UnknownElementConverter(false),
                _ => throw new NotImplementedException($"No implementation found for {nameof(UnknownElementHandlingMode)} '{unknownElementHandlingMode}'")
            };
        }
    }
}
