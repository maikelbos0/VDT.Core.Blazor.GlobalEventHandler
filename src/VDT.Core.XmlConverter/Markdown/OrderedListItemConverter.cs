using System;
using System.IO;
using System.Linq;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for rendering elements as ordered list items in Markdown
    /// </summary>
    public class OrderedListItemConverter : BlockElementConverter {
        private const string orderedListName = "ol";

        /// <summary>
        /// Construct an instance of a Markdown ordered list item converter
        /// </summary>
        public OrderedListItemConverter() : base("1. ", "li") { }

        /// <inheritdoc/>
        public override bool IsValidFor(ElementData elementData) 
            => base.IsValidFor(elementData) 
            && string.Equals(elementData.Ancestors.FirstOrDefault()?.Name, orderedListName, StringComparison.OrdinalIgnoreCase);

        /// <inheritdoc/>
        public override void RenderStart(ElementData elementData, TextWriter writer) {
            base.RenderStart(elementData, writer);
            elementData.GetContentTracker().Prefixes.Push("\t");
        }

        /// <inheritdoc/>
        override public void RenderEnd(ElementData elementData, TextWriter writer) {
            elementData.GetContentTracker().Prefixes.Pop();
            base.RenderEnd(elementData, writer);
        }
    }
}
