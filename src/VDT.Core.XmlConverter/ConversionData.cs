using System;
using System.Collections.Generic;
using System.Xml;

namespace VDT.Core.XmlConverter {
    internal class ConversionData {
        internal Stack<ElementData> Ancestors { get; set; } = new Stack<ElementData>();

        internal NodeData? CurrentNodeData { get; set; }

        internal ElementData? CurrentElementData { get; set; }

        internal void ReadNode(XmlReader reader) {
            while (Ancestors.Count > reader.Depth) {
                Ancestors.Pop();
            }

            if (reader.Depth > Ancestors.Count) {
                Ancestors.Push(CurrentElementData ?? throw new InvalidOperationException($"Expected parent node to be an {XmlNodeType.Element} but found {CurrentNodeData?.NodeType}"));
            }

            if (reader.NodeType == XmlNodeType.Element) {
                CurrentElementData = new ElementData(
                    reader.Name,
                    reader.GetAttributes(),
                    reader.IsEmptyElement,
                    Ancestors.ToArray()
                );
                CurrentNodeData = null;                
            }
            else {
                CurrentElementData = null;
                CurrentNodeData = new NodeData(reader.NodeType, Ancestors.ToArray());
            }
        }

        // public Dictionary<string, object> AdditionalData { get; } = new Dictionary<string, object>();
    }
}