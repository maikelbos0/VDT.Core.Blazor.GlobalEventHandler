using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Tracks trailing line terminators when writing Markdown content to a <see cref="TextWriter"/>; used to determine if additional line terminators are needed in later content
    /// </summary>
    public class TrailingNewLineTracker { // TODO consider renaming; maybe additional things will need tracking
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
        /// Construct a trailing new line tracker
        /// </summary>
        /// <param name="additionalData">Additional data for the current conversion</param>
        public TrailingNewLineTracker(Dictionary<string, object?> additionalData) {
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
