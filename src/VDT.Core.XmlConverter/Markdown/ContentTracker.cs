using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Tracks Markdown content being written to a <see cref="TextWriter"/>; used to determine later output
    /// </summary>
    public class ContentTracker {
        private static Regex newLineFinder = new Regex("\r\n?|\n", RegexOptions.Compiled);

        private readonly Dictionary<string, object?> additionalData;

        /// <summary>
        /// Amount of trailing line terminators
        /// </summary>
        public int TrailingNewLineCount {
            get {
                if (additionalData.TryGetValue(nameof(TrailingNewLineCount), out var countObj) && countObj is int count) {
                    return count;
                }

                return 0;
            }
            private set {
                additionalData[nameof(TrailingNewLineCount)] = value;
            }
        }

        /// <summary>
        /// <see langword="true"/> if the last things written was a trailing line terminator; otherwise <see langword="false"/>
        /// </summary>
        public bool HasTrailingNewLine => TrailingNewLineCount > 0;

        /// <summary>
        /// Number of tabs that will be added to all lines as an indentation prefix
        /// </summary>
        public int IndentationCount {
            get {
                if (additionalData.TryGetValue(nameof(IndentationCount), out var countObj) && countObj is int count) {
                    return count;
                }

                return 0;
            }
            set {
                additionalData[nameof(IndentationCount)] = value;
            }
        }

        /// <summary>
        /// Construct a Markdown content tracker
        /// </summary>
        /// <param name="additionalData">Additional data for the current conversion</param>
        public ContentTracker(Dictionary<string, object?> additionalData) {
            this.additionalData = additionalData;
        }

        /// <summary>
        /// Writes a string to the text stream
        /// </summary>
        /// <param name="writer">Writer to write the string to</param>
        /// <param name="value">String to write</param>
        public void Write(TextWriter writer, string value) {
            WriteInternal(writer, value, false);
        }

        /// <summary>
        /// Writes a string to the text stream, followed by a line terminator
        /// </summary>
        /// <param name="writer">Writer to write the string to</param>
        /// <param name="value">String to write</param>
        public void WriteLine(TextWriter writer, string value) {
            WriteInternal(writer, value, true);
        }

        /// <summary>
        /// Writes a line terminator to the text stream
        /// </summary>
        /// <param name="writer">Writer to write the line terminator to</param>
        public void WriteLine(TextWriter writer) {
            WriteInternal(writer, null, true);
        }

        private void WriteInternal(TextWriter writer, string? value, bool addNewLine) {
            if (value != null) {
                var lines = newLineFinder.Split(value);
                var newLineCount = lines.Reverse().TakeWhile(s => string.IsNullOrEmpty(s)).Count();
                var newLine = $"{Environment.NewLine}{new string('\t', IndentationCount)}";

                if (newLineCount == lines.Length) {
                    TrailingNewLineCount += newLineCount - 1;
                }
                else {
                    TrailingNewLineCount = newLineCount;
                }

                writer.Write(string.Join(newLine, lines));
            }

            if (addNewLine) {
                // TODO figure out: does the new line here need indentation?
                writer.WriteLine();
                TrailingNewLineCount++;
            }
        }
    }
}
