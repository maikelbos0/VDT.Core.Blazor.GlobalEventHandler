namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Specifies the method by which preformatted text get converted to Markdown code blocks
    /// </summary>
    public enum PreConversionMode {
        /// <summary>
        /// Surround code blocks with triple backticks (```)
        /// </summary>
        Fenced,

        /// <summary>
        /// Indent code inside code blocks blocks with a tab
        /// </summary>
        Indented
    }
}