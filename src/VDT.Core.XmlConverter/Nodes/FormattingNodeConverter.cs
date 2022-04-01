using System.IO;
using System.Security;
using System.Xml;

namespace VDT.Core.XmlConverter.Nodes {
    public class FormattingNodeConverter : INodeConverter {
        public delegate string NameValueFormatter(string name, string value);

        public NameValueFormatter Formatter { get; set; }
        public bool XmlEncodeValue { get; set; }

        public FormattingNodeConverter(NameValueFormatter formatter, bool xmlEncodeValue) {
            Formatter = formatter;
            XmlEncodeValue = xmlEncodeValue;
        }

        public void Convert(XmlReader reader, TextWriter writer) {
            var name = reader.Name;
            var value = reader.Value;

            if (XmlEncodeValue) {
                value = SecurityElement.Escape(value) ?? string.Empty;
            }

            writer.Write(Formatter(name, value));
        }
    }
}
