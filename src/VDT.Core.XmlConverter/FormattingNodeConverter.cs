using System.IO;
using System.Security;
using System.Xml;

namespace VDT.Core.XmlConverter {
    /// <summary>
    /// Converter that converts nodes using a formatting method that accepts the node name and value
    /// </summary>
    public class FormattingNodeConverter : INodeConverter {
        /// <summary>
        /// Formatting method for converting nodes
        /// </summary>
        /// <param name="name">Name of the current node</param>
        /// <param name="value">Text value of the current node</param>
        /// <returns>Formatted value to use as converted value</returns>
        public delegate string NameValueFormatter(string name, string value);

        /// <summary>
        /// Formatting method to use
        /// </summary>
        public NameValueFormatter Formatter { get; set; }

        /// <summary>
        /// <see langword="true"/> of the value should be xml encoded, or <see langword="false"/> if not
        /// </summary>
        public bool XmlEncodeValue { get; set; }

        /// <summary>
        /// Construct a formatting node converter
        /// </summary>
        /// <param name="formatter">Formatting method to use</param>
        /// <param name="xmlEncodeValue"><see langword="true"/> of the value should be xml encoded, or <see langword="false"/> if not</param>
        public FormattingNodeConverter(NameValueFormatter formatter, bool xmlEncodeValue) {
            Formatter = formatter;
            XmlEncodeValue = xmlEncodeValue;
        }

        /// <inheritdoc/>
        public void Convert(XmlReader reader, TextWriter writer, ConversionData data) {
            var name = reader.Name;
            var value = reader.Value;

            if (XmlEncodeValue) {
                value = SecurityElement.Escape(value) ?? string.Empty;
            }

            writer.Write(Formatter(name, value));
        }
    }
}
