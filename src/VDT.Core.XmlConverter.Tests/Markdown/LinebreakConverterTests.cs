using System.Collections.Generic;
using System.IO;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class LinebreakConverterTests {
        [Theory]
        [InlineData("br", true)]
        [InlineData("BR", true)]
        [InlineData("foo", false)]
        public void IsValidFor(string elementName, bool expectedIsValid) {
            var converter = new LinebreakConverter();

            Assert.Equal(expectedIsValid, converter.IsValidFor(ElementDataHelper.Create(elementName)));
        }

        [Fact]
        public void RenderStart() {
            using var writer = new StringWriter();

            var converter = new LinebreakConverter();

            converter.RenderStart(ElementDataHelper.Create("br"), writer);

            Assert.Equal("  \r\n", writer.ToString());
        }

        [Fact]
        public void RenderEnd() {
            using var writer = new StringWriter();

            var converter = new LinebreakConverter();

            converter.RenderEnd(ElementDataHelper.Create("bar"), writer);

            Assert.Equal("", writer.ToString());
        }
    }
}
