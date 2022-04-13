using System.Collections.Generic;
using System.Xml;

namespace VDT.Core.XmlConverter {
    /// <summary>
    /// Data relating to a conversion that is in progress
    /// </summary>
    public class ConversionData {
        internal Stack<ElementData> ElementAncestors { get; set; } = new Stack<ElementData>();

        /// <summary>
        /// Type of the current node of the <see cref="XmlReader"/> being converted
        /// </summary>
        public XmlNodeType CurrentNodeType { get; private set; } = XmlNodeType.None;

        /// <summary>
        /// Data about the element being converted if the current node is a <see cref="XmlNodeType.Element"/>; otherwise <see langword="null"/>
        /// </summary>
        public ElementData? CurrentElement { get; private set; }

        internal void ReadNode(XmlReader reader) {
            CurrentNodeType = reader.NodeType;

            if (CurrentNodeType == XmlNodeType.Element) {
                CurrentElement = new ElementData(
                    reader.Name,
                    reader.GetAttributes(),
                    reader.IsEmptyElement,
                    ElementAncestors.ToArray()
                );
            }
            else {
                CurrentElement = null;
            }
        }

        // public Dictionary<string, object> AdditionalData { get; } = new Dictionary<string, object>();
    }
}