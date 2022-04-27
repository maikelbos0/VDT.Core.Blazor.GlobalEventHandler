using System.Collections.Generic;
using System.IO;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class ParagraphConverterTests {
        [Theory]
        [InlineData("p", true)]
        [InlineData("P", true)]
        [InlineData("baz", false)]
        public void IsValidFor(string elementName, bool expectedIsValid) {
            var converter = new ParagraphConverter();

            Assert.Equal(expectedIsValid, converter.IsValidFor(ElementDataHelper.Create(elementName)));
        }

        [Theory]
        [InlineData(false, 0, "\r\n\r\n\t\t")]
        [InlineData(true, 0, "\r\n\r\n")]
        [InlineData(false, 1, "\r\n\t\t")]
        [InlineData(true, 1, "\r\n")]
        [InlineData(false, 2, "\t\t")]
        [InlineData(true, 2, "")]
        public void RenderStart_Renders_StartOuput(bool isFirstChild, int trailingNewLineCount, string expectedOutput) {
            using var writer = new StringWriter();

            var converter = new ParagraphConverter();
            var elementData = ElementDataHelper.Create(
                "bar",
                ancestors: new[] {
                    ElementDataHelper.Create("li"),
                    ElementDataHelper.Create("li")
                },
                isFirstChild: isFirstChild,
                additionalData: new Dictionary<string, object?> {
                    { nameof(ContentTracker.TrailingNewLineCount), trailingNewLineCount }
                }
            );

            converter.RenderStart(elementData, writer);

            Assert.Equal(expectedOutput, writer.ToString());
        }

        [Fact]
        public void RenderEnd_Renders_EndOuput() {
            using var writer = new StringWriter();

            var converter = new ParagraphConverter();

            converter.RenderEnd(ElementDataHelper.Create("p"), writer);

            Assert.Equal("\r\n\r\n", writer.ToString());
        }
    }
}
