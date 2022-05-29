using NSubstitute;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class ConverterOptionsBuilderTests {
        [Fact]
        public void Build_Always_Calls_SetNodeRemovingConverterForNonMarkdownNodeTypes() {
            var options = new ConverterOptions();
            var builder = new ConverterOptionsBuilder();
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().SetNodeRemovingConverterForNonMarkdownNodeTypes(options);
        }

        [Fact]
        public void Build_Always_Calls_AddHeaderElementConverters() {
            var options = new ConverterOptions();
            var builder = new ConverterOptionsBuilder();
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddHeaderElementConverters(options);
        }

        [Fact]
        public void Build_Always_Calls_AddParagraphConverter() {
            var options = new ConverterOptions();
            var builder = new ConverterOptionsBuilder();
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddParagraphConverter(options);
        }

        [Fact]
        public void Build_Always_Calls_AddLinebreakConverter() {
            var options = new ConverterOptions();
            var builder = new ConverterOptionsBuilder();
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddLinebreakConverter(options);
        }

        [Fact]
        public void Build_Always_Calls_AddListItemElementConverters() {
            var options = new ConverterOptions();
            var builder = new ConverterOptionsBuilder();
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddListItemElementConverters(options);
        }

        [Fact]
        public void Build_Always_Calls_AddHorizontalRuleConverter() {
            var options = new ConverterOptions();
            var builder = new ConverterOptionsBuilder();
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddHorizontalRuleConverter(options);
        }

        [Fact]
        public void Build_Always_Calls_AddHyperlinkConverter() {
            var options = new ConverterOptions();
            var builder = new ConverterOptionsBuilder();
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddHyperlinkConverter(options);
        }

        [Fact]
        public void Build_Always_Calls_AddImageConverter() {
            var options = new ConverterOptions();
            var builder = new ConverterOptionsBuilder();
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddImageConverter(options);
        }

        [Fact]
        public void Build_Always_Calls_SetDefaultElementConverter() {
            var options = new ConverterOptions();
            var builder = new ConverterOptionsBuilder();
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().SetDefaultElementConverter(options, UnknownElementHandlingMode.None);
        }
    }
}
