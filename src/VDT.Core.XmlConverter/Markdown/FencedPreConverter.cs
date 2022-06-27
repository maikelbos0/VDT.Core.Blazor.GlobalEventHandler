using System.IO;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for rendering Markdown fenced code blocks
    /// </summary>
    public class FencedPreConverter : BlockElementConverter {        
        /// <summary>
        /// Construct an instance of a Markdown fenced pre converter
        /// </summary>
        public FencedPreConverter() : base("```", "pre") { }

        /// <inheritdoc/>
        public override void RenderEnd(ElementData elementData, TextWriter writer) {
            base.RenderEnd(elementData, writer);

            elementData.GetContentTracker().WriteLine(writer, "```");
        }
    }
}
