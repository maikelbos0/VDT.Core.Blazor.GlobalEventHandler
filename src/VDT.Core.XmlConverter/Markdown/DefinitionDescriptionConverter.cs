using System.IO;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for rendering elements as descriptions of definition terms inside a definition list in Markdown
    /// </summary>
    public class DefinitionDescriptionConverter : BlockElementConverter {
        /// <summary>
        /// Construct an instance of a Markdown definition description converter
        /// </summary>
        public DefinitionDescriptionConverter() : base(": ", "dd") { }

        /// <inheritdoc/>
        public override void RenderStart(ElementData elementData, TextWriter writer) {
            base.RenderStart(elementData, writer);
            elementData.GetContentTracker().Prefixes.Push("\t");
        }

        /// <inheritdoc/>
        public override void RenderEnd(ElementData elementData, TextWriter writer) {
            elementData.GetContentTracker().Prefixes.Pop();
            base.RenderEnd(elementData, writer);
        }
    }
}
