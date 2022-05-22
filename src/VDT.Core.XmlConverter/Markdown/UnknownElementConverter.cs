using System.IO;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for rendering unknown elements in Markdown
    /// </summary>
    public class UnknownElementConverter : IElementConverter {
        private readonly bool shouldRenderContent;

        /// <summary>
        /// Construct an instance of a Markdown unknown element converter
        /// </summary>
        /// <param name="shouldRenderContent">Determines if the child nodes of the current element should be rendered</param>
        public UnknownElementConverter(bool shouldRenderContent) {
            this.shouldRenderContent = shouldRenderContent;
        }

        /// <inheritdoc/>
        public bool IsValidFor(ElementData elementData) => true;

        /// <inheritdoc/>
        public void RenderStart(ElementData elementData, TextWriter writer) { }

        /// <inheritdoc/>
        public bool ShouldRenderContent(ElementData elementData) => shouldRenderContent;

        /// <inheritdoc/>
        public void RenderEnd(ElementData elementData, TextWriter writer) { }
    }
}
