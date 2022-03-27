using System.Collections.Generic;
using System.IO;
using VDT.Core.XmlConverter.Elements;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Elements {
    public class BasicElementConverterTests {
        [Fact]
        public void IsValidFor_Returns_True_When_ElementName_In_ValidForElementNames() {
            var converter = new BasicElementConverter("start", "end", "foo", "bar");

            Assert.True(converter.IsValidFor(new ElementData("bar", new Dictionary<string, string>())));
        }

        [Fact]
        public void IsValidFor_Returns_False_When_ElementName_Not_In_ValidForElementNames() {
            var converter = new BasicElementConverter("start", "end", "foo", "bar");

            Assert.False(converter.IsValidFor(new ElementData("baz", new Dictionary<string, string>())));
        }

        [Fact]
        public void RenderStart_Renders_StartOuput() {
            var converter = new BasicElementConverter("start", "end", "foo", "bar");
            var writer = new StringWriter();

            converter.RenderStart(new ElementData("bar", new Dictionary<string, string>()), writer);

            Assert.Equal("start", writer.ToString());
        }

        [Fact]
        public void ShouldRenderContent_Returns_True() {
            var converter = new BasicElementConverter("start", "end", "foo", "bar");

            Assert.True(converter.ShouldRenderContent(new ElementData("bar", new Dictionary<string, string>())));
        }

        [Fact]
        public void RenderEnd_Renders_EndOuput() {
            var converter = new BasicElementConverter("end", "end", "foo", "bar");
            var writer = new StringWriter();

            converter.RenderEnd(new ElementData("bar", new Dictionary<string, string>()), writer);

            Assert.Equal("end", writer.ToString());
        }
    }
}
