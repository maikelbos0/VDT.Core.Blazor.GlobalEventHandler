using System;
using System.IO;
using System.Linq;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for rendering elements as content inside a Markdown code block
    /// </summary>
    public class PreContentConverter : IElementConverter {
        private const string preName = "pre";

        /// <inheritdoc/>
        public bool IsValidFor(ElementData elementData) => elementData.Ancestors.Any(e => string.Equals(e.Name, preName, StringComparison.OrdinalIgnoreCase));

        /// <inheritdoc/>
        public void RenderStart(ElementData elementData, TextWriter writer) { }

        /// <inheritdoc/>
        public bool ShouldRenderContent(ElementData elementData) => true;

        /// <inheritdoc/>
        public void RenderEnd(ElementData elementData, TextWriter writer) { }
    }
}
