using System;
using VDT.Core.XmlConverter.Elements;
using VDT.Core.XmlConverter.Nodes;

namespace VDT.Core.XmlConverter.Markdown {
    public static class MarkdownConverterOptionsExtensions {
        public static ConverterOptions UseMarkdown(this ConverterOptions options) {

            var removingNodeConverter = new FormattingNodeConverter((name, value) => "", false);

            options.WhitespaceConverter = removingNodeConverter;
            options.SignificantWhitespaceConverter = removingNodeConverter;
            
            options.TextConverter = new FormattingNodeConverter((name, value) => value.Trim(), false);

            // TODO blockquote, figure out escaping, whitespace, lists, etc

            options.ElementConverters.Add(new BasicElementConverter("# ", $"{Environment.NewLine}{Environment.NewLine}", "h1"));
            options.ElementConverters.Add(new BasicElementConverter("## ", $"{Environment.NewLine}{Environment.NewLine}", "h2"));
            options.ElementConverters.Add(new BasicElementConverter("### ", $"{Environment.NewLine}{Environment.NewLine}", "h3"));
            options.ElementConverters.Add(new BasicElementConverter("#### ", $"{Environment.NewLine}{Environment.NewLine}", "h4"));
            options.ElementConverters.Add(new BasicElementConverter("##### ", $"{Environment.NewLine}{Environment.NewLine}", "h5"));
            options.ElementConverters.Add(new BasicElementConverter("###### ", $"{Environment.NewLine}{Environment.NewLine}", "h6"));

            options.ElementConverters.Add(new BasicElementConverter("**", "**", "strong", "b"));
            options.ElementConverters.Add(new BasicElementConverter("*", "*", "em", "i"));

            options.ElementConverters.Add(new BasicElementConverter($"  {Environment.NewLine}", "", "br"));
            options.ElementConverters.Add(new BasicElementConverter("", $"{Environment.NewLine}{Environment.NewLine}", "p"));

            // TODO handle other node types

            return options;
        }
    }
}
