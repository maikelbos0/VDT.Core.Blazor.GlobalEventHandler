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
            return new ConverterOptionsBuilder()
                .UseUnknownElementHandlingMode(unknownElementHandlingMode)
                .Build(options, new ConverterOptionsAssembler());
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
