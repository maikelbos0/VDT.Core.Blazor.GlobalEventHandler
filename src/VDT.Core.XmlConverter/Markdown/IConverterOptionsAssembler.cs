using System.Collections.Generic;

namespace VDT.Core.XmlConverter.Markdown {
    internal interface IConverterOptionsAssembler {
        public void SetTextConverter(ConverterOptions options, CharacterEscapeMode characterEscapeMode, HashSet<ElementConverterTarget> elementConverterTargets, Dictionary<char, string> customCharacterEscapes);

        public void SetNodeConverterForNonMarkdownNodeTypes(ConverterOptions options);

        public void AddHeadingConverters(ConverterOptions options);

        public void AddParagraphConverter(ConverterOptions options);

        public void AddLinebreakConverter(ConverterOptions options);

        public void AddListItemConverters(ConverterOptions options);

        public void AddHorizontalRuleConverter(ConverterOptions options);

        public void AddBlockquoteConverter(ConverterOptions options);

        public void AddPreConverters(ConverterOptions options, PreConversionMode preConversionMode);

        public void AddHyperlinkConverter(ConverterOptions options);

        public void AddImageConverter(ConverterOptions options);

        public void AddImportantConverter(ConverterOptions options);

        public void AddEmphasisConverter(ConverterOptions options);

        public void AddInlineCodeConverter(ConverterOptions options);

        public void AddStrikethroughConverter(ConverterOptions options);

        public void AddHighlightConverter(ConverterOptions options);

        public void AddSubscriptConverter(ConverterOptions options);

        public void AddSuperscriptConverter(ConverterOptions options);

        public void AddTagRemovingElementConverter(ConverterOptions options, HashSet<string> tagsToRemove);

        public void AddElementRemovingConverter(ConverterOptions options, HashSet<string> elementsToRemove);

        public void SetDefaultElementConverter(ConverterOptions options, UnknownElementHandlingMode unknownElementHandlingMode);

        public void AddDefinitionListConverters(ConverterOptions options);
    }
}
