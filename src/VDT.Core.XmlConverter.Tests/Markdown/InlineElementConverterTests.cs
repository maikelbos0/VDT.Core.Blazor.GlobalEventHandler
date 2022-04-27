using System.IO;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class InlineElementConverterTests {
        [Fact]
        public void RenderStart_Renders_StartOuput() {
            using var writer = new StringWriter();

            var converter = new InlineElementConverter("start", "end", "foo", "bar");

            converter.RenderStart(ElementDataHelper.Create("bar"), writer);

            Assert.Equal("start", writer.ToString());
        }

        [Fact]
        public void RenderEnd_Renders_EndOuput() {
            using var writer = new StringWriter();

            var converter = new InlineElementConverter("start", "end", "foo", "bar");

            converter.RenderEnd(ElementDataHelper.Create("bar"), writer);

            Assert.Equal("end", writer.ToString());
        }
    }
}
