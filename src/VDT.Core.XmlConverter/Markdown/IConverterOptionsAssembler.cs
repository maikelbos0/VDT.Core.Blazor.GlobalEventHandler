namespace VDT.Core.XmlConverter.Markdown {
    internal interface IConverterOptionsAssembler {
        public void SetNodeRemovingConverterForNonMarkdownNodeTypes(ConverterOptions options);

        public void AddHeaderElementConverters(ConverterOptions options);

        public void AddParagraphConverter(ConverterOptions options);

        public void AddListItemElementConverters(ConverterOptions options);

        public void AddHorizontalRuleConverter(ConverterOptions options);
    }
}
