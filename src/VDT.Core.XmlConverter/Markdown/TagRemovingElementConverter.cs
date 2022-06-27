using System.IO;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for rendering elements as inline Markdown without markup
    /// </summary>
    public class TagRemovingElementConverter : BaseElementConverter {
        /// <summary>
        /// Construct an instance of a Markdown null element converter
        /// </summary>
        /// <param name="validForElementNames">Element names for which this converter is valid; names are case-insensitive</param>
        public TagRemovingElementConverter(params string[] validForElementNames) : base(validForElementNames) { }

        /// <inheritdoc/>
        public override void RenderStart(ElementData elementData, TextWriter writer) { }

        /// <inheritdoc/>
        public override void RenderEnd(ElementData elementData, TextWriter writer) { }
    }
}
