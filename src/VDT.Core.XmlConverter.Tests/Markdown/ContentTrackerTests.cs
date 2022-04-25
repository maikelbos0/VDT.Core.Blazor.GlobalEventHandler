using NSubstitute;
using System.Collections.Generic;
using System.IO;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class ContentTrackerTests {
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
        public void Write(bool? currentHasTrailingNewLine, string value, bool expectedHasTrailingNewLine) {
            using var writer = Substitute.For<TextWriter>();

            var additionalData = GetAdditionalData(currentHasTrailingNewLine);
            var tracker = new ContentTracker(additionalData);

            tracker.Write(writer, value);

            writer.Received().Write(value);
            Assert.True(additionalData.ContainsKey(nameof(ContentTracker.HasTrailingNewLine)));
            Assert.Equal(expectedHasTrailingNewLine, additionalData[nameof(ContentTracker.HasTrailingNewLine)]);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(false)]
        [InlineData(true)]
        public void WriteLine_With_Value(bool? currentHasTrailingNewLine) {
            using var writer = Substitute.For<TextWriter>();

            var additionalData = GetAdditionalData(currentHasTrailingNewLine);
            var tracker = new ContentTracker(additionalData);

            tracker.WriteLine(writer, "foo");

            writer.Received().WriteLine("foo");
            Assert.True(additionalData.ContainsKey(nameof(ContentTracker.HasTrailingNewLine)));
            Assert.True(Assert.IsType<bool>(additionalData[nameof(ContentTracker.HasTrailingNewLine)]));
        }

        [Theory]
        [InlineData(null)]
        [InlineData(false)]
        [InlineData(true)]
        public void WriteLine_Without_Value(bool? currentHasTrailingNewLine) {
            using var writer = Substitute.For<TextWriter>();

            var additionalData = GetAdditionalData(currentHasTrailingNewLine);
            var tracker = new ContentTracker(additionalData);

            tracker.WriteLine(writer);

            writer.Received().WriteLine();
            Assert.True(additionalData.ContainsKey(nameof(ContentTracker.HasTrailingNewLine)));
            Assert.True(Assert.IsType<bool>(additionalData[nameof(ContentTracker.HasTrailingNewLine)]));
        }

        private Dictionary<string, object?> GetAdditionalData(bool? currentHasTrailingNewLine) {
            var additionalData = new Dictionary<string, object?>();

            if (currentHasTrailingNewLine.HasValue) {
                additionalData[nameof(ContentTracker.HasTrailingNewLine)] = currentHasTrailingNewLine.Value;
            }

            return additionalData;
        }
    }
}
