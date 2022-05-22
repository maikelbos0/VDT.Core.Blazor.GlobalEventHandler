using System.IO;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class UnknownElementConverterTests {
        [Fact]
        public void IsValidFor() {
            var converter = new UnknownElementConverter(true);

            Assert.True(converter.IsValidFor(ElementDataHelper.Create("bar")));
        }

        [Fact]
        public void RenderStart() {
            using var writer = new StringWriter();

            var converter = new UnknownElementConverter(true);

            converter.RenderStart(ElementDataHelper.Create("bar"), writer);

            Assert.Equal("", writer.ToString());
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void ShouldRenderContent(bool shouldRenderContent, bool expectedShouldRenderContent) {
            var converter = new UnknownElementConverter(shouldRenderContent);

            Assert.Equal(expectedShouldRenderContent, converter.ShouldRenderContent(ElementDataHelper.Create("bar")));
        }

        [Fact]
        public void RenderEnd() {
            using var writer = new StringWriter();

            var converter = new UnknownElementConverter(true);

            converter.RenderEnd(ElementDataHelper.Create("bar"), writer);

            Assert.Equal("", writer.ToString());
        }
    }
}
