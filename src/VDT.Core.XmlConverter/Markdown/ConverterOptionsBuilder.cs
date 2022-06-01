using System;
using System.Collections.Generic;
using System.Linq;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Builder for adding converter options to convert HTML markup to Markdown format
    /// </summary>
    public class ConverterOptionsBuilder {
        private static Dictionary<ElementConverterTarget, Action<IConverterOptionsAssembler, ConverterOptionsBuilder, ConverterOptions>> targetAssemblerActionMapping = new Dictionary<ElementConverterTarget, Action<IConverterOptionsAssembler, ConverterOptionsBuilder, ConverterOptions>>() {
            { ElementConverterTarget.Heading, (assembler, _, options) =>  assembler.AddHeadingConverters(options) },
            { ElementConverterTarget.Paragraph, (assembler, _, options) =>  assembler.AddParagraphConverter(options) },
            { ElementConverterTarget.Linebreak, (assembler, _, options) =>  assembler.AddLinebreakConverter(options) },
            { ElementConverterTarget.ListItem, (assembler, _, options) =>  assembler.AddListItemConverters(options) },
            { ElementConverterTarget.HorizontalRule, (assembler, _, options) =>  assembler.AddHorizontalRuleConverter(options) },
            { ElementConverterTarget.Blockquote, (assembler, _, options) =>  assembler.AddBlockquoteConverter(options) },
            { ElementConverterTarget.Pre, (assembler, _, options) =>  assembler.AddPreConverters(options) },
            { ElementConverterTarget.Hyperlink, (assembler, _, options) =>  assembler.AddHyperlinkConverter(options) },
            { ElementConverterTarget.Image, (assembler, _, options) =>  assembler.AddImageConverter(options) },
            { ElementConverterTarget.Emphasis, (assembler, _, options) =>  assembler.AddEmphasisConverters(options) },
            { ElementConverterTarget.InlineCode, (assembler, _, options) =>  assembler.AddInlineCodeConverter(options) },
            { ElementConverterTarget.RemoveTag, (assembler, _, options) =>  assembler.AddTagRemovingElementConverter(options) },
            { ElementConverterTarget.RemoveElement, (assembler, _, options) =>  assembler.AddElementRemovingConverter(options) }
        };

        /// <summary>
        /// Specifies the way to handle elements that can't be converted to Markdown
        /// </summary>
        public UnknownElementHandlingMode UnknownElementHandlingMode { get; set; }

        // TODO make public
        internal HashSet<ElementConverterTarget> ElementConverterTargets { get; set; } = new HashSet<ElementConverterTarget>() {
            ElementConverterTarget.Heading,
            ElementConverterTarget.Paragraph,
            ElementConverterTarget.Linebreak,
            ElementConverterTarget.ListItem,
            ElementConverterTarget.HorizontalRule,
            ElementConverterTarget.Blockquote,
            ElementConverterTarget.Pre,
            ElementConverterTarget.Hyperlink,
            ElementConverterTarget.Image,
            ElementConverterTarget.Emphasis,
            ElementConverterTarget.InlineCode,
            ElementConverterTarget.RemoveTag,
            ElementConverterTarget.RemoveElement
        };

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
