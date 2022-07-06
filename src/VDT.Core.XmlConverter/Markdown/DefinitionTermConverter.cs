using System.IO;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for rendering elements as definition terms inside a definition list in Markdown
    /// </summary>
    public class DefinitionTermConverter : BaseElementConverter {
        /// <summary>
        /// Construct an instance of a Markdown definition term converter
        /// </summary>
        public DefinitionTermConverter() : base("dt") { }

        /// <inheritdoc/>
        public override void RenderStart(ElementData elementData, TextWriter writer) {
            var tracker = elementData.GetContentTracker();

            if (!elementData.IsFirstChild) {
                while (tracker.TrailingNewLineCount < 2) {
                    tracker.WriteLine(writer);
                }
            }
        }

        /// <inheritdoc/>
        public override void RenderEnd(ElementData elementData, TextWriter writer) {
            var tracker = elementData.GetContentTracker();

            if (!tracker.HasTrailingNewLine) {
                tracker.WriteLine(writer);
            }
        }
    }
}
