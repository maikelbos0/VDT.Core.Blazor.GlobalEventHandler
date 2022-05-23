using System.IO;
using System.Security;

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
        /// <see langword="true"/> if the value should be XML encoded; otherwise <see langword="false"/>
        /// </summary>
        public bool XmlEncodeValue { get; set; }

        /// <summary>
        /// Construct a formatting node converter
        /// </summary>
        /// <param name="formatter">Formatting method to use</param>
        /// <param name="xmlEncodeValue"><see langword="true"/> if the value should be XML encoded; otherwise <see langword="false"/></param>
        public FormattingNodeConverter(NameValueFormatter formatter, bool xmlEncodeValue) {
            Formatter = formatter;
            XmlEncodeValue = xmlEncodeValue;
        }

        /// <inheritdoc/>
        public void Convert(TextWriter writer, NodeData data) {
            var value = data.Value;

            if (XmlEncodeValue) {
                value = SecurityElement.Escape(value) ?? string.Empty;
            }

            writer.Write(Formatter(data.Name, value));
        }
    }
}
