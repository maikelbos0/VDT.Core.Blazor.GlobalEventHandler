using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class ConverterOptionsBuilderTests {
        [Fact]
        public void UnknownElementHandlingMode_Defaults_To_None() {
            var builder = new ConverterOptionsBuilder();

            Assert.Equal(UnknownElementHandlingMode.None, builder.UnknownElementHandlingMode);
        }

        [Fact]
        public void ElementConverterTargets_Defaults_To_Basic_Markdown() {
            var builder = new ConverterOptionsBuilder();

            Assert.Equal(new HashSet<ElementConverterTarget>() {
                ElementConverterTarget.Heading,
                ElementConverterTarget.Paragraph,
                ElementConverterTarget.Linebreak,
                ElementConverterTarget.ListItem,
                ElementConverterTarget.HorizontalRule,
                ElementConverterTarget.Blockquote,
                ElementConverterTarget.Pre,
                ElementConverterTarget.Hyperlink,
                ElementConverterTarget.Image,
                ElementConverterTarget.Important,
                ElementConverterTarget.Emphasis,
                ElementConverterTarget.InlineCode,
                ElementConverterTarget.RemoveTag,
                ElementConverterTarget.RemoveElement
            }, builder.ElementConverterTargets);
        }

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

            builder.UnknownElementHandlingMode = UnknownElementHandlingMode.RemoveTags;
            builder.Build(options, assembler);

            assembler.Received().SetDefaultElementConverter(options, UnknownElementHandlingMode.RemoveTags);
        }

        [Fact]
        public void UseUnknownElementHandlingMode_Returns_Self() {
            var builder = new ConverterOptionsBuilder();

            Assert.Same(builder, builder.UseUnknownElementHandlingMode(UnknownElementHandlingMode.RemoveTags));
        }

        [Fact]
        public void UseUnknownElementHandlingMode_Sets_UnknownElementHandlingMode() {
            var builder = new ConverterOptionsBuilder();

            builder.UseUnknownElementHandlingMode(UnknownElementHandlingMode.RemoveTags);

            Assert.Equal(UnknownElementHandlingMode.RemoveTags, builder.UnknownElementHandlingMode);
        }

        [Fact]
        public void UsePreConversionMode_Sets_PreConversionMode() {
            var builder = new ConverterOptionsBuilder();

            builder.UsePreConversionMode(PreConversionMode.Indented);

            Assert.Equal(PreConversionMode.Indented, builder.PreConversionMode);
        }

        [Fact]
        public void UsePreConversionMode_Returns_Self() {
            var builder = new ConverterOptionsBuilder();

            Assert.Same(builder, builder.UsePreConversionMode(PreConversionMode.Indented));
        }

        [Fact]
        public void AddElementConverters_Returns_Self() {
            var builder = new ConverterOptionsBuilder();

            Assert.Same(builder, builder.AddElementConverters());
        }

        [Fact]
        public void AddElementConverters_Adds_Targets() {
            var builder = CreateBuilder(ElementConverterTarget.Hyperlink);

            builder.AddElementConverters(ElementConverterTarget.Hyperlink, ElementConverterTarget.Image, ElementConverterTarget.InlineCode);

            Assert.Equal(new HashSet<ElementConverterTarget>() {
                ElementConverterTarget.Hyperlink,
                ElementConverterTarget.Image,
                ElementConverterTarget.InlineCode,
            }, builder.ElementConverterTargets);
        }

        [Fact]
        public void AddAllElementConverters_Returns_Self() {
            var builder = new ConverterOptionsBuilder();

            Assert.Same(builder, builder.AddAllElementConverters());
        }

        [Fact]
        public void AddAllElementConverters_Adds_All_Targets() {
            var builder = CreateBuilder(ElementConverterTarget.Hyperlink, ElementConverterTarget.Emphasis);

            builder.AddAllElementConverters();

            Assert.Equal(new HashSet<ElementConverterTarget>(Enum.GetValues(typeof(ElementConverterTarget)).Cast<ElementConverterTarget>()), builder.ElementConverterTargets);
        }

        [Fact]
        public void RemoveElementConverters_Returns_Self() {
            var builder = new ConverterOptionsBuilder();

            Assert.Same(builder, builder.RemoveElementConverters());
        }

        [Fact]
        public void RemoveElementConverters_Removes_Targets() {
            var builder = CreateBuilder(ElementConverterTarget.Hyperlink, ElementConverterTarget.Image, ElementConverterTarget.InlineCode);
            builder.RemoveElementConverters(ElementConverterTarget.Heading, ElementConverterTarget.Image, ElementConverterTarget.InlineCode);

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

            builder.PreConversionMode = PreConversionMode.Indented;
            builder.Build(options, assembler);

            assembler.Received().AddPreConverters(options, builder.PreConversionMode);
        }

        [Fact]
        public void Build_ElementConverterTarget_Hyperlink_Calls_AddHyperlinkConverter() {
            var options = new ConverterOptions();
            var builder = CreateBuilder(ElementConverterTarget.Hyperlink);
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddHyperlinkConverter(options);
        }

        [Fact]
        public void Build_ElementConverterTarget_Image_Calls_AddImageConverter() {
            var options = new ConverterOptions();
            var builder = CreateBuilder(ElementConverterTarget.Image);
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddImageConverter(options);
        }

        [Fact]
        public void Build_ElementConverterTarget_Emphasis_Calls_AddEmphasisConverter() {
            var options = new ConverterOptions();
            var builder = CreateBuilder(ElementConverterTarget.Emphasis);
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddEmphasisConverter(options);
        }

        [Fact]
        public void Build_ElementConverterTarget_Important_Calls_AddImportantConverter() {
            var options = new ConverterOptions();
            var builder = CreateBuilder(ElementConverterTarget.Important);
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddImportantConverter(options);
        }

        [Fact]
        public void Build_ElementConverterTarget_InlineCode_Calls_AddInlineCodeConverter() {
            var options = new ConverterOptions();
            var builder = CreateBuilder(ElementConverterTarget.InlineCode);
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddInlineCodeConverter(options);
        }

        [Fact]
        public void Build_ElementConverterTarget_Strikethrough_Calls_AddStrikethroughConverter() {
            var options = new ConverterOptions();
            var builder = CreateBuilder(ElementConverterTarget.Strikethrough);
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddStrikethroughConverter(options);
        }

        [Fact]
        public void Build_ElementConverterTarget_Highlight_Calls_AddHighlightConverter() {
            var options = new ConverterOptions();
            var builder = CreateBuilder(ElementConverterTarget.Highlight);
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddHighlightConverter(options);
        }

        [Fact]
        public void Build_ElementConverterTarget_Subscript_Calls_AddSubscriptConverter() {
            var options = new ConverterOptions();
            var builder = CreateBuilder(ElementConverterTarget.Subscript);
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddSubscriptConverter(options);
        }

        [Fact]
        public void Build_ElementConverterTarget_Superscript_Calls_AddSuperscriptConverter() {
            var options = new ConverterOptions();
            var builder = CreateBuilder(ElementConverterTarget.Superscript);
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddSuperscriptConverter(options);
        }

        [Fact]
        public void Build_ElementConverterTarget_RemoveTag_Calls_AddTagRemovingElementConverter() {
            var options = new ConverterOptions();
            var builder = CreateBuilder(ElementConverterTarget.RemoveTag);
            var assembler = Substitute.For<IConverterOptionsAssembler>();

            builder.Build(options, assembler);

            assembler.Received().AddTagRemovingElementConverter(options);
        }

        [Fact]
        public void Build_ElementConverterTarget_RemoveElement_Calls_AddElementRemovingConverter() {
            var options = new ConverterOptions();
            var builder = CreateBuilder(ElementConverterTarget.RemoveElement);
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
