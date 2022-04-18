using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace VDT.Core.XmlConverter {
    /// <summary>
    /// Information about a node being converted
    /// </summary>
    public class NodeData {
        /// <summary>
        /// Type of the node
        /// </summary>
        public XmlNodeType NodeType { get; }

        /// <summary>
        /// Ancestor elements to the current node in order from lowest (direct parent) to highest (most far removed ancestor)
        /// </summary>
        public IReadOnlyList<ElementData> Ancestors { get; }

        /// <summary>
        /// Additional data that is shared by the entire conversion of an xml document and can be freely used by converters
        /// </summary>
        public Dictionary<string, object> AdditionalData { get; }

        internal NodeData(XmlNodeType nodeType, IList<ElementData> ancestors, Dictionary<string, object> additionalData) {
            NodeType = nodeType;
            Ancestors = new ReadOnlyCollection<ElementData>(ancestors);
            AdditionalData = additionalData;
        }
    }
}
