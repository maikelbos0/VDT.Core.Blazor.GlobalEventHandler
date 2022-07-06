using System.IO;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for lists containing list items of any type
    /// </summary>
    public class ListConverter : BaseElementConverter {
        /// <summary>
        /// Construct an instance of a Markdown list converter
        /// </summary>
        /// <param name="validForElementNames">Element names for which this converter is valid; names are case-insensitive</param>
        public ListConverter(params string[] validForElementNames) : base(validForElementNames) { }

        /// <inheritdoc/>
        public override void RenderStart(ElementData elementData, TextWriter writer) { }

        /// <inheritdoc/>
        public override void RenderEnd(ElementData elementData, TextWriter writer) {
            var tracker = elementData.GetContentTracker();

            while (tracker.TrailingNewLineCount < 2) {
                tracker.WriteLine(writer);
            }
        }
    }
}
