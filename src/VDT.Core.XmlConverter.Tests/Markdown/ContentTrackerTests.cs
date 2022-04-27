using NSubstitute;
using System.Collections.Generic;
using System.IO;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class ContentTrackerTests {
        [Theory]
        [InlineData(null, "", 0, false)]
        [InlineData(0, "", 0, false)]
        [InlineData(2, "", 2, true)]

        [InlineData(null, "\r\ntest\r\n", 1, true)]
        [InlineData(0, "\r\ntest\r\n", 1, true)]
        [InlineData(2, "\r\ntest\r\n", 1, true)]

        [InlineData(null, "\r\ntest", 0, false)]
        [InlineData(0, "\r\ntest", 0, false)]
        [InlineData(2, "\r\ntest", 0, false)]
        public void Write(int? trailingNewLineCount, string value, int expectedTrailingNewLineCount, bool expectedHasTrailingNewLine) {
            using var writer = Substitute.For<TextWriter>();

            var additionalData = GetAdditionalData(trailingNewLineCount);
            var tracker = new ContentTracker(additionalData);

            tracker.Write(writer, value);

            writer.Received().Write(value);
            Assert.Equal(expectedTrailingNewLineCount, tracker.TrailingNewLineCount);
            Assert.Equal(expectedHasTrailingNewLine, tracker.HasTrailingNewLine);
        }

        [Theory]
        [InlineData(null, "", 1)]
        [InlineData(0, "", 1)]
        [InlineData(2, "", 3)]

        [InlineData(null, "\r\ntest\r\n", 2)]
        [InlineData(0, "\r\ntest\r\n", 2)]
        [InlineData(2, "\r\ntest\r\n", 2)]

        [InlineData(null, "\r\ntest", 1)]
        [InlineData(0, "\r\ntest", 1)]
        [InlineData(2, "\r\ntest", 1)]

        [InlineData(null, "\r\n\r\n", 3)]
        [InlineData(0, "\r\n\r\n", 3)]
        [InlineData(2, "\r\n\r\n", 5)]
        public void WriteLine_With_Value(int? trailingNewLineCount, string value, int expectedTrailingNewLineCount) {
            using var writer = Substitute.For<TextWriter>();

            var additionalData = GetAdditionalData(trailingNewLineCount);
            var tracker = new ContentTracker(additionalData);

            tracker.WriteLine(writer, value);

            writer.Received().WriteLine(value);
            Assert.Equal(expectedTrailingNewLineCount, tracker.TrailingNewLineCount);
            Assert.True(tracker.HasTrailingNewLine);
        }

        [Theory]
        [InlineData(null, 1)]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        public void WriteLine_Without_Value(int? trailingNewLineCount, int expectedTrailingNewLineCount) {
            using var writer = Substitute.For<TextWriter>();

            var additionalData = GetAdditionalData(trailingNewLineCount);
            var tracker = new ContentTracker(additionalData);

            tracker.WriteLine(writer);

            writer.Received().WriteLine();
            Assert.Equal(expectedTrailingNewLineCount, tracker.TrailingNewLineCount);
            Assert.True(tracker.HasTrailingNewLine);
        }

        private Dictionary<string, object?> GetAdditionalData(int? trailingNewLineCount) {
            var additionalData = new Dictionary<string, object?>();

            if (trailingNewLineCount.HasValue) {
                additionalData[nameof(ContentTracker.TrailingNewLineCount)] = trailingNewLineCount.Value;
            }

            return additionalData;
        }
    }
}
