using System;
using System.IO;
using System.Linq;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for rendering elements as block Markdown requiring indentation when nesting
    /// </summary>
    public class BlockElementConverter : BaseElementConverter {
        private const string listItemName = "li";

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
            if (!elementData.IsFirstChild) {
                writer.WriteLine();
                writer.Write(new string('\t', GetAncestorListItemCount(elementData)));
            }

            writer.Write(startOutput);
        }

        /// <inheritdoc/>
        public override void RenderEnd(ElementData elementData, TextWriter writer) {
            writer.WriteLine();
        }

        internal int GetAncestorListItemCount(ElementData elementData) => elementData.Ancestors.Count(d => string.Equals(d.Name, listItemName, StringComparison.OrdinalIgnoreCase));
    }
}
