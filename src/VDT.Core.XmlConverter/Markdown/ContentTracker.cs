using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Tracks Markdown content being written to a <see cref="TextWriter"/>; used to determine later output
    /// </summary>
    public class ContentTracker {
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
            writer.Write(value);
            TrailingNewLineCount = GetTrailingNewLineCount(value);
        }

        /// <summary>
        /// Writes a string to the text stream, followed by a line terminator
        /// </summary>
        /// <param name="writer">Writer to write the string to</param>
        /// <param name="value">String to write</param>
        public void WriteLine(TextWriter writer, string value) {
            writer.WriteLine(value);
            TrailingNewLineCount = GetTrailingNewLineCount(value) + 1;
        }

        /// <summary>
        /// Writes a line terminator to the text stream
        /// </summary>
        /// <param name="writer">Writer to write the line terminator to</param>
        public void WriteLine(TextWriter writer) {
            writer.WriteLine();
            TrailingNewLineCount++;
        }

        private int GetTrailingNewLineCount(string value) {
            var values = value.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var newLineCount = values.Reverse().TakeWhile(s => string.IsNullOrEmpty(s)).Count();

            if (newLineCount == values.Length) {
                return TrailingNewLineCount + newLineCount - 1;
            }
            else {
                return newLineCount;
            }
        }
    }
}
