using NSubstitute;
using System.Collections.Generic;
using System.IO;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class TrailingNewLineTrackerTests {
        [Theory]
        [InlineData(null, "", false)]
        [InlineData(false, "", false)]
        [InlineData(true, "", true)]
        [InlineData(null, "\r\ntest\r\n", true)]
        [InlineData(false, "\r\ntest\r\n", true)]
        [InlineData(true, "\r\ntest\r\n", true)]
        [InlineData(null, "\r\ntest", false)]
        [InlineData(false, "\r\ntest", false)]
        [InlineData(true, "\r\ntest", false)]
        public void NewLineTracker_Write_Succeeds(bool? currentHasTrailingNewLine, string value, bool expectedHasTrailingNewLine) {
            using var writer = Substitute.For<TextWriter>();

            var additionalData = GetAdditionalData(currentHasTrailingNewLine);
            var tracker = new TrailingNewLineTracker(additionalData);

            tracker.Write(writer, value);

            writer.Received().Write(value);
            Assert.True(additionalData.ContainsKey(nameof(TrailingNewLineTracker.HasTrailingNewLine)));
            Assert.Equal(expectedHasTrailingNewLine, additionalData[nameof(TrailingNewLineTracker.HasTrailingNewLine)]);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(false)]
        [InlineData(true)]
        public void NewLineTracker_WriteLine_Value_Succeeds(bool? currentHasTrailingNewLine) {
            using var writer = Substitute.For<TextWriter>();

            var additionalData = GetAdditionalData(currentHasTrailingNewLine);
            var tracker = new TrailingNewLineTracker(additionalData);

            tracker.WriteLine(writer, "foo");

            writer.Received().WriteLine("foo");
            Assert.True(additionalData.ContainsKey(nameof(TrailingNewLineTracker.HasTrailingNewLine)));
            Assert.True(Assert.IsType<bool>(additionalData[nameof(TrailingNewLineTracker.HasTrailingNewLine)]));
        }

        [Theory]
        [InlineData(null)]
        [InlineData(false)]
        [InlineData(true)]
        public void NewLineTracker_WriteLine_Succeeds(bool? currentHasTrailingNewLine) {
            using var writer = Substitute.For<TextWriter>();

            var additionalData = GetAdditionalData(currentHasTrailingNewLine);
            var tracker = new TrailingNewLineTracker(additionalData);

            tracker.WriteLine(writer);

            writer.Received().WriteLine();
            Assert.True(additionalData.ContainsKey(nameof(TrailingNewLineTracker.HasTrailingNewLine)));
            Assert.True(Assert.IsType<bool>(additionalData[nameof(TrailingNewLineTracker.HasTrailingNewLine)]));
        }

        private Dictionary<string, object?> GetAdditionalData(bool? currentHasTrailingNewLine) {
            var additionalData = new Dictionary<string, object?>();

            if (currentHasTrailingNewLine.HasValue) {
                additionalData[nameof(TrailingNewLineTracker.NewLineCount)] = currentHasTrailingNewLine.Value ? 1 : 0;
            }

            return additionalData;
        }
    }
}
