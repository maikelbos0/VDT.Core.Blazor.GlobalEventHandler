using System.IO;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for rendering hyperlinks as Markdown
    /// </summary>
    public class HyperlinkConverter : BaseElementConverter {
        /// <summary>
        /// Construct an instance of a hyperlink converter
        /// </summary>
        public HyperlinkConverter() : base("a") { }

        /// <inheritdoc/>
        public override void RenderStart(ElementData elementData, TextWriter writer)
            => elementData.GetContentTracker().Write(writer, "[");

        /// <inheritdoc/>
        public override void RenderEnd(ElementData elementData, TextWriter writer) {
            var tracker = elementData.GetContentTracker();
            
            tracker.Write(writer, "](");

            if (elementData.TryGetAttribute("href", out var url)) {
                tracker.Write(writer, url);
            }

            if (elementData.TryGetAttribute("title", out var title)) {
                tracker.Write(writer, " \"");
                tracker.Write(writer, title);
                tracker.Write(writer, "\"");
            }

            tracker.Write(writer, ")");
        }
    }
}
