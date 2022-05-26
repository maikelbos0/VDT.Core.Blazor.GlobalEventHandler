namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Extension methods for adding Markdown converters to <see cref="ConverterOptions"/>
    /// </summary>
    public static class ConverterOptionsExtensions {
        /// <summary>
        /// Add converters for all markup supported by basic Markdown
        /// </summary>
        /// <param name="options">The options for converting XML</param>
        /// <param name="unknownElementHandlingMode">Specifies the way to handle elements that can't be converted to Markdown</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public static ConverterOptions UseMarkdown(this ConverterOptions options, UnknownElementHandlingMode unknownElementHandlingMode = UnknownElementHandlingMode.None) {
            var removingNodeConverter = new FormattingNodeConverter((name, value) => "", false);
            
            options.CDataConverter = removingNodeConverter;
            options.CommentConverter = removingNodeConverter;
            options.DocumentTypeConverter = removingNodeConverter;
            options.ProcessingInstructionConverter = removingNodeConverter;
            options.XmlDeclarationConverter = removingNodeConverter;

            options.SignificantWhitespaceConverter = removingNodeConverter;
            options.WhitespaceConverter = removingNodeConverter;
            
            options.TextConverter = new TextConverter();

            // Register pre content converter before any other element converters so it clears 
            options.ElementConverters.Add(new PreContentConverter());
            options.ElementConverters.Add(new PreConverter());

            options.ElementConverters.Add(new OrderedListItemConverter());
            options.ElementConverters.Add(new UnorderedListItemConverter());
                        
            options.ElementConverters.Add(new BlockElementConverter("# ", "h1"));
            options.ElementConverters.Add(new BlockElementConverter("## ", "h2"));
            options.ElementConverters.Add(new BlockElementConverter("### ", "h3"));
            options.ElementConverters.Add(new BlockElementConverter("#### ", "h4"));
            options.ElementConverters.Add(new BlockElementConverter("##### ", "h5"));
            options.ElementConverters.Add(new BlockElementConverter("###### ", "h6"));
            options.ElementConverters.Add(new BlockElementConverter("---", "hr"));

            options.ElementConverters.Add(new BlockquoteConverter());
            options.ElementConverters.Add(new ParagraphConverter());
            options.ElementConverters.Add(new LinebreakConverter());

            options.ElementConverters.Add(new HyperlinkConverter());
            options.ElementConverters.Add(new ImageConverter());

            options.ElementConverters.Add(new InlineElementConverter("**", "**", "strong", "b"));
            options.ElementConverters.Add(new InlineElementConverter("*", "*", "em", "i"));
            options.ElementConverters.Add(new InlineElementConverter("`", "`", "code", "kbd", "samp", "var"));

            options.ElementConverters.Add(new NullElementConverter("html", "body", "ul", "ol", "menu", "div", "span"));
            options.ElementConverters.Add(new ElementRemovingConverter("script", "style", "head", "frame", "meta", "iframe", "frameset"));

            switch (unknownElementHandlingMode) {
                case UnknownElementHandlingMode.RemoveTags:
                    options.DefaultElementConverter = new UnknownElementConverter(true);
                    break;
                case UnknownElementHandlingMode.RemoveElements:
                    options.DefaultElementConverter = new UnknownElementConverter(false);
                    break;
            }

            return options;
        }

        /// <summary>
        /// Add converters for all markup supported by extended Markdown
        /// </summary>
        /// <param name="options">The options for converting XML</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public static ConverterOptions AddExtendedMarkdown(this ConverterOptions options) {
            options.ElementConverters.Add(new InlineElementConverter("~~", "~~", "del"));
            options.ElementConverters.Add(new InlineElementConverter("==", "==", "mark"));
            options.ElementConverters.Add(new InlineElementConverter("~", "~", "sub"));
            options.ElementConverters.Add(new InlineElementConverter("^", "^", "sup"));

            return options;
        }
    }
}
