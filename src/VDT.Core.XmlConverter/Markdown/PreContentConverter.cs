using System;
using System.IO;
using System.Linq;

namespace VDT.Core.XmlConverter.Markdown {
    internal class PreContentConverter : IElementConverter {
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
