using System.IO;
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
            OutputAndAssert(
                new ConverterOptions().UseMarkdown(false, UnknownElementHandlingMode.RemoveElements),
                "SimpleDocument.html",
                "SimpleDocument.md"
            );
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Convert_Nested_List() {
            OutputAndAssert(
                new ConverterOptions().UseMarkdown(),
                "NestedList.html",
                "NestedList.md"
            );
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Convert_Blockquote() {
            OutputAndAssert(
                new ConverterOptions().UseMarkdown(),
                "Blockquote.html",
                "Blockquote.md"
            );
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Convert_Extended_Inline() {
            OutputAndAssert(
                new ConverterOptions().UseMarkdown(true),
                "ExtendedInline.html",
                "ExtendedInline.md"
            );
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Convert_Extended_Block () {
            OutputAndAssert(
                new ConverterOptions().UseMarkdown(true),
                "ExtendedBlock.html",
                "ExtendedBlock.md"
            );
        }

        private void OutputAndAssert(ConverterOptions options, string htmlFile, string expectedMarkdownFile) {
            var converter = new Converter(options);

            var result = converter.Convert(ReadFile(htmlFile));

            outputHelper.WriteLine(result);
            Assert.Equal(ReadFile(expectedMarkdownFile), result);
        }

        private static string ReadFile(string fileName) => File.ReadAllText(Path.Combine("Markdown", fileName));
    }
}
