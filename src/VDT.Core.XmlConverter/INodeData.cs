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
        /// Additional data that is shared by the entire conversion of an xml document and can be freely used by converters
        /// </summary>
        Dictionary<string, object?> AdditionalData { get; }
    }
}
