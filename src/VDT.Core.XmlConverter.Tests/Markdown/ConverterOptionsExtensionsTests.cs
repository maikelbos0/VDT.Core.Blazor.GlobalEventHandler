using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class ConverterOptionsExtensionsTests {
        [Fact]
        public void UseMarkdown_Returns_Self() {
            var options = new ConverterOptions();

            Assert.Same(options, options.UseMarkdown());
        }

        [Fact]
        public void CreateMarkdownBuilder_Sets_UnknownElementHandlingMode() {
            var builder = ConverterOptionsExtensions.CreateMarkdownBuilder(false, UnknownElementHandlingMode.RemoveTags);

            Assert.Equal(UnknownElementHandlingMode.RemoveTags, builder.UnknownElementHandlingMode);
        }

        [Fact]
        public void CreateMarkdownBuilder_With_UseExtendedSyntax_True_Adds_All_ElementConverterTargets() {
            var builder = ConverterOptionsExtensions.CreateMarkdownBuilder(true, null);

            Assert.Equal(new ConverterOptionsBuilder().AddAllElementConverters().ElementConverterTargets, builder.ElementConverterTargets);
        }

        [Fact]
        public void CreateMarkdownBuilder_With_UseExtendedSyntax_False_Adds_Basic_ElementConverterTarget() {
            var builder = ConverterOptionsExtensions.CreateMarkdownBuilder(false, null);

            Assert.Equal(new ConverterOptionsBuilder().ElementConverterTargets, builder.ElementConverterTargets);
        }
    }
}
