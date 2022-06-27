using System.IO;
using System.Xml;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class NodeRemovingConverterTests {
        [Fact]
        public void Convert() {
            using var writer = new StringWriter();

            var converter = new NodeRemovingConverter();

            converter.Convert(writer, NodeDataHelper.Create(XmlNodeType.Comment));

            Assert.Equal("", writer.ToString());
        }
    }
}
