namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Builder for adding converter options to convert HTML markup to Markdown format
    /// </summary>
    public class ConverterOptionsBuilder {
        private UnknownElementHandlingMode unknownElementHandlingMode;

        /// <summary>
        /// Specify the way to handle elements that can't be converted to Markdown
        /// </summary>
        /// <param name="unknownElementHandlingMode">Specifies the way to handle elements that can't be converted to Markdown</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public ConverterOptionsBuilder UseUnknownElementHandlingMode(UnknownElementHandlingMode unknownElementHandlingMode) {
            this.unknownElementHandlingMode = unknownElementHandlingMode;

            return this;
        }

        /// <summary>
        /// Build converter options to convert HTML markup to Markdown format
        /// </summary>
        /// <returns></returns>
        public ConverterOptions Build() {
            return Build(new ConverterOptions(), new ConverterOptionsAssembler());
        }

        internal ConverterOptions Build(ConverterOptions options, IConverterOptionsAssembler assembler) {
            assembler.SetNodeRemovingConverterForNonMarkdownNodeTypes(options);

            return options
                .AddDefaultMarkdown()
                .UseUnknownElementHandlingMode(unknownElementHandlingMode);
        }
    }
}
