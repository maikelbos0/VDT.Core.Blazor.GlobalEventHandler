using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for rendering xml text as Markdown text
    /// </summary>
    public class TextConverter : INodeConverter {
        private const string preName = "pre";

        private static Regex whitespaceNormalizer = new Regex("\\s+", RegexOptions.Compiled);
        private static Regex newLineFinder = new Regex("(\r\n?|\n)", RegexOptions.Compiled);

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
            { '&', "&amp;" }
        };

        /// <inheritdoc/>
        public void Convert(XmlReader reader, TextWriter writer, NodeData data) {
            var tracker = data.GetContentTracker();
            var value = reader.Value;
            var valueBuilder = new StringBuilder();

            if (data.Ancestors.Any(e => string.Equals(e.Name, preName, StringComparison.OrdinalIgnoreCase))) {
                var indentation = data.GetIndentation();

                value = newLineFinder.Replace(value, m => $"{Environment.NewLine}{indentation}");

                if (!value.StartsWith(Environment.NewLine)) {
                    value = $"{Environment.NewLine}{indentation}{value}";
                }
            }
            else {
                value = whitespaceNormalizer.Replace(reader.Value, " ");

                if (data.IsFirstChild || tracker.HasTrailingNewLine) {
                    value = value.TrimStart();
                }
            }

            foreach (var c in value) {
                if (markdownEscapeCharacters.TryGetValue(c, out var str)) {
                    valueBuilder.Append(str);
                }
                else {
                    valueBuilder.Append(c);
                }
            }

            tracker.Write(writer, valueBuilder.ToString());
        }
    }
}
