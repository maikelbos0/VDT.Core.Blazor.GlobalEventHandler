using System;
using System.Linq;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for rendering elements as ordered list items in Markdown
    /// </summary>
    public class OrderedListItemConverter : BlockElementConverter {
        private const string orderedListName = "ol";

        /// <summary>
        /// Construct an instance of an Markdown ordered list item converter
        /// </summary>
        public OrderedListItemConverter() : base("1. ", Environment.NewLine, "li") { }

        /// <inheritdoc/>
        public override bool IsValidFor(ElementData elementData) 
            => base.IsValidFor(elementData) 
            && string.Equals(elementData.Ancestors.FirstOrDefault()?.Name, orderedListName, StringComparison.OrdinalIgnoreCase);
    }
}
