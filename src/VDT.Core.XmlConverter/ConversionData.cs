using System;
using System.Collections.Generic;
using System.Xml;

namespace VDT.Core.XmlConverter {
    /// <summary>
    /// Data relating to a conversion that is in progress
    /// </summary>
    // TODO make internal again?
    public class ConversionData {
        // TODO something with this; rename?
        internal Stack<ElementData> ElementAncestors { get; set; } = new Stack<ElementData>();

        /// <summary>
        /// Data about the node being converted if the current node is not an <see cref="XmlNodeType.Element"/>; otherwise <see langword="null"/>
        /// </summary>
        public NodeData? CurrentNodeData { get; internal set; }

        /// <summary>
        /// Data about the element being converted if the current node is an <see cref="XmlNodeType.Element"/>; otherwise <see langword="null"/>
        /// </summary>
        public ElementData? CurrentElementData { get; internal set; }

        internal void ReadNode(XmlReader reader) {
            while (ElementAncestors.Count > reader.Depth) {
                ElementAncestors.Pop();
            }

            if (reader.Depth > ElementAncestors.Count) {
                ElementAncestors.Push(CurrentElementData ?? throw new InvalidOperationException($"Expected parent node to be an {XmlNodeType.Element} but found {CurrentNodeData?.NodeType}"));
            }

            if (reader.NodeType == XmlNodeType.Element) {
                CurrentElementData = new ElementData(
                    reader.Name,
                    reader.GetAttributes(),
                    reader.IsEmptyElement,
                    ElementAncestors.ToArray()
                );
                CurrentNodeData = null;                
            }
            else {
                CurrentElementData = null;
                CurrentNodeData = new NodeData(reader.NodeType, ElementAncestors.ToArray());
            }
        }

        // public Dictionary<string, object> AdditionalData { get; } = new Dictionary<string, object>();
    }
}