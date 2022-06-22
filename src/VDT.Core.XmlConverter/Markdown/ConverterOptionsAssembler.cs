using System;
using System.Collections.Generic;
using System.Linq;

namespace VDT.Core.XmlConverter.Markdown {
    internal class ConverterOptionsAssembler : IConverterOptionsAssembler {
        private readonly static Dictionary<char, string> htmlCharacterEscapes = new Dictionary<char, string>() {
            { '<', "&lt;" },
            { '>', "&gt;" },
            { '&', "&amp;" }
        };

        private readonly static Dictionary<char, string> converterCharacterEscapes = new Dictionary<char, string>() {
            { '\\', "\\\\" },
            { '`', "\\`" },
            { '*', "\\*" },
            { '_', "\\_" },
            { '{', "\\{" }, // used for header id
            { '}', "\\}" }, // used for header id
            { '[', "\\[" },
            { ']', "\\]" },
            { '(', "\\(" },
            { ')', "\\)" },
            { '#', "\\#" },
            { '+', "\\+" },
            { '-', "\\-" },
            { '.', "\\." },
            { '!', "\\!" },
            { '~', "\\~" },
            { '=', "\\=" },
            { '^', "\\^" }
        };

        private readonly static Dictionary<ElementConverterTarget, char[]> elementConverterCharacters = new Dictionary<ElementConverterTarget, char[]>() {
            { ElementConverterTarget.Heading, new[] { '\\', '#' } },
            { ElementConverterTarget.Paragraph, Array.Empty<char>() },
            { ElementConverterTarget.Linebreak, Array.Empty<char>() },
            { ElementConverterTarget.ListItem, new[] { '\\', '*', '+', '-', '.' } },
            { ElementConverterTarget.HorizontalRule, new[] { '\\', '-' } },
            { ElementConverterTarget.Blockquote, Array.Empty<char>() },
            { ElementConverterTarget.Pre, new[] { '\\', '`' } },
            { ElementConverterTarget.Hyperlink, new[] { '\\', '[', ']', '(', ')' } },
            { ElementConverterTarget.Image, new[] { '\\', '[', ']', '(', ')', '!' } },
            { ElementConverterTarget.Important, new[] { '\\', '*', '_' } },
            { ElementConverterTarget.Emphasis, new[] { '\\', '*', '_' } },
            { ElementConverterTarget.InlineCode, new[] { '\\', '`' } },
            { ElementConverterTarget.Strikethrough, new[] { '\\', '~' } },
            { ElementConverterTarget.Highlight, new[] { '\\', '=' } },
            { ElementConverterTarget.Subscript, new[] { '\\', '~' } },
            { ElementConverterTarget.Superscript, new[] { '\\', '^' } },
            { ElementConverterTarget.RemoveTag, Array.Empty<char>() },
            { ElementConverterTarget.RemoveElement, Array.Empty<char>() }
        };
        
        public void SetTextConverter(ConverterOptions options, CharacterEscapeMode characterEscapeMode, HashSet<ElementConverterTarget> elementConverterTargets, Dictionary<char, string> customCharacterEscapes) {
            var characterEscapes = new Dictionary<char, string>();

            if (characterEscapeMode != CharacterEscapeMode.CustomOnly) {
                foreach (var escape in htmlCharacterEscapes) {
                    characterEscapes[escape.Key] = escape.Value;
                }
            }

            switch (characterEscapeMode) {
                case CharacterEscapeMode.Full:
                    foreach (var escape in converterCharacterEscapes) {
                        characterEscapes[escape.Key] = escape.Value;
                    }
                    break;

                case CharacterEscapeMode.ElementConverterBased:
                    foreach (var character in elementConverterTargets.SelectMany(t => elementConverterCharacters[t])) {
                        characterEscapes[character] = converterCharacterEscapes[character];
                    }                    
                    break;

                case CharacterEscapeMode.Custom:
                case CharacterEscapeMode.CustomOnly:
                    // Do nothing
                    break;

                default:
                    throw new NotImplementedException($"No implementation found for {nameof(CharacterEscapeMode)} '{characterEscapeMode}'");
            }

            foreach (var escape in customCharacterEscapes) {
                characterEscapes[escape.Key] = escape.Value;
            }

            options.TextConverter = new TextConverter(characterEscapes);
        }

        public void SetNodeConverterForNonMarkdownNodeTypes(ConverterOptions options) {
            options.CDataConverter = new NodeRemovingConverter();
            options.CommentConverter = new NodeRemovingConverter();
            options.DocumentTypeConverter = new NodeRemovingConverter();
            options.ProcessingInstructionConverter = new NodeRemovingConverter();
            options.XmlDeclarationConverter = new NodeRemovingConverter();
            options.SignificantWhitespaceConverter = new NodeRemovingConverter();
            options.WhitespaceConverter = new NodeRemovingConverter();
        }

        public void AddHeadingConverters(ConverterOptions options) {
            options.ElementConverters.Add(new BlockElementConverter("# ", "h1"));
            options.ElementConverters.Add(new BlockElementConverter("## ", "h2"));
            options.ElementConverters.Add(new BlockElementConverter("### ", "h3"));
            options.ElementConverters.Add(new BlockElementConverter("#### ", "h4"));
            options.ElementConverters.Add(new BlockElementConverter("##### ", "h5"));
            options.ElementConverters.Add(new BlockElementConverter("###### ", "h6"));
        }

        public void AddParagraphConverter(ConverterOptions options) {
            options.ElementConverters.Add(new ParagraphConverter());
        }

        public void AddLinebreakConverter(ConverterOptions options) {
            options.ElementConverters.Add(new LinebreakConverter());
        }

        public void AddListItemConverters(ConverterOptions options) {
            options.ElementConverters.Add(new OrderedListItemConverter());
            options.ElementConverters.Add(new UnorderedListItemConverter());
        }

        public void AddHorizontalRuleConverter(ConverterOptions options) {
            options.ElementConverters.Add(new BlockElementConverter("---", "hr"));
        }

        public void AddBlockquoteConverter(ConverterOptions options) {
            options.ElementConverters.Add(new BlockquoteConverter());
        }

        public void AddPreConverters(ConverterOptions options, PreConversionMode preConversionMode) {
            // Register pre content converter before any other element converters so it clears 
            options.ElementConverters.Insert(0, new PreContentConverter());

            options.ElementConverters.Add(preConversionMode switch {
                PreConversionMode.Indented => new IndentedPreConverter(),
                PreConversionMode.Fenced => new FencedPreConverter(),
                _ => throw new NotImplementedException($"No implementation found for {nameof(PreConversionMode)} '{preConversionMode}'")
            });
        }

        public void AddHyperlinkConverter(ConverterOptions options) {
            options.ElementConverters.Add(new HyperlinkConverter());
        }

        public void AddImageConverter(ConverterOptions options) {
            options.ElementConverters.Add(new ImageConverter());
        }

        public void AddImportantConverter(ConverterOptions options) {
            options.ElementConverters.Add(new InlineElementConverter("**", "**", "strong", "b"));
        }

        public void AddEmphasisConverter(ConverterOptions options) {
            options.ElementConverters.Add(new InlineElementConverter("*", "*", "em", "i"));
        }

        public void AddInlineCodeConverter(ConverterOptions options) {
            options.ElementConverters.Add(new InlineElementConverter("`", "`", "code", "kbd", "samp", "var"));
        }

        public void AddStrikethroughConverter(ConverterOptions options) {
            options.ElementConverters.Add(new InlineElementConverter("~~", "~~", "del"));
        }

        public void AddHighlightConverter(ConverterOptions options) {
            options.ElementConverters.Add(new InlineElementConverter("==", "==", "mark"));
        }

        public void AddSubscriptConverter(ConverterOptions options) {
            options.ElementConverters.Add(new InlineElementConverter("~", "~", "sub"));
        }

        public void AddSuperscriptConverter(ConverterOptions options) {
            options.ElementConverters.Add(new InlineElementConverter("^", "^", "sup"));
        }

        public void AddTagRemovingElementConverter(ConverterOptions options) {
            options.ElementConverters.Add(new TagRemovingElementConverter("html", "body", "ul", "ol", "menu", "div", "span"));
        }

        public void AddElementRemovingConverter(ConverterOptions options) {
            // TODO consider not removing some of these? Meta, frame, iframe, frameset?
            options.ElementConverters.Add(new ElementRemovingConverter("script", "style", "head", "frame", "meta", "iframe", "frameset"));
        }

        public void SetDefaultElementConverter(ConverterOptions options, UnknownElementHandlingMode unknownElementHandlingMode) {
            options.DefaultElementConverter = unknownElementHandlingMode switch {
                UnknownElementHandlingMode.None => new NoOpElementConverter(),
                UnknownElementHandlingMode.RemoveTags => new TagRemovingElementConverter(),
                UnknownElementHandlingMode.RemoveElements => new ElementRemovingConverter(),
                _ => throw new NotImplementedException($"No implementation found for {nameof(UnknownElementHandlingMode)} '{unknownElementHandlingMode}'")
            };
        }
    }
}
