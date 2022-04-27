using System.Linq;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class BaseElementConverterTests {
        [Theory]
        [InlineData("foo", true)]
        [InlineData("FOO", true)]
        [InlineData("bar", true)]
        [InlineData("baz", false)]
        public void IsValidFor(string elementName, bool expectedIsValid) {
            var converter = new BlockElementConverter("start", "foo", "bar");

            Assert.Equal(expectedIsValid, converter.IsValidFor(ElementDataHelper.Create(elementName)));
        }

        [Fact]
        public void ShouldRenderContent_Returns_True() {
            var converter = new BlockElementConverter("start", "foo", "bar");

            Assert.True(converter.ShouldRenderContent(ElementDataHelper.Create("bar")));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0, "ol")]
        [InlineData(0, "div")]
        [InlineData(1, "ol", "li")]
        [InlineData(2, "li", "li")]
        [InlineData(2, "LI", "LI")]
        [InlineData(2, "ol", "li", "ul", "li", "div")]
        public void GetAncestorListItemCount(int expectedCount, params string[] elementNames) {
            var converter = new BlockElementConverter("start", "foo", "bar");
            var elementData = ElementDataHelper.Create(
                "li",
                elementNames.Select(n => ElementDataHelper.Create(n))
            );

            Assert.Equal(expectedCount, converter.GetAncestorListItemCount(elementData));
        }
    }
}
