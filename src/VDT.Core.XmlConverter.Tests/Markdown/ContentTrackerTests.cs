using System.Collections.Generic;
using System.IO;
using System.Linq;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class ContentTrackerTests {
        [Theory]
        [InlineData(null, false, "", 0, false, "")]
        [InlineData(0, true, "", 0, false, "")]
        [InlineData(2, true, "", 2, true, "\t> ")]

        [InlineData(null, false, "\r\ntest\r\n", 1, true, "\r\ntest\r\n")]
        [InlineData(0, true, "\r\ntest\r\n", 1, true, "\r\n\t> test\r\n\t> ")]
        [InlineData(2, true, "\r\ntest\r\n", 1, true, "\t> \r\n\t> test\r\n\t> ")]

        [InlineData(null, false, "\r\ntest", 0, false, "\r\ntest")]
        [InlineData(0, true, "\r\ntest", 0, false, "\r\n\t> test")]
        [InlineData(2, true, "\r\ntest", 0, false, "\t> \r\n\t> test")]
        public void Write(int? trailingNewLineCount, bool hasPrefixes, string value, int expectedTrailingNewLineCount, bool expectedHasTrailingNewLine, string expectedValue) {
            using var writer = new StringWriter();

            var additionalData = GetAdditionalData(trailingNewLineCount, hasPrefixes);
            var tracker = new ContentTracker(additionalData);

            tracker.Write(writer, value);

            Assert.Equal(expectedValue, writer.ToString());
            Assert.Equal(expectedTrailingNewLineCount, tracker.TrailingNewLineCount);
            Assert.Equal(expectedHasTrailingNewLine, tracker.HasTrailingNewLine);
        }

        [Theory]
        [InlineData(null, false, "", 1, "\r\n")]
        [InlineData(0, true, "", 1, "\r\n")]
        [InlineData(2, true, "", 3, "\t> \r\n")]

        [InlineData(null, false, "\r\ntest\r\n", 2, "\r\ntest\r\n\r\n")]
        [InlineData(0, true, "\r\ntest\r\n", 2, "\r\n\t> test\r\n\t> \r\n")]
        [InlineData(2, true, "\r\ntest\r\n", 2, "\t> \r\n\t> test\r\n\t> \r\n")]

        [InlineData(null, false, "\r\ntest", 1, "\r\ntest\r\n")]
        [InlineData(0, true, "\r\ntest", 1, "\r\n\t> test\r\n")]
        [InlineData(2, true, "\r\ntest", 1, "\t> \r\n\t> test\r\n")]

        [InlineData(null, false, "\r\n\r\n", 3, "\r\n\r\n\r\n")]
        [InlineData(0, true, "\r\n\r\n", 3, "\r\n\t> \r\n\t> \r\n")]
        [InlineData(2, true, "\r\n\r\n", 5, "\t> \r\n\t> \r\n\t> \r\n")]
        public void WriteLine_With_Value(int? trailingNewLineCount, bool hasPrefixes, string value, int expectedTrailingNewLineCount, string expectedValue) {
            using var writer = new StringWriter();

            var additionalData = GetAdditionalData(trailingNewLineCount, hasPrefixes);
            var tracker = new ContentTracker(additionalData);

            tracker.WriteLine(writer, value);

            Assert.Equal(expectedValue, writer.ToString());
            Assert.Equal(expectedTrailingNewLineCount, tracker.TrailingNewLineCount);
            Assert.True(tracker.HasTrailingNewLine);
        }

        [Theory]
        [InlineData(null, false, 1, "\r\n")]
        [InlineData(0, true, 1, "\r\n")]
        [InlineData(1, true, 2, "\t> \r\n")]
        public void WriteLine_Without_Value(int? trailingNewLineCount, bool hasPrefixes, int expectedTrailingNewLineCount, string expectedValue) {
            using var writer = new StringWriter();

            var additionalData = GetAdditionalData(trailingNewLineCount, hasPrefixes);
            var tracker = new ContentTracker(additionalData);

            tracker.WriteLine(writer);

            Assert.Equal(expectedValue, writer.ToString());
            Assert.Equal(expectedTrailingNewLineCount, tracker.TrailingNewLineCount);
            Assert.True(tracker.HasTrailingNewLine);
        }

        private Dictionary<string, object?> GetAdditionalData(int? trailingNewLineCount, bool hasPrefixes) {
            var additionalData = new Dictionary<string, object?>();

            if (trailingNewLineCount.HasValue) {
                additionalData[nameof(ContentTracker.TrailingNewLineCount)] = trailingNewLineCount.Value;
            }

            if (hasPrefixes) {
                additionalData[nameof(ContentTracker.Prefixes)] = new Stack<string>(new string[] { "\t", "> " });
            }

            return additionalData;
        }
    }
}
