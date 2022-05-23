using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace VDT.Core.XmlConverter {
    /// <summary>
    /// Information about a node being converted
    /// </summary>
    public class NodeData : INodeData {
        /// <summary>
        /// Type of the node
        /// </summary>
        public XmlNodeType NodeType { get; }

        /// <summary>
        /// Qualified name
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Text value
        /// </summary>
        public string Value { get; }

        /// <inheritdoc/>
        public IReadOnlyList<ElementData> Ancestors { get; }

        /// <inheritdoc/>
        public bool IsFirstChild { get; }

        /// <inheritdoc/>
        public Dictionary<string, object?> AdditionalData { get; }

        internal NodeData(XmlNodeType nodeType, string name, string value, IList<ElementData> ancestors, bool isFirstChild, Dictionary<string, object?> additionalData) {
            NodeType = nodeType;
            Name = name;
            Value = value;
            Ancestors = new ReadOnlyCollection<ElementData>(ancestors);
            IsFirstChild = isFirstChild;
            AdditionalData = additionalData;
        }
    }
}
