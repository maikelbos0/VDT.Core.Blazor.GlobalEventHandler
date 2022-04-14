using System.IO;
using System.Security;

namespace VDT.Core.XmlConverter {
    /// <summary>
    /// Converter that outputs xml semantically identical to the input
    /// </summary>
    public class NoOpElementConverter : IElementConverter {
        /// <inheritdoc/>
        public bool IsValidFor(ElementData elementData) => true;

        /// <inheritdoc/>
        public void RenderStart(ElementData elementData, TextWriter writer) {
            writer.Write("<");
            writer.Write(elementData.Name);

            foreach (var attribute in elementData.Attributes) {
                writer.Write(" ");
                writer.Write(attribute.Key);
                writer.Write("=\"");
                writer.Write(SecurityElement.Escape(attribute.Value));
                writer.Write("\"");
            }

            if (elementData.IsSelfClosing) {
                writer.Write("/");
            }

            writer.Write(">");
        }

        /// <inheritdoc/>
        public bool ShouldRenderContent(ElementData elementData) => !elementData.IsSelfClosing;

        /// <inheritdoc/>
        public void RenderEnd(ElementData elementData, TextWriter writer) {
            if (!elementData.IsSelfClosing) {
                writer.Write("</");
                writer.Write(elementData.Name);
                writer.Write(">");
            }
        }
    }
}
