using NSubstitute;
using System.Collections.Generic;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class ConverterOptionsBuilderTests {
        [Fact]
        public void Build_Always_Calls_SetNodeConverterForNonMarkdownNodeTypes() {
            var options = new ConverterOptions();
            var builder = new ConverterOptionsBuilder();
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().SetNodeConverterForNonMarkdownNodeTypes(options);
        }

        [Fact]
        public void Build_Always_Calls_SetTextConverter() {
            var options = new ConverterOptions();
            var builder = new ConverterOptionsBuilder();
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().SetTextConverter(options);
        }

        [Fact]
        public void Build_Always_Calls_SetDefaultElementConverter() {
            var options = new ConverterOptions();
            var builder = new ConverterOptionsBuilder();
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().SetDefaultElementConverter(options, UnknownElementHandlingMode.None);
        }

        [Fact]
        public void AddTargets_Returns_Self() {
            var builder = new ConverterOptionsBuilder();

            Assert.Same(builder, builder.AddTargets());
        }

        [Fact]
        public void AddTargets_Adds_Targets() {
            var builder = CreateBuilder(ElementConverterTarget.Hyperlink);

            builder.AddTargets(ElementConverterTarget.Hyperlink, ElementConverterTarget.Image, ElementConverterTarget.InlineCode);

            Assert.Equal(3, builder.ElementConverterTargets.Count);
            Assert.Contains(ElementConverterTarget.Hyperlink, builder.ElementConverterTargets);
            Assert.Contains(ElementConverterTarget.Image, builder.ElementConverterTargets);
            Assert.Contains(ElementConverterTarget.InlineCode, builder.ElementConverterTargets);
        }

        [Fact]
        public void RemoveTargets_Returns_Self() {
            var builder = new ConverterOptionsBuilder();

            Assert.Same(builder, builder.RemoveTargets());
        }

        [Fact]
        public void RemoveTargets_Removes_Targets() {
            var builder = CreateBuilder(ElementConverterTarget.Hyperlink, ElementConverterTarget.Image, ElementConverterTarget.InlineCode);
            builder.RemoveTargets(ElementConverterTarget.Heading, ElementConverterTarget.Image, ElementConverterTarget.InlineCode);

            Assert.Equal(ElementConverterTarget.Hyperlink, Assert.Single(builder.ElementConverterTargets));
        }

        [Fact]
        public void Build_ElementConverterTarget_Heading_Calls_AddHeadingConverters() {
            var options = new ConverterOptions();
            var builder = CreateBuilder(ElementConverterTarget.Heading);
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddHeadingConverters(options);
        }

        [Fact]
        public void Build_ElementConverterTarget_Paragraph_Calls_AddParagraphConverter() {
            var options = new ConverterOptions();
            var builder = CreateBuilder(ElementConverterTarget.Paragraph);
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddParagraphConverter(options);
        }

        [Fact]
        public void Build_ElementConverterTarget_Linebreak_Calls_AddLinebreakConverter() {
            var options = new ConverterOptions();
            var builder = CreateBuilder(ElementConverterTarget.Linebreak);
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddLinebreakConverter(options);
        }

        [Fact]
        public void Build_ElementConverterTarget_ListItem_Calls_AddListItemConverters() {
            var options = new ConverterOptions();
            var builder = CreateBuilder(ElementConverterTarget.ListItem);
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddListItemConverters(options);
        }

        [Fact]
        public void Build_ElementConverterTarget_HorizontalRule_Calls_AddHorizontalRuleConverter() {
            var options = new ConverterOptions();
            var builder = CreateBuilder(ElementConverterTarget.HorizontalRule);
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddHorizontalRuleConverter(options);
        }

        [Fact]
        public void Build_ElementConverterTarget_Blockquote_Calls_AddBlockquoteConverter() {
            var options = new ConverterOptions();
            var builder = CreateBuilder(ElementConverterTarget.Blockquote);
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddBlockquoteConverter(options);
        }

        [Fact]
        public void Build_ElementConverterTarget_Pre_Calls_AddPreConverters() {
            var options = new ConverterOptions();
            var builder = CreateBuilder(ElementConverterTarget.Pre);
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddPreConverters(options);
        }


        // TODO convert below tests to be based on targets


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
        public void Build_Always_Calls_AddEmphasisConverters() {
            var options = new ConverterOptions();
            var builder = new ConverterOptionsBuilder();
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddEmphasisConverters(options);
        }

        [Fact]
        public void Build_Always_Calls_AddInlineCodeConverter() {
            var options = new ConverterOptions();
            var builder = new ConverterOptionsBuilder();
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddInlineCodeConverter(options);
        }

        [Fact]
        public void Build_Always_Calls_AddTagRemovingElementConverter() {
            var options = new ConverterOptions();
            var builder = new ConverterOptionsBuilder();
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddTagRemovingElementConverter(options);
        }

        [Fact]
        public void Build_Always_Calls_AddElementRemovingConverter() {
            var options = new ConverterOptions();
            var builder = new ConverterOptionsBuilder();
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddElementRemovingConverter(options);
        }

        private ConverterOptionsBuilder CreateBuilder(params ElementConverterTarget[] targets)
            => new ConverterOptionsBuilder() {
                ElementConverterTargets = new HashSet<ElementConverterTarget>(targets)
            };
    }
}
