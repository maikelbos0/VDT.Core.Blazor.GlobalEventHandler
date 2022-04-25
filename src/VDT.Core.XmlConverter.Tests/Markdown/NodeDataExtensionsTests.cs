using NSubstitute;
using System.Collections.Generic;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class NodeDataExtensionsTests {
        [Fact]
        public void GetContentTracker_Creates_Tracker_If_Needed() {
            var nodeData = Substitute.For<INodeData>();
            var additionalData = new Dictionary<string, object?>();

            nodeData.AdditionalData.Returns(additionalData);

            var tracker = nodeData.GetContentTracker();

            Assert.NotNull(tracker);
            Assert.True(additionalData.ContainsKey(nameof(ContentTracker)));
            Assert.Equal(tracker, additionalData[nameof(ContentTracker)]);
        }

        [Fact]
        public void GetContentTracker_Returns_Existing_Tracker() {
            var nodeData = Substitute.For<INodeData>();
            var additionalData = new Dictionary<string, object?>();
            var existingTracker = new ContentTracker(additionalData);

            additionalData[nameof(ContentTracker)] = existingTracker;
            nodeData.AdditionalData.Returns(additionalData);

            var tracker = nodeData.GetContentTracker();

            Assert.NotNull(tracker);
            Assert.Equal(tracker, additionalData[nameof(ContentTracker)]);
            Assert.Equal(tracker, existingTracker);
        }
    }
}
