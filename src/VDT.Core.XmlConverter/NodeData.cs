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

        internal NodeData(XmlNodeType nodeType, IList<ElementData> ancestors) {
            NodeType = nodeType;
            Ancestors = new ReadOnlyCollection<ElementData>(ancestors);
        }
    }
}
