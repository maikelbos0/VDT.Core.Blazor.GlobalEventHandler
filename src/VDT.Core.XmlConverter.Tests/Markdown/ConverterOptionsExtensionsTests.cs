using VDT.Core.XmlConverter.Markdown;
using Xunit;
using Xunit.Abstractions;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class ConverterOptionsExtensionsTests {
        private readonly ITestOutputHelper output;

        public ConverterOptionsExtensionsTests(ITestOutputHelper output) {
            this.output = output;
        }

        [Fact]
        public void UseMarkdown_Returns_Self() {
            var options = new ConverterOptions();

            Assert.Equal(options, options.UseMarkdown());
        }

        [Fact]
        public void UseMarkdown_Convert_Pre() {
            const string xml = "<pre><span>Test</span></pre>";

            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            Assert.Equal("```\r\nTest\r\n```\r\n", converter.Convert(xml));
        }

        [Fact]
        public void UseMarkdown_Convert_Hyperlink() {
            const string xml = "<a href=\"https://www.google.com\">Test</a>";

            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            Assert.Equal("[Test](https://www.google.com)", converter.Convert(xml));
        }

        [Theory]
        [InlineData("<strong>Bold</strong>", "**Bold**")]
        [InlineData("<b>Bold</b>", "**Bold**")]
        [InlineData("<em>Italic</em>", "*Italic*")]
        [InlineData("<i>Italic</i>", "*Italic*")]
        [InlineData("<code>Inline code</code>", "`Inline code`")]
        [InlineData("<kbd>Inline code</kbd>", "`Inline code`")]
        [InlineData("<samp>Inline code</samp>", "`Inline code`")]
        [InlineData("<var>Inline code</var>", "`Inline code`")]
        public void UseMarkdown_Convert_Inline_Markup(string xml, string expectedMarkdown) {
            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            Assert.Equal(expectedMarkdown, converter.Convert(xml));
        }

        [Theory]
        [InlineData("Linebreak<br/>", "Linebreak  \r\n")]
        public void UseMarkdown_Convert_New_Lines(string xml, string expectedMarkdown) {
            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            Assert.Equal(expectedMarkdown, converter.Convert(xml));
        }

        [Theory]
        [InlineData("<html>Test</html>")]
        [InlineData("<body>Test</body>")]
        [InlineData("<html><body>Test</body></html>")]
        [InlineData("<ul>Test</ul>")]
        [InlineData("<ol>Test</ol>")]
        [InlineData("<menu>Test</menu>")]
        [InlineData("<span>Test</span>")]
        [InlineData("<div>Test</div>")]
        public void UseMarkdown_Convert_Removes_Unconvertible_Tags(string xml) {
            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            Assert.Equal("Test", converter.Convert(xml));
        }

        [Theory]
        [InlineData("<script>Don't render me</script>Test")]
        [InlineData("<style>Don't render me</style>Test")]
        [InlineData("<head>Don't render me</head>Test")]
        [InlineData("<frame>Don't render me</frame>Test")]
        [InlineData("<meta>Don't render me</meta>Test")]
        [InlineData("<iframe>Don't render me</iframe>Test")]
        [InlineData("<frameset>Don't render me</frameset>Test")]
        public void UseMarkdown_Convert_Removes_Elements_Without_Content(string xml) {
            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            Assert.Equal("Test", converter.Convert(xml));
        }

        [Fact]
        public void UseMarkdown_Convert_Text() {
            const string xml = "<p xml:space=\"preserve\">\t Test \t</p>\r\n\t <p> Test \t </p>";

            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            Assert.Equal("\r\n\r\nTest \r\n\r\nTest \r\n\r\n", converter.Convert(xml));
        }

        [Theory]
        [InlineData(UnknownElementHandlingMode.None, "<form>Test</form>")]
        [InlineData(UnknownElementHandlingMode.RemoveTags, "Test")]
        [InlineData(UnknownElementHandlingMode.RemoveElements, "")]
        public void UseMarkdown_Convert_Uses_UnknownElementHandlingMode(UnknownElementHandlingMode unknownElementHandlingMode, string expectedMarkdown) {
            var options = new ConverterOptions().UseMarkdown(unknownElementHandlingMode);
            var converter = new Converter(options);

            Assert.Equal(expectedMarkdown, converter.Convert("<form>Test</form>"));
        }

        [Fact]
        public void UseMarkdown_Convert_Uses_Default_UnknownElementHandlingMode_Node() {
            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            Assert.Equal("<form>Test</form>", converter.Convert("<form>Test</form>"));
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
    <li>
        For more information, search on <a href=""https://www.google.com""><strong>Google.com</strong></a>
        <h3>A header here!</h3>
        <p>And a paragraph!</p>
        <pre>
public void ThisIsCode() {
    Whitespace.Should().Be(""preserved"");
}</pre>
    </li>
</ol>

<blockquote>
Quote<br/>
Unquote
</blockquote>

<ul>
    <li>
        This is going to be interesting!
        <h3>A header in an unordered list!</h3>
    </li>
</ul>

<hr/>

<p>
    Why not embed an image?<br/>
    <img src=""https://picsum.photos/200"" alt=""Like this one!""/>
</p>
";
            var options = new ConverterOptions().UseMarkdown();
            var converter = new Converter(options);

            var result = converter.Convert(xml);

            output.WriteLine(result);
            Assert.Equal("\r\n\r\nThis is a paragraph\\.\r\n\r\nThis paragraph has a line break\\.  \r\nThis is line 2\\. \r\n\r\n1. Here we have some numbers\r\n1. This number has bullets: \r\n\t- Bullet\r\n\t- Foo\r\n1. ## A header in a list item\\!\r\n1. For more information, search on [**Google\\.com**](https://www.google.com)\r\n\t### A header here\\!\r\n\t\r\n\tAnd a paragraph\\!\r\n\t\r\n\t```\r\n\tpublic void ThisIsCode() {\r\n\t    Whitespace.Should().Be(\"preserved\");\r\n\t}\r\n\t```\r\n> Quote  \r\n> Unquote \r\n- This is going to be interesting\\! \r\n\t### A header in an unordered list\\!\r\n---\r\n\r\nWhy not embed an image?  \r\n![Like this one!](https://picsum.photos/200)\r\n\r\n", result);
        }

        [Theory]
        [InlineData("<del>Strikethrough</del>", "~~Strikethrough~~")]
        [InlineData("<mark>Highlighted</mark>", "==Highlighted==")]
        [InlineData("<sub>Subscript</sub>", "~Subscript~")]
        [InlineData("<sup>Superscript</sup>", "^Superscript^")]
        public void AddExtendedMarkdown__Convert_Inline_Markup(string xml, string expectedMarkdown) {
            var options = new ConverterOptions().UseMarkdown().AddExtendedMarkdown();
            var converter = new Converter(options);

            Assert.Equal(expectedMarkdown, converter.Convert(xml));
        }
    }
}
