using System.IO;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class PreConverterTests {
        [Theory]
        [InlineData("pre", true)]
        [InlineData("PRE", true)]
        [InlineData("foo", false)]
        public void IsValidFor(string elementName, bool expectedIsValid) {
            var converter = new PreConverter();

            Assert.Equal(expectedIsValid, converter.IsValidFor(ElementDataHelper.Create(elementName)));
        }

        [Fact]
        public void RenderStart() {
            using var writer = new StringWriter();
            var converter = new PreConverter();

            converter.RenderStart(ElementDataHelper.Create("pre"), writer);

            Assert.Equal("\r\n```\r\n", writer.ToString());
        }

        [Fact]
        public void RenderEnd() {
            using var writer = new StringWriter();

            var converter = new PreConverter();
            var elementData = ElementDataHelper.Create(
                "pre",
                ElementDataHelper.Create("li")
            );

            converter.RenderEnd(elementData, writer);

            Assert.Equal("\r\n\t```\r\n", writer.ToString());
        }
    }
}
