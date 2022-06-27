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
    }
}
