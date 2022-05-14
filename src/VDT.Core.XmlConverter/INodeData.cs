using System.Collections.Generic;

namespace VDT.Core.XmlConverter {
    /// <summary>
    /// Information about a node being converted
    /// </summary>
    public interface INodeData {
        /// <summary>
        /// Ancestor elements to the current node in order from lowest (direct parent) to highest (most far removed ancestor)
        /// </summary>
        IReadOnlyList<ElementData> Ancestors { get; }

        /// <summary>
        /// <see langword="true"/> if this node is the first child of its parent, otherwise <see langword="false"/>
        /// </summary>
        bool IsFirstChild { get; }

        /// <summary>
        /// Additional data that is shared by the entire conversion of an XML document and can be freely used by converters
        /// </summary>
        Dictionary<string, object?> AdditionalData { get; }
    }
}
