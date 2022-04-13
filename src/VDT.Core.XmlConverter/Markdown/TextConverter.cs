using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for rendering xml text as Markdown text
    /// </summary>
    public class TextConverter : INodeConverter {
        private static Dictionary<char, string> markdownEscapeCharacters = new Dictionary<char, string>() {
            { '\\', "\\\\" },
            { '`', "\\`" },
            { '*', "\\*" },
            { '_', "\\_" },
            { '{', "\\{" },
            { '}', "\\}" },
            { '[', "\\[" },
            { ']', "\\]" },
            { '(', "\\(" },
            { ')', "\\)" },
            { '#', "\\#" },
            { '+', "\\+" },
            { '-', "\\-" },
            { '.', "\\." },
            { '!', "\\!" },
            { '|', "\\|" },
            { '<', "&lt;" },
            { '>', "&gt;" },
            { '&', "&amp;" },
            { '\r', "" },
            { '\n', "" }
        };

        /// <inheritdoc/>
        public void Convert(XmlReader reader, TextWriter writer, ConversionData data) {
            var value = reader.Value.Trim();
            
            foreach (var c in value) {
                if (markdownEscapeCharacters.TryGetValue(c, out var str)) {
                    writer.Write(str);
                }
                else {
                    writer.Write(c);
                }
            }
        }
    }
}
