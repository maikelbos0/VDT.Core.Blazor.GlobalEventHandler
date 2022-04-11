using System.Collections.Generic;
using System.IO;
using System.Linq;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class BlockElementConverterTests {
        [Theory]
        [InlineData("foo", true)]
        [InlineData("FOO", true)]
        [InlineData("bar", true)]
        [InlineData("baz", false)]
        public void IsValidFor(string elementName, bool expectedIsValid) {
            var converter = new BlockElementConverter("start", "foo", "bar");

            Assert.Equal(expectedIsValid, converter.IsValidFor(new ElementData(elementName, new Dictionary<string, string>(), false)));
        }

        [Fact]
        public void RenderStart_Renders_StartOuput() {
            using var writer = new StringWriter();
            var converter = new BlockElementConverter("start", "foo", "bar");
            var elementData = new ElementData(
                "bar",
                new Dictionary<string, string>(),
                false,
                new ElementData("li", new Dictionary<string, string>(), false),
                new ElementData("li", new Dictionary<string, string>(), false)
            );

            converter.RenderStart(elementData, writer);

            Assert.Equal("\r\n\t\tstart", writer.ToString());
        }

        [Fact]
        public void ShouldRenderContent_Returns_True() {
            var converter = new BlockElementConverter("start", "foo", "bar");

            Assert.True(converter.ShouldRenderContent(new ElementData("bar", new Dictionary<string, string>(), false)));
        }

        [Fact]
        public void RenderEnd_Renders_EndOuput() {
            using var writer = new StringWriter();
            var converter = new BlockElementConverter("start", "foo", "bar");

            converter.RenderEnd(new ElementData("bar", new Dictionary<string, string>(), false), writer);

            Assert.Equal("\r\n", writer.ToString());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0, "ol")]
        [InlineData(0, "div")]
        [InlineData(1, "ol", "li")]
        [InlineData(2, "li", "li")]
        [InlineData(2, "LI", "LI")]
        [InlineData(2, "ol", "li", "ul", "li", "div")]
        public void GetAncestorListItemCount(int expectedCount, params string[] elementNames) {
            var converter = new BlockElementConverter("start", "foo", "bar");
            var elementData = new ElementData(
                "li",
                new Dictionary<string, string>(),
                false,
                elementNames.Select(n => new ElementData(n, new Dictionary<string, string>(), false)).ToArray()
            );

            Assert.Equal(expectedCount, converter.GetAncestorListItemCount(elementData));
        }
    }
}
