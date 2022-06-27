using System.IO;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for removing nodes that don't exist in Markdown
    /// </summary>
    public class NodeRemovingConverter : INodeConverter {
        /// <inheritdoc/>
        public void Convert(TextWriter writer, NodeData data) { }
    }
}
