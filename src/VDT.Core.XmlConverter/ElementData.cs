using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VDT.Core.XmlConverter {
    /// <summary>
    /// Information about an element being converted
    /// </summary>
    public class ElementData : INodeData {
        /// <summary>
        /// Tag name of the element
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Collection of attributes found on the element
        /// </summary>
        public IReadOnlyDictionary<string, string> Attributes { get; }

        /// <summary>
        /// <see langword="true"/> if the element is an empty, self-closing element and <see langword="false"/> if the element has a separate opening and closing tag
        /// </summary>
        public bool IsSelfClosing { get; }

        /// <inheritdoc/>
        public IReadOnlyList<ElementData> Ancestors { get; }

        /// <inheritdoc/>
        public bool IsFirstChild { get; }

        /// <inheritdoc/>
        public Dictionary<string, object?> AdditionalData { get; }

        internal ElementData(string name, Dictionary<string, string> attributes, bool isSelfClosing, IList<ElementData> ancestors, bool isFirstChild, Dictionary<string, object?> additionalData) {
            Name = name;
            Attributes = new ReadOnlyDictionary<string, string>(attributes);
            IsSelfClosing = isSelfClosing;
            Ancestors = new ReadOnlyCollection<ElementData>(ancestors);
            IsFirstChild = isFirstChild;
            AdditionalData = additionalData;
        }
    }
}
