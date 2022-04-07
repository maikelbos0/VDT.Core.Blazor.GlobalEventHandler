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
        private readonly string endOutput;

        /// <summary>
        /// Constructs an instance of a block Markdown element converter
        /// </summary>
        /// <param name="startOutput"></param>
        /// <param name="endOutput"></param>
        /// <param name="validForElementNames">Element names for which this converter is valid; names are case-insensitive</param>
        public BlockElementConverter(string startOutput, string endOutput, params string[] validForElementNames) : base(validForElementNames) {
            this.startOutput = startOutput;
            this.endOutput = endOutput;
        }

        /// <inheritdoc/>
        public override void RenderStart(ElementData elementData, TextWriter writer) {
            writer.Write(new string('\t', GetAncestorListItemCount(elementData)));
            writer.Write(startOutput);
        }

        /// <inheritdoc/>
        public override void RenderEnd(ElementData elementData, TextWriter writer) => writer.Write(endOutput);

        internal int GetAncestorListItemCount(ElementData elementData) => elementData.Ancestors.Count(d => string.Equals(d.Name, listItemName, StringComparison.OrdinalIgnoreCase));
    }
}
