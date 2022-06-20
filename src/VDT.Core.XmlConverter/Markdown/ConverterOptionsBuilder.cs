using System;
using System.Collections.Generic;
using System.Linq;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Builder for adding converter options to convert HTML markup to Markdown format
    /// </summary>
    public class ConverterOptionsBuilder {
        private static readonly Dictionary<ElementConverterTarget, Action<IConverterOptionsAssembler, ConverterOptionsBuilder, ConverterOptions>> targetAssemblerActionMapping = new Dictionary<ElementConverterTarget, Action<IConverterOptionsAssembler, ConverterOptionsBuilder, ConverterOptions>>() {
            { ElementConverterTarget.Heading, (assembler, _, options) => assembler.AddHeadingConverters(options) },
            { ElementConverterTarget.Paragraph, (assembler, _, options) => assembler.AddParagraphConverter(options) },
            { ElementConverterTarget.Linebreak, (assembler, _, options) => assembler.AddLinebreakConverter(options) },
            { ElementConverterTarget.ListItem, (assembler, _, options) => assembler.AddListItemConverters(options) },
            { ElementConverterTarget.HorizontalRule, (assembler, _, options) => assembler.AddHorizontalRuleConverter(options) },
            { ElementConverterTarget.Blockquote, (assembler, _, options) => assembler.AddBlockquoteConverter(options) },
            { ElementConverterTarget.Pre, (assembler, builder, options) => assembler.AddPreConverters(options, builder.PreConversionMode) },
            { ElementConverterTarget.Hyperlink, (assembler, _, options) => assembler.AddHyperlinkConverter(options) },
            { ElementConverterTarget.Image, (assembler, _, options) => assembler.AddImageConverter(options) },
            { ElementConverterTarget.Important, (assembler, _, options) => assembler.AddImportantConverter(options) },
            { ElementConverterTarget.Emphasis, (assembler, _, options) => assembler.AddEmphasisConverter(options) },
            { ElementConverterTarget.InlineCode, (assembler, _, options) => assembler.AddInlineCodeConverter(options) },
            { ElementConverterTarget.Strikethrough, (assembler, _, options) => assembler.AddStrikethroughConverter(options) },
            { ElementConverterTarget.Highlight, (assembler, _, options) => assembler.AddHighlightConverter(options) },
            { ElementConverterTarget.Subscript, (assembler, _, options) => assembler.AddSubscriptConverter(options) },
            { ElementConverterTarget.Superscript, (assembler, _, options) => assembler.AddSuperscriptConverter(options) },
            // TODO make configurable
            { ElementConverterTarget.RemoveTag, (assembler, _, options) => assembler.AddTagRemovingElementConverter(options) },
            // TODO make configurable
            { ElementConverterTarget.RemoveElement, (assembler, _, options) => assembler.AddElementRemovingConverter(options) }
        };

        /// <summary>
        /// Specifies the way preformatted text get converted to Markdown code blocks; defaults to <see cref="PreConversionMode.Fenced"/>
        /// </summary>
        public PreConversionMode PreConversionMode { get; set; } = PreConversionMode.Fenced;

        /// <summary>
        /// Specifies the way to handle elements that can't be converted to Markdown; defaults to <see cref="UnknownElementHandlingMode.None"/>
        /// </summary>
        public UnknownElementHandlingMode UnknownElementHandlingMode { get; set; } = UnknownElementHandlingMode.None;

        /// <summary>
        /// Specifies the way character escapes in Markdown text are handled; defaults to <see cref="CharacterEscapeMode.ElementConverterBased"/>
        /// </summary>
        public CharacterEscapeMode CharacterEscapeMode { get; set; } = CharacterEscapeMode.ElementConverterBased;

        /// <summary>
        /// Custom character escapes for converting to Markdown text; these will overwrite any default escape sequences in case collisions are found
        /// </summary>
        public Dictionary<char, string> CustomCharacterEscapes { get; set; } = new Dictionary<char, string>();

        /// <summary>
        /// Targets for converting HTML elements to Markdown to add to the resulting <see cref="ConverterOptions"/>
        /// </summary>
        public HashSet<ElementConverterTarget> ElementConverterTargets { get; set; } = new HashSet<ElementConverterTarget>() {
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
        };

        /// <summary>
        /// Specify the way preformatted text get converted to Markdown code blocks
        /// </summary>
        /// <param name="preConversionMode">Specifies the way preformatted text get converted to Markdown code blocks</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public ConverterOptionsBuilder UsePreConversionMode(PreConversionMode preConversionMode) {
            PreConversionMode = preConversionMode;

            return this;
        }

        /// <summary>
        /// Specify the way to handle elements that can't be converted to Markdown
        /// </summary>
        /// <param name="unknownElementHandlingMode">Specifies the way to handle elements that can't be converted to Markdown</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public ConverterOptionsBuilder UseUnknownElementHandlingMode(UnknownElementHandlingMode unknownElementHandlingMode) {
            UnknownElementHandlingMode = unknownElementHandlingMode;

            return this;
        }

        /// <summary>
        /// Specify the way character escapes in Markdown text are handled
        /// </summary>
        /// <param name="characterEscapeMode">Specifies the way character escapes in Markdown text are handled</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public ConverterOptionsBuilder UseCharacterEscapeMode(CharacterEscapeMode characterEscapeMode) {
            CharacterEscapeMode = characterEscapeMode;

            return this;
        }

        /// <summary>
        /// Add a custom character escape for converting to Markdown text; these will overwrite any default escape sequences in case collisions are found
        /// </summary>
        /// <param name="character">Character to escape</param>
        /// <param name="escapeSequence">Value to replace the character with if found</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public ConverterOptionsBuilder AddCustomCharacterEscape(char character, string escapeSequence) {
            CustomCharacterEscapes[character] = escapeSequence;

            return this;
        }

        /// <summary>
        /// Specify custom character escapes for converting to Markdown text; these will overwrite any default escape sequences in case collisions are found
        /// </summary>
        /// <param name="characterEscapes">Custom character escapes for converting to Markdown text</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public ConverterOptionsBuilder UseCustomCharacterEscapes(Dictionary<char, string> characterEscapes) {
            CustomCharacterEscapes = characterEscapes;

            return this;
        }

        /// <summary>
        /// Add converters for target HTML elements to Markdown to the resulting <see cref="ConverterOptions"/>
        /// </summary>
        /// <param name="targets">Target HTML elements to add converters for</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public ConverterOptionsBuilder AddElementConverters(params ElementConverterTarget[] targets) => AddElementConverters(targets.AsEnumerable());

        /// <summary>
        /// Add converters for target HTML elements to Markdown to the resulting <see cref="ConverterOptions"/>
        /// </summary>
        /// <param name="targets">Target HTML elements to add converters for</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public ConverterOptionsBuilder AddElementConverters(IEnumerable<ElementConverterTarget> targets) {
            ElementConverterTargets.UnionWith(targets);

            return this;
        }

        /// <summary>
        /// Add converters for all available target HTML elements to Markdown to the resulting <see cref="ConverterOptions"/>
        /// </summary>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public ConverterOptionsBuilder AddAllElementConverters() {
            ElementConverterTargets.UnionWith(Enum.GetValues(typeof(ElementConverterTarget)).Cast<ElementConverterTarget>());

            return this;
        }

        /// <summary>
        /// Remove converters for target HTML elements to Markdown from the resulting <see cref="ConverterOptions"/>
        /// </summary>
        /// <param name="targets">Target HTML elements to remove converters for</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public ConverterOptionsBuilder RemoveElementConverters(params ElementConverterTarget[] targets) => RemoveElementConverters(targets.AsEnumerable());

        /// <summary>
        /// Remove converters for target HTML elements to Markdown from the resulting <see cref="ConverterOptions"/>
        /// </summary>
        /// <param name="targets">Target HTML elements to remove converters for</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public ConverterOptionsBuilder RemoveElementConverters(IEnumerable<ElementConverterTarget> targets) {
            ElementConverterTargets.ExceptWith(targets);

            return this;
        }

        /// <summary>
        /// Build converter options to convert HTML markup to Markdown format
        /// </summary>
        /// <returns></returns>
        public ConverterOptions Build() {
            return Build(new ConverterOptions(), new ConverterOptionsAssembler());
        }

        internal ConverterOptions Build(ConverterOptions options, IConverterOptionsAssembler assembler) {
            foreach (var assemblerAction in ElementConverterTargets.Select(target => targetAssemblerActionMapping[target])) {
                assemblerAction(assembler, this, options);
            }

            assembler.SetTextConverter(options);
            assembler.SetNodeConverterForNonMarkdownNodeTypes(options);
            assembler.SetDefaultElementConverter(options, UnknownElementHandlingMode);

            return options;
        }
    }
}
