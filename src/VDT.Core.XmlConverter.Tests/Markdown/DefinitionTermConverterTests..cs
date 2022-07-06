using System.Collections.Generic;
using System.IO;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class DefinitionTermConverterTests {
        [Theory]
        [InlineData("dt", true)]
        [InlineData("DT", true)]
        [InlineData("foo", false)]
        public void IsValidFor(string elementName, bool expectedIsValid) {
            var converter = new DefinitionTermConverter();

            Assert.Equal(expectedIsValid, converter.IsValidFor(ElementDataHelper.Create(elementName)));
        }

        [Theory]
        [InlineData(false, 0, "\r\n\r\n")]
        [InlineData(true, 0, "")]
        [InlineData(false, 1, "\r\n")]
        [InlineData(true, 1, "")]
        [InlineData(false, 2, "")]
        [InlineData(true, 2, "")]
        public void RenderStart(bool isFirstChild, int trailingNewLineCount, string expectedOutput) {
            using var writer = new StringWriter();

            var converter = new DefinitionTermConverter();
            var elementData = ElementDataHelper.Create(
                "dt",
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
        public void RenderEnd(int trailingNewLineCount, string expectedOutput) {
            using var writer = new StringWriter();

            var converter = new DefinitionTermConverter();
            var elementData = ElementDataHelper.Create(
                "dt",
                additionalData: new Dictionary<string, object?> {
                    { nameof(ContentTracker.TrailingNewLineCount), trailingNewLineCount }
                }
            );

            converter.RenderEnd(elementData, writer);

            Assert.Equal(expectedOutput, writer.ToString());
        }
    }
}
