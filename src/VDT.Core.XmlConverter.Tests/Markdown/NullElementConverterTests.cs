using System.Collections.Generic;
using System.IO;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class NullElementConverterTests {
        [Theory]
        [InlineData("foo", true)]
        [InlineData("FOO", true)]
        [InlineData("bar", true)]
        [InlineData("baz", false)]
        public void IsValidFor(string elementName, bool expectedIsValid) {
            var converter = new NullElementConverter("foo", "bar");

            Assert.Equal(expectedIsValid, converter.IsValidFor(new ElementData(elementName, new Dictionary<string, string>(), false)));
        }

        [Fact]
        public void RenderStart_Renders_StartOuput() {
            using var writer = new StringWriter();
            var converter = new NullElementConverter("foo", "bar");

            converter.RenderStart(new ElementData("bar", new Dictionary<string, string>(), false), writer);

            Assert.Equal("", writer.ToString());
        }

        [Fact]
        public void ShouldRenderContent_Returns_True() {
            var converter = new NullElementConverter("foo", "bar");

            Assert.True(converter.ShouldRenderContent(new ElementData("bar", new Dictionary<string, string>(), false)));
        }

        [Fact]
        public void RenderEnd_Renders_EndOuput() {
            using var writer = new StringWriter();
            var converter = new NullElementConverter("foo", "bar");

            converter.RenderEnd(new ElementData("bar", new Dictionary<string, string>(), false), writer);

            Assert.Equal("", writer.ToString());
        }
    }
}
