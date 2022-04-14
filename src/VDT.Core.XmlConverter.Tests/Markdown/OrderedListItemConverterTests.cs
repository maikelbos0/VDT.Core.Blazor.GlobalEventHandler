using System;
using System.Collections.Generic;
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
        public void IsValidFor(bool expectedIsValidFor, string elementName, params string[] parentElementNames) {
            var converter = new OrderedListItemConverter();
            var elementData = new ElementData(
                elementName,
                new Dictionary<string, string>(),
                false,
                parentElementNames.Select(n => new ElementData(n, new Dictionary<string, string>(), false, Array.Empty<ElementData>())).ToArray()
            );

            Assert.Equal(expectedIsValidFor, converter.IsValidFor(elementData));
        }


        [Fact]
        public void RenderStart_Renders_StartOuput() {
            using var writer = new StringWriter();
            var converter = new OrderedListItemConverter();
            var elementData = new ElementData(
                "li",
                new Dictionary<string, string>(),
                false,
                new[] {
                    new ElementData("ol", new Dictionary<string, string>(), false, Array.Empty<ElementData>()),
                    new ElementData("li", new Dictionary<string, string>(), false, Array.Empty<ElementData>())
                }
            );

            converter.RenderStart(elementData, writer);

            Assert.Equal("\r\n\t1. ", writer.ToString());
        }
    }
}
