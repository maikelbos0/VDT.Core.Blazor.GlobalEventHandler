using NSubstitute;
using System.Collections.Generic;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class NodeDataExtensionsTests {
        [Fact]
        public void GetContentTracker_Creates_Tracker() {
            var nodeData = Substitute.For<INodeData>();

            var tracker = nodeData.GetContentTracker();

            Assert.NotNull(tracker);
        }

        [Theory]
        [InlineData("bar", true)]
        [InlineData("bAR", true)]
        [InlineData("Bar", true)]
        [InlineData("BAR", true)]
        [InlineData("baz", false)]
        [InlineData("quux", false)]
        public void TryGetAttribute(string name, bool expectedIsFound) {
            var elementData = ElementDataHelper.Create(
                "foo",
                attributes: new Dictionary<string, string>() {
                    { "bar", "baz" }
                }
            );

            var isFound = elementData.TryGetAttribute(name, out var value);

            Assert.Equal(expectedIsFound, isFound);

            if (isFound) {
                Assert.Equal("baz", value);
            }
        }
    }
}
