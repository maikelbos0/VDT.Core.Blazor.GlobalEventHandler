using System.IO;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for rendering unknown elements in Markdown
    /// </summary>
    public class UnknownElementConverter : IElementConverter {
        /// <summary>
        /// Determines if the child nodes of the current element should be rendered
        /// </summary>
        public bool RenderContent { get; }

        /// <summary>
        /// Construct an instance of a Markdown unknown element converter
        /// </summary>
        /// <param name="renderContent">Determines if the child nodes of the current element should be rendered</param>
        public UnknownElementConverter(bool renderContent) {
            RenderContent = renderContent;
        }

        /// <inheritdoc/>
        public bool IsValidFor(ElementData elementData) => true;

        /// <inheritdoc/>
        public void RenderStart(ElementData elementData, TextWriter writer) { }

        /// <inheritdoc/>
        public bool ShouldRenderContent(ElementData elementData) => RenderContent;

        /// <inheritdoc/>
        public void RenderEnd(ElementData elementData, TextWriter writer) { }
    }
}
