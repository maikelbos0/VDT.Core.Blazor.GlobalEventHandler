using System.Collections.Generic;
using System.IO;
using Xunit;

namespace VDT.Core.XmlConverter.Tests {
    public class BasicXmlNodeConverterTests {
        [Fact]
        public void IsValidFor_Returns_True_When_Tag_In_IsValidForTags() {
            var converter = new BasicXmlNodeConverter("start", "end", "foo", "bar");

            Assert.True(converter.IsValidFor(new XmlNodeData("bar", new Dictionary<string, string>())));
        }

        [Fact]
        public void IsValidFor_Returns_False_When_Tag_Not_In_IsValidForTags() {
            var converter = new BasicXmlNodeConverter("start", "end", "foo", "bar");

            Assert.False(converter.IsValidFor(new XmlNodeData("baz", new Dictionary<string, string>())));
        }

        [Fact]
        public void RenderStart_Renders_StartOuput() {
            var converter = new BasicXmlNodeConverter("start", "end", "foo", "bar");
            var writer = new StringWriter();

            converter.RenderStart(new XmlNodeData("bar", new Dictionary<string, string>()), writer);

            Assert.Equal("start", writer.ToString());
        }

        [Fact]
        public void ShouldRenderContent_Returns_True() {
            var converter = new BasicXmlNodeConverter("start", "end", "foo", "bar");

            Assert.True(converter.ShouldRenderContent(new XmlNodeData("bar", new Dictionary<string, string>())));
        }

        [Fact]
        public void RenderEnd_Renders_EndOuput() {
            var converter = new BasicXmlNodeConverter("end", "end", "foo", "bar");
            var writer = new StringWriter();

            converter.RenderEnd(new XmlNodeData("bar", new Dictionary<string, string>()), writer);

            Assert.Equal("end", writer.ToString());
        }
    }
}
