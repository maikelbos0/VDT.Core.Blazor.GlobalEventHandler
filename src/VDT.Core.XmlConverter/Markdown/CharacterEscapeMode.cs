namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Specifies the way character escapes in Markdown text are handled
    /// </summary>
    public enum CharacterEscapeMode {
        /// <summary>
        /// Escape all known characters and characters provided in <see cref="ConverterOptionsBuilder.CustomCharacterEscapes"/>
        /// </summary>
        Full,

        /// <summary>
        /// Escape only characters used in the element converters added according to <see cref="ConverterOptionsBuilder.ElementConverterTargets"/> and characters provided in 
        /// <see cref="ConverterOptionsBuilder.CustomCharacterEscapes"/>
        /// </summary>
        ConverterBased,

        /// <summary>
        /// Escape only characters provided in <see cref="ConverterOptionsBuilder.CustomCharacterEscapes"/>
        /// </summary>
        CustomOnly
    }
}
