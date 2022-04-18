using System;
using System.Collections.Generic;
using System.Xml;

namespace VDT.Core.XmlConverter {
    internal class ConversionData {
        internal Stack<ElementData> Ancestors { get; } = new Stack<ElementData>();

        internal Dictionary<string, object> AdditionalData { get; } = new Dictionary<string, object>();

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
                    Ancestors.ToArray(),
                    AdditionalData
                );
                CurrentNodeData = null;                
            }
            else {
                CurrentNodeData = new NodeData(
                    reader.NodeType, 
                    Ancestors.ToArray(),
                    AdditionalData
                );
                CurrentElementData = null;
            }
        }
    }
}