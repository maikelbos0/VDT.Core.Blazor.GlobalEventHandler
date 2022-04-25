using System;
using System.Collections.Generic;
using System.IO;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Tracks Markdown content being written to a <see cref="TextWriter"/>; used to determine later output
    /// </summary>
    public class ContentTracker {
        private readonly Dictionary<string, object?> additionalData;

        /// <summary>
        /// <see langword="true"/> if the last things written was a trailing line terminator; otherwise <see langword="false"/>
        /// </summary>
        public bool HasTrailingNewLine {
            get {
                if (additionalData.TryGetValue(nameof(HasTrailingNewLine), out var valueObj) && valueObj is bool value) {
                    return value;
                }

                return false;
            }
            private set {
                additionalData[nameof(HasTrailingNewLine)] = value;
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
            writer.Write(value);
            HasTrailingNewLine = value.EndsWith(Environment.NewLine) || (HasTrailingNewLine && value == string.Empty);
        }

        /// <summary>
        /// Writes a string to the text stream, followed by a line terminator
        /// </summary>
        /// <param name="writer">Writer to write the string to</param>
        /// <param name="value">String to write</param>
        public void WriteLine(TextWriter writer, string value) {
            writer.WriteLine(value);
            HasTrailingNewLine = true;
        }

        /// <summary>
        /// Writes a line terminator to the text stream
        /// </summary>
        /// <param name="writer">Writer to write the line terminator to</param>
        public void WriteLine(TextWriter writer) {
            writer.WriteLine();
            HasTrailingNewLine = true;
        }
    }
}
