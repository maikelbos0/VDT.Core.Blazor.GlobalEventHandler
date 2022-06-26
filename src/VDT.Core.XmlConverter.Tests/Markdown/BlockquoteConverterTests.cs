using System.Collections.Generic;
using System.IO;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class BlockquoteConverterTests {
        [Theory]
        [InlineData("blockquote", true)]
        [InlineData("BLOCKQUOTE", true)]
        [InlineData("foo", false)]
        public void IsValidFor(string elementName, bool expectedIsValid) {
            var converter = new BlockquoteConverter();

            Assert.Equal(expectedIsValid, converter.IsValidFor(ElementDataHelper.Create(elementName)));
        }

        [Fact]
        public void RenderStart() {
            using var writer = new StringWriter();

            var converter = new BlockquoteConverter();
            var elementData = ElementDataHelper.Create("blockquote");

            converter.RenderStart(elementData, writer);

            Assert.Equal("\r\n", writer.ToString());
            Assert.Equal("> ", Assert.Single(Assert.IsType<Stack<string>>(elementData.AdditionalData[nameof(ContentTracker.Prefixes)])));
        }

        [Fact]
        public void RenderEnd() {
            using var writer = new StringWriter();

            var converter = new BlockquoteConverter();
            var prefixes = new Stack<string>();
            var elementData = ElementDataHelper.Create(
                "blockquote",
                additionalData: new Dictionary<string, object?>() {
                    { nameof(ContentTracker.Prefixes), prefixes },
                    { nameof(ContentTracker.TrailingNewLineCount), 1 }
                }
            );
            
            prefixes.Push("\t");
            prefixes.Push("> ");

            converter.RenderEnd(elementData, writer);

            Assert.Equal("\t\r\n", writer.ToString());
            Assert.Equal("\t", Assert.Single(Assert.IsType<Stack<string>>(elementData.AdditionalData[nameof(ContentTracker.Prefixes)])));
        }
    }
}
