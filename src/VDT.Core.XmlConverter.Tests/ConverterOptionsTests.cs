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
            var converter = Assert.IsType<FormattingNodeConverter>(options.TextConverter);
            
            Assert.True(converter.XmlEncodeValue);
            Assert.Equal("value", converter.Formatter("name", "value"));
        }

        [Fact]
        public void CDataConverter() { 
            var options = new ConverterOptions();
            var converter = Assert.IsType<FormattingNodeConverter>(options.CDataConverter);

            Assert.False(converter.XmlEncodeValue);
            Assert.Equal("<![CDATA[value]]>", converter.Formatter("name", "value"));
        }

        [Fact]
        public void CommentConverter() { 
            var options = new ConverterOptions();
            var converter = Assert.IsType<FormattingNodeConverter>(options.CommentConverter);

            Assert.False(converter.XmlEncodeValue);
            Assert.Equal("<!--value-->", converter.Formatter("name", "value"));
        }

        [Fact]
        public void XmlDeclarationConverter() { 
            var options = new ConverterOptions();
            var converter = Assert.IsType<FormattingNodeConverter>(options.XmlDeclarationConverter);

            Assert.False(converter.XmlEncodeValue);
            Assert.Equal("<?xml value?>", converter.Formatter("name", "value"));
        }

        [Fact]
        public void WhitespaceConverter() { 
            var options = new ConverterOptions();
            var converter = Assert.IsType<FormattingNodeConverter>(options.WhitespaceConverter);

            Assert.False(converter.XmlEncodeValue);
            Assert.Equal("value", converter.Formatter("name", "value"));
        }

        [Fact]
        public void SignificantWhitespaceConverter() { 
            var options = new ConverterOptions();
            var converter = Assert.IsType<FormattingNodeConverter>(options.SignificantWhitespaceConverter);

            Assert.False(converter.XmlEncodeValue);
            Assert.Equal("value", converter.Formatter("name", "value"));
        }
    }
}
