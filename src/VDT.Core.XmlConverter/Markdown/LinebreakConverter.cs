using System.IO;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for rendering linebreaks in Markdown
    /// </summary>
    public class LinebreakConverter : BaseElementConverter {
        /// <summary>
        /// Construct an instance of an Markdown linebreak converter
        /// </summary>
        public LinebreakConverter() : base("br") { }

        /// <inheritdoc/>
        public override void RenderStart(ElementData elementData, TextWriter writer) {
            elementData.GetContentTracker().WriteLine(writer, "  ");
        }

        /// <inheritdoc/>
        public override void RenderEnd(ElementData elementData, TextWriter writer) { }
    }
}
