using System;

namespace VDT.Core.XmlConverter.Markdown {
    public static class ConverterOptionsExtensions {
        // TODO default converter: unknownelementhandlemode enum?
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

            // TODO blockquote

            // Register pre content converter before any other element converters so it clears 
            options.ElementConverters.Add(new PreContentConverter());
            options.ElementConverters.Add(new PreConverter());

            options.ElementConverters.Add(new NullElementConverter("html", "body", "ul", "ol", "menu", "div", "span"));
            options.ElementConverters.Add(new ElementRemovingConverter("script", "style", "head", "frame", "meta", "iframe", "frameset", "col", "colgroup"));

            // TODO handle form elements?

            options.ElementConverters.Add(new HyperlinkConverter());
            options.ElementConverters.Add(new ImageConverter());

            options.ElementConverters.Add(new OrderedListItemConverter());
            options.ElementConverters.Add(new UnorderedListItemConverter());
            
            options.ElementConverters.Add(new BlockElementConverter("# ", "h1"));
            options.ElementConverters.Add(new BlockElementConverter("## ", "h2"));
            options.ElementConverters.Add(new BlockElementConverter("### ", "h3"));
            options.ElementConverters.Add(new BlockElementConverter("#### ", "h4"));
            options.ElementConverters.Add(new BlockElementConverter("##### ", "h5"));
            options.ElementConverters.Add(new BlockElementConverter("###### ", "h6"));

            options.ElementConverters.Add(new ParagraphConverter());

            options.ElementConverters.Add(new InlineElementConverter("**", "**", "strong", "b"));
            options.ElementConverters.Add(new InlineElementConverter("*", "*", "em", "i"));
            options.ElementConverters.Add(new InlineElementConverter("`", "`", "code", "kbd", "samp", "var"));

            options.ElementConverters.Add(new InlineElementConverter($"  {Environment.NewLine}", "", "br"));

            return options;
        }
    }
}
