using VDT.Core.XmlConverter.Markdown;
using Xunit;
using Xunit.Abstractions;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class ConverterTests {
        private readonly ITestOutputHelper outputHelper;

        public ConverterTests(ITestOutputHelper outputHelper) {
            this.outputHelper = outputHelper;
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Convert_Simple_Document() {
            const string xml = @"
<h2>This is a header.</h2>

<p>This is a paragraph.</p>

<p>
    This paragraph has a line break.<br/>
    This is line 2.
</p>

<ul>
    <li>A point</li>
    <li>Another point</li>
</ul>

<form>
We can't do anything with forms so let's not render them.
</form>
";

            var options = new ConverterOptions().UseMarkdown(unknownElementHandlingMode: UnknownElementHandlingMode.RemoveElements);

            OutputAndAssertResult(xml, options, "\r\n## This is a header\\.\r\n\r\nThis is a paragraph\\.\r\n\r\nThis paragraph has a line break\\.  \r\nThis is line 2\\. \r\n\r\n- A point\r\n- Another point\r\n");
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Convert_Nested_List() {
            const string xml = @"
<ol>
    <li>Here we have some numbers</li>
    <li>This number has bullets:
        <ul>
            <li>Bullet</li>
            <li>Foo</li>
        </ul>
    </li>
    <li>
        <h3>A header here!</h3>
    </li>
    <li>
        For some reason there's text before the header.
        <h3>A header here!</h3>
        <p>And a paragraph!</p>
    </li>
</ol>
";

            var options = new ConverterOptions().UseMarkdown();

            OutputAndAssertResult(xml, options, "\r\n1. Here we have some numbers\r\n1. This number has bullets: \r\n\t- Bullet\r\n\t- Foo\r\n1. \r\n\t### A header here\\!\r\n1. For some reason there's text before the header\\. \r\n\t### A header here\\!\r\n\t\r\n\tAnd a paragraph\\!\r\n\t\r\n");
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Convert_Blockquote() {
            const string xml = @"

<blockquote>
<h1>On quotes</h1>
Here goes a quote<br/>
It's the <i>best</i> quote<br/>
We only want <strong>good</strong> quotes; the best quotes
</blockquote>
";

            var options = new ConverterOptions().UseMarkdown();

            OutputAndAssertResult(xml, options, "\r\n> # On quotes\r\n> Here goes a quote  \r\n> It's the *best* quote  \r\n> We only want **good** quotes; the best quotes \r\n");
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Convert_Extended_Inline() {
            const string xml = @"
<p>
    This is a paragraph with <sup>superscript</sup> and <sub>subscript</sub>.
    We could even use <del>strikethrough</del> and <mark>highlight</mark> if we wanted.
</p>
";

            var options = new ConverterOptions().UseMarkdown(useExtendedSyntax: true);

            OutputAndAssertResult(xml, options, "\r\n\r\nThis is a paragraph with ^superscript^ and ~subscript~\\. We could even use ~~strikethrough~~ and ==highlight== if we wanted\\. \r\n\r\n");
        }

        private void OutputAndAssertResult(string xml, ConverterOptions options, string expectedResult) {
            var converter = new Converter(options);

            var result = converter.Convert(xml);

            outputHelper.WriteLine(result);
            Assert.Equal(expectedResult, result);
        }
    }
}
