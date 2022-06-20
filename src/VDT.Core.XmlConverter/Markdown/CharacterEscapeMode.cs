namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Specifies the way character escapes in Markdown text are handled
    /// </summary>
    public enum CharacterEscapeMode {
        /// <summary>
        /// Escape html characters, all known Markdown special characters and those provided in <see cref="ConverterOptionsBuilder.CustomCharacterEscapes"/>
        /// </summary>
        Full,

        /// <summary>
        /// Escape html characters, characters used in the element converters added according to <see cref="ConverterOptionsBuilder.ElementConverterTargets"/>
        /// and those provided in <see cref="ConverterOptionsBuilder.CustomCharacterEscapes"/>
        /// </summary>
        ElementConverterBased,

        /// <summary>
        /// Escape html characters and those provided in <see cref="ConverterOptionsBuilder.CustomCharacterEscapes"/>
        /// </summary>
        Custom,

        /// <summary>
        /// Escape only characters provided in <see cref="ConverterOptionsBuilder.CustomCharacterEscapes"/>
        /// </summary>
        CustomOnly
    }
}
