using System.Collections.Generic;
using System.IO;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class BlockElementConverterTests {
        [Theory]
        [InlineData(false, 0, "\r\n\t\tstart")]
        [InlineData(true, 0, "start")]
        [InlineData(false, 1, "\t\tstart")]
        [InlineData(true, 1, "start")]
        public void RenderStart_Renders_StartOuput(bool isFirstChild, int trailingNewLineCount, string expectedOutput) {
            using var writer = new StringWriter();

            var converter = new BlockElementConverter("start", "foo", "bar");
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

        [Theory]
        [InlineData(0, "\r\n")]
        [InlineData(1, "")]
        public void RenderEnd_Renders_EndOuput(int trailingNewLineCount, string expectedOutput) {
            using var writer = new StringWriter();

            var converter = new BlockElementConverter("start", "foo", "bar");
            var elementData = ElementDataHelper.Create(
                "bar",
                additionalData: new Dictionary<string, object?> {
                    { nameof(ContentTracker.TrailingNewLineCount), trailingNewLineCount }
                }
            );

            converter.RenderEnd(elementData, writer);

            Assert.Equal(expectedOutput, writer.ToString());
        }
    }
}
