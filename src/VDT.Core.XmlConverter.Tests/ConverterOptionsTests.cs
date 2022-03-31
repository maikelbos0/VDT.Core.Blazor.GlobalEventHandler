using VDT.Core.XmlConverter.Elements;
using VDT.Core.XmlConverter.Nodes;
using Xunit;

namespace VDT.Core.XmlConverter.Tests {
    public class ConverterOptionsTests {
        [Fact]
        public void ElementConverters() {
            var options = new ConverterOptions();

            Assert.Empty(options.ElementConverters);
        }
        
        [Fact]
        public void DefaultElementConverter() { 
            var options = new ConverterOptions();

            Assert.IsType<NoOpElementConverter>(options.DefaultElementConverter);
        }

        [Fact]
        public void TextConverter() { 
            var options = new ConverterOptions();
            var converter = Assert.IsType<NodeValueConverter>(options.TextConverter);
            
            Assert.True(converter.XmlEncodeValue);
            Assert.Null(converter.StartOuput);
            Assert.Null(converter.EndOutput);
        }

        [Fact]
        public void CDataConverter() { 
            var options = new ConverterOptions();
            var converter = Assert.IsType<NodeValueConverter>(options.CDataConverter);

            Assert.False(converter.XmlEncodeValue);
            Assert.Equal("<![CDATA[", converter.StartOuput);
            Assert.Equal("]]>", converter.EndOutput);
        }

        [Fact]
        public void CommentConverter() { 
            var options = new ConverterOptions();
            var converter = Assert.IsType<NodeValueConverter>(options.CommentConverter);

            Assert.False(converter.XmlEncodeValue);
            Assert.Equal("<!--", converter.StartOuput);
            Assert.Equal("-->", converter.EndOutput);
        }
    }
}
