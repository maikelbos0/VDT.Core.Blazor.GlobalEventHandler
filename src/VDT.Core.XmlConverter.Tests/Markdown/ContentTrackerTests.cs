using System.Collections.Generic;
using System.IO;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class ContentTrackerTests {
        [Theory]
        [InlineData(null, null, "", 0, false, "")]
        [InlineData(0, 1, "", 0, false, "")]
        [InlineData(2, 1, "", 2, true, "")]

        [InlineData(null, null, "\r\ntest\r\n", 1, true, "\r\ntest\r\n")]
        [InlineData(0, 1, "\r\ntest\r\n", 1, true, "\r\n\ttest\r\n\t")]
        [InlineData(2, 1, "\r\ntest\r\n", 1, true, "\r\n\ttest\r\n\t")]

        [InlineData(null, null, "\r\ntest", 0, false, "\r\ntest")]
        [InlineData(0, 1, "\r\ntest", 0, false, "\r\n\ttest")]
        [InlineData(2, 1, "\r\ntest", 0, false, "\r\n\ttest")]
        public void Write(int? trailingNewLineCount, int? indentationCount, string value, int expectedTrailingNewLineCount, bool expectedHasTrailingNewLine, string expectedValue) {
            using var writer = new StringWriter();

            var additionalData = GetAdditionalData(trailingNewLineCount, indentationCount);
            var tracker = new ContentTracker(additionalData);

            tracker.Write(writer, value);

            Assert.Equal(expectedValue, writer.ToString());
            Assert.Equal(expectedTrailingNewLineCount, tracker.TrailingNewLineCount);
            Assert.Equal(expectedHasTrailingNewLine, tracker.HasTrailingNewLine);
        }

        [Theory]
        [InlineData(null, null, "", 1, "\r\n")]
        [InlineData(0, 1, "", 1, "\r\n")]
        [InlineData(2, 1, "", 3, "\r\n")]

        [InlineData(null, null, "\r\ntest\r\n", 2, "\r\ntest\r\n\r\n")]
        [InlineData(0, 1, "\r\ntest\r\n", 2, "\r\n\ttest\r\n\t\r\n")]
        [InlineData(2, 1, "\r\ntest\r\n", 2, "\r\n\ttest\r\n\t\r\n")]

        [InlineData(null, null, "\r\ntest", 1, "\r\ntest\r\n")]
        [InlineData(0, 1, "\r\ntest", 1, "\r\n\ttest\r\n")]
        [InlineData(2, 1, "\r\ntest", 1, "\r\n\ttest\r\n")]

        [InlineData(null, null, "\r\n\r\n", 3, "\r\n\r\n\r\n")]
        [InlineData(0, 1, "\r\n\r\n", 3, "\r\n\t\r\n\t\r\n")]
        [InlineData(2, 1, "\r\n\r\n", 5, "\r\n\t\r\n\t\r\n")]
        public void WriteLine_With_Value(int? trailingNewLineCount, int? indentationCount, string value, int expectedTrailingNewLineCount, string expectedValue) {
            using var writer = new StringWriter();

            var additionalData = GetAdditionalData(trailingNewLineCount, indentationCount);
            var tracker = new ContentTracker(additionalData);

            tracker.WriteLine(writer, value);

            Assert.Equal(expectedValue, writer.ToString());
            Assert.Equal(expectedTrailingNewLineCount, tracker.TrailingNewLineCount);
            Assert.True(tracker.HasTrailingNewLine);
        }

        [Theory]
        [InlineData(null, null, 1, "\r\n")]
        [InlineData(0, 1, 1, "\r\n")]
        [InlineData(1, 1, 2, "\r\n")]
        public void WriteLine_Without_Value(int? trailingNewLineCount, int? indentationCount, int expectedTrailingNewLineCount, string expectedValue) {
            using var writer = new StringWriter();

            var additionalData = GetAdditionalData(trailingNewLineCount, indentationCount);
            var tracker = new ContentTracker(additionalData);

            tracker.WriteLine(writer);

            Assert.Equal(expectedValue, writer.ToString());
            Assert.Equal(expectedTrailingNewLineCount, tracker.TrailingNewLineCount);
            Assert.True(tracker.HasTrailingNewLine);
        }

        private Dictionary<string, object?> GetAdditionalData(int? trailingNewLineCount, int? indentationCount) {
            var additionalData = new Dictionary<string, object?>();

            if (trailingNewLineCount.HasValue) {
                additionalData[nameof(ContentTracker.TrailingNewLineCount)] = trailingNewLineCount.Value;
            }

            if (indentationCount.HasValue) {
                additionalData[nameof(ContentTracker.IndentationCount)] = indentationCount.Value;
            }

            return additionalData;
        }
    }
}
