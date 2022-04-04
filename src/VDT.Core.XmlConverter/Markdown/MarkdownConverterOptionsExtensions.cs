using System;
using VDT.Core.XmlConverter.Elements;

namespace VDT.Core.XmlConverter.Markdown {
    public static class MarkdownConverterOptionsExtensions {
        public static ConverterOptions UseMarkdown(this ConverterOptions options) {
            // TODO linebreak, paragraph, blockquote, figure out escaping, whitespace, lists, etc

            options.ElementConverters.Add(new BasicElementConverter("# ", $"{Environment.NewLine}{Environment.NewLine}", "h1"));
            options.ElementConverters.Add(new BasicElementConverter("## ", $"{Environment.NewLine}{Environment.NewLine}", "h2"));
            options.ElementConverters.Add(new BasicElementConverter("### ", $"{Environment.NewLine}{Environment.NewLine}", "h3"));
            options.ElementConverters.Add(new BasicElementConverter("#### ", $"{Environment.NewLine}{Environment.NewLine}", "h4"));
            options.ElementConverters.Add(new BasicElementConverter("##### ", $"{Environment.NewLine}{Environment.NewLine}", "h5"));
            options.ElementConverters.Add(new BasicElementConverter("###### ", $"{Environment.NewLine}{Environment.NewLine}", "h6"));

            options.ElementConverters.Add(new BasicElementConverter("**", "**", "strong", "b"));
            options.ElementConverters.Add(new BasicElementConverter("*", "*", "em", "i"));

            // TODO handle other node types

            return options;
        }
    }
}
