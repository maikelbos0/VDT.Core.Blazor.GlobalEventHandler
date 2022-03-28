using System.Collections.Generic;
using System.IO;
using VDT.Core.XmlConverter.Elements;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Elements {
    public class BasicElementConverterTests {
        [Fact]
        public void IsValidFor_Returns_True_When_ElementName_In_ValidForElementNames() {
            var converter = new BasicElementConverter("start", "end", "foo", "bar");

            Assert.True(converter.IsValidFor(new ElementData("bar", new Dictionary<string, string>(), false)));
        }

        [Fact]
        public void IsValidFor_Returns_False_When_ElementName_Not_In_ValidForElementNames() {
            var converter = new BasicElementConverter("start", "end", "foo", "bar");

            Assert.False(converter.IsValidFor(new ElementData("baz", new Dictionary<string, string>(), false)));
        }

        [Fact]
        public void RenderStart_Renders_StartOuput() {
            using var writer = new StringWriter();
            var converter = new BasicElementConverter("start", "end", "foo", "bar");

            converter.RenderStart(new ElementData("bar", new Dictionary<string, string>(), false), writer);

            Assert.Equal("start", writer.ToString());
        }

        [Fact]
        public void ShouldRenderContent_Returns_True() {
            var converter = new BasicElementConverter("start", "end", "foo", "bar");

            Assert.True(converter.ShouldRenderContent(new ElementData("bar", new Dictionary<string, string>(), false)));
        }

        [Fact]
        public void RenderEnd_Renders_EndOuput() {
            using var writer = new StringWriter();
            var converter = new BasicElementConverter("end", "end", "foo", "bar");

            converter.RenderEnd(new ElementData("bar", new Dictionary<string, string>(), false), writer);

            Assert.Equal("end", writer.ToString());
        }
    }
}
