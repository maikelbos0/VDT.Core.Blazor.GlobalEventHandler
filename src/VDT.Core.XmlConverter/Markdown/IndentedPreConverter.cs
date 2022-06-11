using System.IO;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for rendering Markdown indented code blocks
    /// </summary>
    public class IndentedPreConverter : BlockElementConverter {        
        /// <summary>
        /// Construct an instance of a Markdown indented pre converter
        /// </summary>
        public IndentedPreConverter() : base("", "pre") { }

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
