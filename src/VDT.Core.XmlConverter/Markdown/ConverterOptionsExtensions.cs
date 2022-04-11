using System;

namespace VDT.Core.XmlConverter.Markdown {
    public static class ConverterOptionsExtensions {
        public static ConverterOptions UseMarkdown(this ConverterOptions options) {
            var removingNodeConverter = new FormattingNodeConverter((name, value) => "", false);
            
            options.CDataConverter = removingNodeConverter;
            options.CommentConverter = removingNodeConverter;
            options.DocumentTypeConverter = removingNodeConverter;
            options.ProcessingInstructionConverter = removingNodeConverter;
            options.XmlDeclarationConverter = removingNodeConverter;

            options.SignificantWhitespaceConverter = removingNodeConverter;
            options.WhitespaceConverter = removingNodeConverter;
            
            options.TextConverter = new TextConverter();

            // TODO blockquote, image, hyperlink, figure out escaping, etc

            // html, body
            options.ElementConverters.Add(new NullElementConverter("ul", "ol", "menu"));

            // TODO add converters to remove script, style, head, frame, meta, iframe, frameset, col, colgroup?

            // TODO handle form elements?

            options.ElementConverters.Add(new OrderedListItemConverter());
            options.ElementConverters.Add(new BlockElementConverter("- ", "li"));

            options.ElementConverters.Add(new BlockElementConverter("# ", "h1"));
            options.ElementConverters.Add(new BlockElementConverter("## ", "h2"));
            options.ElementConverters.Add(new BlockElementConverter("### ", "h3"));
            options.ElementConverters.Add(new BlockElementConverter("#### ", "h4"));
            options.ElementConverters.Add(new BlockElementConverter("##### ", "h5"));
            options.ElementConverters.Add(new BlockElementConverter("###### ", "h6"));

            options.ElementConverters.Add(new InlineElementConverter("**", "**", "strong", "b"));
            options.ElementConverters.Add(new InlineElementConverter("*", "*", "em", "i"));

            options.ElementConverters.Add(new InlineElementConverter($"  {Environment.NewLine}", "", "br"));
            options.ElementConverters.Add(new InlineElementConverter("", $"{Environment.NewLine}{Environment.NewLine}", "p"));

            return options;
        }
    }
}
