using System.IO;
using System.Linq;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class OrderedListItemConverterTests {
        [Theory]
        [InlineData(false, "div")]
        [InlineData(false, "div", "ol")]
        [InlineData(false, "li")]
        [InlineData(false, "li", "ul")]
        [InlineData(true, "li", "ol")]
        [InlineData(false, "li", "ul", "ol")]
        [InlineData(true, "li", "ol", "ul")]
        public void IsValidFor(bool expectedIsValidFor, string elementName, params string[] ancestorElementNames) {
            var converter = new OrderedListItemConverter();
            var elementData = ElementDataHelper.Create(
                elementName,
                ancestorElementNames.Select(n => ElementDataHelper.Create(n))
            );

            Assert.Equal(expectedIsValidFor, converter.IsValidFor(elementData));
        }


        [Fact]
        public void RenderStart() {
            using var writer = new StringWriter();
            var converter = new OrderedListItemConverter();
            var elementData = ElementDataHelper.Create(
                "li",
                ElementDataHelper.Create("ol"),
                ElementDataHelper.Create("li")
            );

            converter.RenderStart(elementData, writer);

            Assert.Equal("\r\n\t1. ", writer.ToString());
        }
    }
}
