namespace VDT.Core.XmlConverter.Markdown {
    internal interface IConverterOptionsAssembler {
        public void SetTextConverter(ConverterOptions options);

        public void SetNodeConverterForNonMarkdownNodeTypes(ConverterOptions options);

        public void AddHeadingConverters(ConverterOptions options);

        public void AddParagraphConverter(ConverterOptions options);

        public void AddLinebreakConverter(ConverterOptions options);

        public void AddListItemConverters(ConverterOptions options);

        public void AddHorizontalRuleConverter(ConverterOptions options);

        public void AddBlockquoteConverter(ConverterOptions options);

        public void AddPreConverters(ConverterOptions options);

        public void AddHyperlinkConverter(ConverterOptions options);

        public void AddImageConverter(ConverterOptions options);

        public void AddEmphasisConverters(ConverterOptions options);

        public void AddInlineCodeConverter(ConverterOptions options);

        public void AddStrikethroughConverter(ConverterOptions options);

        public void AddHighlightConverter(ConverterOptions options);

        public void AddTagRemovingElementConverter(ConverterOptions options);

        public void AddElementRemovingConverter(ConverterOptions options);

        public void SetDefaultElementConverter(ConverterOptions options, UnknownElementHandlingMode unknownElementHandlingMode);
    }
}
