﻿using System.IO;
using System.Text;
using System.Xml;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class ConverterOptionsExtensionsTests {
        [Fact]
        public void UseMarkdown_Returns_Self() {
            var options = new ConverterOptions();
            
            Assert.Equal(options, options.UseMarkdown());
        }

        [Fact]
        public void UseMarkdown_Convert_Hyperlink() {
            const string xml = "<a href=\"https://www.google.com\">Test</a>";

            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            Assert.Equal("[Test](https://www.google.com)", converter.Convert(xml));
        }

        [Theory]
        [InlineData("<li>List item</li>", "- List item\r\n")]
        [InlineData("<menu><li>List item</li></menu>", "- List item\r\n")]
        [InlineData("<ul><li>List item</li></ul>", "- List item\r\n")]
        [InlineData("<ol><li>List item</li></ol>", "1. List item\r\n")]
        public void UseMarkdown_Convert_List_Item(string xml, string expectedMarkdown) {
            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            Assert.Equal(expectedMarkdown, converter.Convert(xml));
        }

        [Theory]
        [InlineData("<h1>Heading 1</h1>", "# Heading 1\r\n")]
        [InlineData("<h2>Heading 2</h2>", "## Heading 2\r\n")]
        [InlineData("<h3>Heading 3</h3>", "### Heading 3\r\n")]
        [InlineData("<h4>Heading 4</h4>", "#### Heading 4\r\n")]
        [InlineData("<h5>Heading 5</h5>", "##### Heading 5\r\n")]
        [InlineData("<h6>Heading 6</h6>", "###### Heading 6\r\n")]
        public void UseMarkdown_Convert_Header(string xml, string expectedMarkdown) {
            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            Assert.Equal(expectedMarkdown, converter.Convert(xml));
        }

        [Theory]
        [InlineData("<strong>Bold</strong>", "**Bold**")]
        [InlineData("<b>Bold</b>", "**Bold**")]
        [InlineData("<em>Italic</em>", "*Italic*")]
        [InlineData("<i>Italic</i>", "*Italic*")]
        public void UseMarkdown_Convert_Inline_Markup(string xml, string expectedMarkdown) {
            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            Assert.Equal(expectedMarkdown, converter.Convert(xml));
        }

        [Theory]
        [InlineData("Linebreak<br/>", "Linebreak  \r\n")]
        [InlineData("<p>Paragraph</p>", "Paragraph\r\n\r\n")]
        public void UseMarkdown_Convert_New_Lines(string xml, string expectedMarkdown) {
            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            Assert.Equal(expectedMarkdown, converter.Convert(xml));
        }

        [Fact]
        public void UseMarkdown_Convert_Text() {
            const string xml = "<p xml:space=\"preserve\">\t Test \t</p>\r\n\t <p> Test \t </p>";

            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            Assert.Equal("Test \r\n\r\nTest \r\n\r\n", converter.Convert(xml));
        }

        [Theory]
        [InlineData("<?xml-stylesheet type=\"text/xsl\" href=\"style.xsl\"?>Test", "Test")]
        [InlineData("<?xml version=\"1.0\" encoding=\"UTF-8\"?>Test", "Test")]
        [InlineData("<![CDATA[Content]]>Test", "Test")]
        public void UseMarkdown_Convert_Removes_All_Unconvertible_Node_Types(string xml, string expectedMarkdown) {
            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            Assert.Equal(expectedMarkdown, converter.Convert(xml));
        }

        [Fact]
        public void UseMarkdown_Convert_Removes_Document_Type_Declarations() {
            const string xml = "<!DOCTYPE foo [ <!ENTITY val \"bar\"> ]><p>Test</p>";

            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            using var reader = XmlReader.Create(stream, new XmlReaderSettings() { DtdProcessing = DtdProcessing.Parse });

            Assert.Equal("Test\r\n\r\n", converter.Convert(reader));
        }

        [Fact]
        public void UseMarkdown_Convert() {
            const string xml = @"
<p>This is a paragraph.</p>

<p>
    This paragraph has a line break.<br/>
    This is line 2.
</p>

<ol>
    <li>Here we have some numbers</li>
    <li>This number has bullets:
        <ul>
            <li>Bullet</li>
            <li>Foo</li>
        </ul>
    </li>
    <li><h2>A header in a list item!</h2></li>
    <li>For more information, search on <a href=""https://www.google.com""><strong>Google.com</strong></a></li>
</ol>

<p>
    Why not embed an image? <img src=""https://picsum.photos/200"" alt=""Like this one!""/>
</p>
";
            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);
            
            Assert.Equal("This is a paragraph\\.\r\n\r\nThis paragraph has a line break\\.  \r\nThis is line 2\\. \r\n\r\n1. Here we have some numbers\r\n1. This number has bullets: \r\n\t- Bullet\r\n\t- Foo\r\n1. ## A header in a list item\\!\r\n1. For more information, search on [**Google\\.com**](https://www.google.com)\r\nWhy not embed an image? ![Like this one!](https://picsum.photos/200)\r\n\r\n", converter.Convert(xml));
        }
    }
}
