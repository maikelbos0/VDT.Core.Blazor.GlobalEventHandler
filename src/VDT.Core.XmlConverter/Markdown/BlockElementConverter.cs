using System.IO;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for rendering elements as block Markdown requiring indentation when nesting
    /// </summary>
    public class BlockElementConverter : BaseElementConverter {
        private readonly string startOutput;

        /// <summary>
        /// Constructs an instance of a block Markdown element converter
        /// </summary>
        /// <param name="startOutput">Value to render at the start of the element, before any possible child content is rendered</param>
        /// <param name="validForElementNames">Element names for which this converter is valid; names are case-insensitive</param>
        public BlockElementConverter(string startOutput, params string[] validForElementNames) : base(validForElementNames) {
            this.startOutput = startOutput;
        }

        /// <inheritdoc/>
        public override void RenderStart(ElementData elementData, TextWriter writer) {
            var tracker = elementData.GetContentTracker();

            if (!elementData.IsFirstChild) {
                if (!tracker.HasTrailingNewLine) {
                    tracker.WriteLine(writer);
                }

                tracker.Write(writer, new string('\t', GetAncestorListItemCount(elementData)));
            }

            tracker.Write(writer, startOutput);
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
