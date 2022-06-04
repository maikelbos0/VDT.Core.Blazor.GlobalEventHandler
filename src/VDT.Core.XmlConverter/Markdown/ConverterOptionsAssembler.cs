using System;

namespace VDT.Core.XmlConverter.Markdown {
    internal class ConverterOptionsAssembler : IConverterOptionsAssembler {
        public void SetTextConverter(ConverterOptions options) {
            // TODO add escape characters for ~, =, ^ for extended as needed
            options.TextConverter = new TextConverter();
        }

        public void SetNodeConverterForNonMarkdownNodeTypes(ConverterOptions options) {
            options.CDataConverter = new NodeRemovingConverter();
            options.CommentConverter = new NodeRemovingConverter();
            options.DocumentTypeConverter = new NodeRemovingConverter();
            options.ProcessingInstructionConverter = new NodeRemovingConverter();
            options.XmlDeclarationConverter = new NodeRemovingConverter();
            options.SignificantWhitespaceConverter = new NodeRemovingConverter();
            options.WhitespaceConverter = new NodeRemovingConverter();
        }

        public void AddHeadingConverters(ConverterOptions options) {
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

        public void AddListItemConverters(ConverterOptions options) {
            options.ElementConverters.Add(new OrderedListItemConverter());
            options.ElementConverters.Add(new UnorderedListItemConverter());
        }

        public void AddHorizontalRuleConverter(ConverterOptions options) {
            options.ElementConverters.Add(new BlockElementConverter("---", "hr"));
        }

        public void AddBlockquoteConverter(ConverterOptions options) {
            options.ElementConverters.Add(new BlockquoteConverter());
        }

        public void AddPreConverters(ConverterOptions options) {
            // Register pre content converter before any other element converters so it clears 
            options.ElementConverters.Insert(0, new PreContentConverter());

            // TODO allow switch between indented and fenced code block
            options.ElementConverters.Add(new PreConverter());
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

        public void AddStrikethroughConverter(ConverterOptions options) {
            options.ElementConverters.Add(new InlineElementConverter("~~", "~~", "del"));
        }

        public void AddHighlightConverter(ConverterOptions options) {
            options.ElementConverters.Add(new InlineElementConverter("==", "==", "mark"));
        }

        public void AddSubscriptConverter(ConverterOptions options) {
            options.ElementConverters.Add(new InlineElementConverter("~", "~", "sub"));
        }

        public void AddTagRemovingElementConverter(ConverterOptions options) {
            options.ElementConverters.Add(new TagRemovingElementConverter("html", "body", "ul", "ol", "menu", "div", "span"));
        }

        public void AddElementRemovingConverter(ConverterOptions options) {
            // TODO consider not removing some of these? Meta, frame, iframe, frameset?
            options.ElementConverters.Add(new ElementRemovingConverter("script", "style", "head", "frame", "meta", "iframe", "frameset"));
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
