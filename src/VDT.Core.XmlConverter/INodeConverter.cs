using System.IO;

namespace VDT.Core.XmlConverter {
    /// <summary>
    /// Definition of a converter that can convert an XML node to the desired output
    /// </summary>
    public interface INodeConverter {
        /// <summary>
        /// Renders converted output for the node data
        /// </summary>
        /// <param name="writer">Text writer to write the resulting output to</param>
        /// <param name="data">Data relating to the current node</param>
        public void Convert(TextWriter writer, NodeData data);
    }
}
