using System;
using System.IO;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for rendering code blocks as Markdown
    /// </summary>
    public class PreConverter : BlockElementConverter {
        /// <summary>
        /// Construct an instance of a pre converter
        /// </summary>
        public PreConverter() : base($"```{Environment.NewLine}", "pre") {
        }

        /// <inheritdoc/>
        public override void RenderEnd(ElementData elementData, TextWriter writer) {
            base.RenderEnd(elementData, writer);

            var tracker = elementData.GetContentTracker();

            tracker.Write(writer, new string('\t', GetAncestorListItemCount(elementData)));
            tracker.WriteLine(writer, "```");
            tracker.WriteLine(writer);
        }
    }
}
