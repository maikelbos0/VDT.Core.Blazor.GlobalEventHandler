namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Specifies the way to handle elements that can't be converted to Markdown
    /// </summary>
    public enum UnknownElementHandlingMode {
        /// <summary>
        /// Leave the elements as-is
        /// </summary>
        None = 0,

        /// <summary>
        /// Remove only the tags but render the child content of the elements
        /// </summary>
        RemoveTags = 1,

        /// <summary>
        /// Remove the entire elements including child content
        /// </summary>
        RemoveElements = 2
    }
}
