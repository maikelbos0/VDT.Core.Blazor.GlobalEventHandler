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
            
            options.TextConverter = new FormattingNodeConverter((name, value) => value.Trim(), false);

            // TODO blockquote, figure out escaping, lists, etc

            options.ElementConverters.Add(new BlockElementConverter("# ", $"{Environment.NewLine}", "h1"));
            options.ElementConverters.Add(new BlockElementConverter("## ", $"{Environment.NewLine}", "h2"));
            options.ElementConverters.Add(new BlockElementConverter("### ", $"{Environment.NewLine}", "h3"));
            options.ElementConverters.Add(new BlockElementConverter("#### ", $"{Environment.NewLine}", "h4"));
            options.ElementConverters.Add(new BlockElementConverter("##### ", $"{Environment.NewLine}", "h5"));
            options.ElementConverters.Add(new BlockElementConverter("###### ", $"{Environment.NewLine}", "h6"));

            options.ElementConverters.Add(new InlineElementConverter("**", "**", "strong", "b"));
            options.ElementConverters.Add(new InlineElementConverter("*", "*", "em", "i"));

            options.ElementConverters.Add(new InlineElementConverter($"  {Environment.NewLine}", "", "br"));
            options.ElementConverters.Add(new InlineElementConverter("", $"{Environment.NewLine}{Environment.NewLine}", "p"));

            // TODO handle other node types

            return options;
        }
    }
}
