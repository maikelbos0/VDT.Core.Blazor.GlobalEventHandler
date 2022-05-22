using System.IO;
using System.Linq;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class PreContentConverterTests {
        [Theory]
        [InlineData(true, "pre")]
        [InlineData(true, "foo", "pre", "bar")]
        [InlineData(false)]
        [InlineData(false, "foo", "bar")]
        public void IsValidFor(bool expectedIsValidFor, params string[] ancestorElementNames) {
            var converter = new PreContentConverter();
            var elementData = ElementDataHelper.Create(
                "foo",
                ancestorElementNames.Select(n => ElementDataHelper.Create(n))
            );

            Assert.Equal(expectedIsValidFor, converter.IsValidFor(elementData));
        }

        [Fact]
        public void RenderStart() {
            using var writer = new StringWriter();

            var converter = new PreContentConverter();

            converter.RenderStart(ElementDataHelper.Create("bar"), writer);

            Assert.Equal("", writer.ToString());
        }

        [Fact]
        public void ShouldRenderContent_Returns_True() {
            var converter = new PreContentConverter();

            Assert.True(converter.ShouldRenderContent(ElementDataHelper.Create("bar")));
        }

        [Fact]
        public void RenderEnd() {
            using var writer = new StringWriter();

            var converter = new PreContentConverter();

            converter.RenderEnd(ElementDataHelper.Create("bar"), writer);

            Assert.Equal("", writer.ToString());
        }
    }
}
