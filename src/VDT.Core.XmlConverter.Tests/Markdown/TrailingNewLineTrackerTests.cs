using NSubstitute;
using System.Collections.Generic;
using System.IO;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class TrailingNewLineTrackerTests {
        [Theory]
        [InlineData(null, "", 0)]
        [InlineData(0, "", 0)]
        [InlineData(1, "", 1)]
        [InlineData(null, "\r\ntest\r\n", 1)]
        [InlineData(2, "\r\ntest\r\n", 1)]
        [InlineData(null, "\r\ntest", 0)]
        [InlineData(2, "\r\ntest", 0)]
        [InlineData(null, "\r\n\r\n", 2)]
        [InlineData(2, "\r\n\r\n", 4)]
        public void NewLineTracker_Write_Succeeds(int? currentNewLineCount, string value, int expectedNewLineCount) {
            using var writer = Substitute.For<TextWriter>();

            var additionalData = GetAdditionalData(currentNewLineCount);
            var tracker = new TrailingNewLineTracker(additionalData);

            tracker.Write(writer, value);

            writer.Received().Write(value);
            Assert.True(additionalData.ContainsKey(nameof(TrailingNewLineTracker.NewLineCount)));
            Assert.Equal(expectedNewLineCount, additionalData[nameof(TrailingNewLineTracker.NewLineCount)]);
        }

        [Theory]
        [InlineData(null, "", 1)]
        [InlineData(0, "", 1)]
        [InlineData(1, "", 2)]
        [InlineData(null, "\r\ntest\r\n", 2)]
        [InlineData(2, "\r\ntest\r\n", 2)]
        [InlineData(null, "\r\ntest", 1)]
        [InlineData(2, "\r\ntest", 1)]
        [InlineData(null, "\r\n\r\n", 3)]
        [InlineData(2, "\r\n\r\n", 5)]
        public void NewLineTracker_WriteLine_Value_Succeeds(int? currentNewLineCount, string value, int expectedNewLineCount) {
            using var writer = Substitute.For<TextWriter>();

            var additionalData = GetAdditionalData(currentNewLineCount);
            var tracker = new TrailingNewLineTracker(additionalData);

            tracker.WriteLine(writer, value);

            writer.Received().WriteLine(value);
            Assert.True(additionalData.ContainsKey(nameof(TrailingNewLineTracker.NewLineCount)));
            Assert.Equal(expectedNewLineCount, additionalData[nameof(TrailingNewLineTracker.NewLineCount)]);
        }

        [Theory]
        [InlineData(null, 1)]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        public void NewLineTracker_WriteLine_Succeeds(int? currentNewLineCount, int expectedNewLineCount) {
            using var writer = Substitute.For<TextWriter>();

            var additionalData = GetAdditionalData(currentNewLineCount);
            var tracker = new TrailingNewLineTracker(additionalData);

            tracker.WriteLine(writer);

            writer.Received().WriteLine();
            Assert.True(additionalData.ContainsKey(nameof(TrailingNewLineTracker.NewLineCount)));
            Assert.Equal(expectedNewLineCount, additionalData[nameof(TrailingNewLineTracker.NewLineCount)]);
        }

        private Dictionary<string, object?> GetAdditionalData(int? currentNewLineCount) {
            var additionalData = new Dictionary<string, object?>();

            if (currentNewLineCount.HasValue) {
                additionalData[nameof(TrailingNewLineTracker.NewLineCount)] = currentNewLineCount.Value;
            }

            return additionalData;
        }
    }
}
