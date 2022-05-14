using System.IO;
using System.Xml;

namespace VDT.Core.XmlConverter {
    /// <summary>
    /// Definition of a converter that can convert an XML node to the desired output
    /// </summary>
    public interface INodeConverter {
        /// <summary>
        /// Converts the current node of the <see cref="XmlReader"/> and writes the results to the <see cref="TextWriter"/>
        /// </summary>
        /// <param name="reader">XML reader for which to convert the current node</param>
        /// <param name="writer">Text writer to write the resulting output to</param>
        /// <param name="data">Data relating to the current node</param>
        public void Convert(XmlReader reader, TextWriter writer, NodeData data);
    }
}
