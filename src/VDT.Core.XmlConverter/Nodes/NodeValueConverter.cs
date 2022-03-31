using System.IO;
using System.Security;
using System.Xml;

namespace VDT.Core.XmlConverter.Nodes {
    public class NodeValueConverter : INodeConverter {
        public bool XmlEncodeValue { get; }
        public string? StartOuput { get; }
        public string? EndOutput { get; }

        public NodeValueConverter(bool xmlEncodeValue) : this(xmlEncodeValue, null, null) {
        }

        public NodeValueConverter(bool xmlEncodeValue, string? startOuput, string? endOutput) {
            StartOuput = startOuput;
            EndOutput = endOutput;
            XmlEncodeValue = xmlEncodeValue;
        }

        public void Convert(XmlReader reader, TextWriter writer) {
            writer.Write(StartOuput);

            if (XmlEncodeValue) {
                writer.Write(SecurityElement.Escape(reader.Value));
            }
            else {
                writer.Write(reader.Value);
            }

            writer.Write(EndOutput);
        }
    }
}
