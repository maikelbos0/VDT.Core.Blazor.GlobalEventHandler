using System.IO;
using System.Security;
using System.Xml;

namespace VDT.Core.XmlConverter.Nodes {
    public class NodeValueConverter : INodeConverter {
        public bool XmlEncode { get; }

        public NodeValueConverter(bool xmlEncode) {
            XmlEncode = xmlEncode;
        }

        public void Convert(XmlReader reader, TextWriter writer) {
            if (XmlEncode) {
                writer.Write(SecurityElement.Escape(reader.Value));
            }
            else {
                writer.Write(reader.Value);
            }
        }
    }
}
