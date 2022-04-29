using System.IO;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for removing elements that contain no rendered content
    /// </summary>
    public class ElementRemovingConverter : BaseElementConverter {
        /// <summary>
        /// Construct an instance of a null Markdown element converter
        /// </summary>
        /// <param name="validForElementNames">Element names for which this converter is valid; names are case-insensitive</param>
        public ElementRemovingConverter(params string[] validForElementNames) : base(validForElementNames) { }

        /// <inheritdoc/>
        public override void RenderStart(ElementData elementData, TextWriter writer) { }

        /// <inheritdoc/>
        public override bool ShouldRenderContent(ElementData elementData) => false;

        /// <inheritdoc/>
        public override void RenderEnd(ElementData elementData, TextWriter writer) { }
    }
}
