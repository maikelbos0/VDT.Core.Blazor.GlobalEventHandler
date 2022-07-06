using System.Collections.Generic;
using System.IO;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class ListConverterTests {
        [Fact]
        public void RenderStart() {
            using var writer = new StringWriter();

            var converter = new ListConverter();

            converter.RenderStart(ElementDataHelper.Create("dl"), writer);

            Assert.Equal("", writer.ToString());
        }

        [Fact]
        public void ShouldRenderContent_Returns_True() {
            var converter = new ListConverter();

            Assert.True(converter.ShouldRenderContent(ElementDataHelper.Create("dl")));
        }

        [Theory]
        [InlineData(0, "\r\n\r\n")]
        [InlineData(1, "\r\n")]
        [InlineData(2, "")]
        public void RenderEnd(int trailingNewLineCount, string expectedOutput) {
            using var writer = new StringWriter();

            var converter = new ListConverter();
            var elementData = ElementDataHelper.Create(
                "dl",
                additionalData: new Dictionary<string, object?> {
                    { nameof(ContentTracker.TrailingNewLineCount), trailingNewLineCount }
                }
            );

            converter.RenderEnd(elementData, writer);

            Assert.Equal(expectedOutput, writer.ToString());
        }
    }
}
