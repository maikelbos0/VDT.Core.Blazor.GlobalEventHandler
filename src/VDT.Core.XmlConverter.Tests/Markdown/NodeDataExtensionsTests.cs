using NSubstitute;
using System.Collections.Generic;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class NodeDataExtensionsTests {
        [Fact]
        public void GetTrailingNewLineTracker_Creates_Tracker_If_Needed() {
            var nodeData = Substitute.For<INodeData>();
            var additionalData = new Dictionary<string, object?>();

            nodeData.AdditionalData.Returns(additionalData);

            var tracker = nodeData.GetTrailingNewLineTracker();

            Assert.NotNull(tracker);
            Assert.True(additionalData.ContainsKey(nameof(TrailingNewLineTracker)));
            Assert.Equal(tracker, additionalData[nameof(TrailingNewLineTracker)]);
        }

        [Fact]
        public void GetTrailingNewLineTracker_Returns_Existing_Tracker() {
            var nodeData = Substitute.For<INodeData>();
            var additionalData = new Dictionary<string, object?>();
            var existingTracker = new TrailingNewLineTracker(additionalData);

            additionalData[nameof(TrailingNewLineTracker)] = existingTracker;
            nodeData.AdditionalData.Returns(additionalData);

            var tracker = nodeData.GetTrailingNewLineTracker();

            Assert.NotNull(tracker);
            Assert.Equal(tracker, additionalData[nameof(TrailingNewLineTracker)]);
            Assert.Equal(tracker, existingTracker);
        }
    }
}
