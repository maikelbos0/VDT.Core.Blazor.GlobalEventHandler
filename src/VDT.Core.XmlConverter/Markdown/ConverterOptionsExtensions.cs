namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Extension methods for adding Markdown converters to <see cref="ConverterOptions"/>
    /// </summary>
    public static class ConverterOptionsExtensions {
        /// <summary>
        /// Add converters for converting HTML markup to Markdown
        /// </summary>
        /// <param name="options">The options for converting XML</param>
        /// <param name="useExtendedSyntax"><see langword="true"/> to add support for extended Markdown syntax; otherwise <see langword="false"/></param>
        /// <param name="unknownElementHandlingMode">Specifies the way to handle elements that can't be converted to Markdown</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public static ConverterOptions UseMarkdown(
            this ConverterOptions options,
            bool useExtendedSyntax = false,
            UnknownElementHandlingMode? unknownElementHandlingMode = null
        ) {
            var builder = new ConverterOptionsBuilder();

            if (useExtendedSyntax) {
                builder.AddAllElementConverters();
            }

            if (unknownElementHandlingMode != null) {
                builder.UseUnknownElementHandlingMode(unknownElementHandlingMode.Value);
            }

            return builder.Build(options, new ConverterOptionsAssembler());
        }
    }
}
