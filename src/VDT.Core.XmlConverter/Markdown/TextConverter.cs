using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for rendering XML text as Markdown text
    /// </summary>
    public class TextConverter : INodeConverter {
        private const string preName = "pre";

        private static Regex newLineFinder = new Regex("^(\r\n?|\n)", RegexOptions.Compiled);
        private static Regex whitespaceNormalizer = new Regex("\\s+", RegexOptions.Compiled);
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
        public void Convert(TextWriter writer, NodeData data) {
            if (data.Ancestors.Any(e => string.Equals(e.Name, preName, StringComparison.OrdinalIgnoreCase))) {
                ConvertPreText(writer, data);
            }
            else {
                ConvertText(writer, data);
            }
        }

        private void ConvertPreText(TextWriter writer, NodeData data) {
            var tracker = data.GetContentTracker();
            var value = data.Value;

            if (!newLineFinder.IsMatch(value)) {
                tracker.WriteLine(writer);  
            }

            tracker.Write(writer, value);
        }

        private void ConvertText(TextWriter writer, NodeData data) {
            var tracker = data.GetContentTracker();
            var value = whitespaceNormalizer.Replace(data.Value, " ");
            var valueBuilder = new StringBuilder();

            if (data.IsFirstChild || tracker.HasTrailingNewLine) {
                value = value.TrimStart();
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
