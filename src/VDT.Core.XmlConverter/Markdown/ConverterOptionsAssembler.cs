namespace VDT.Core.XmlConverter.Markdown {
    internal class ConverterOptionsAssembler : IConverterOptionsAssembler {
        public void SetNodeRemovingConverterForNonMarkdownNodeTypes(ConverterOptions options) {
            options.CDataConverter = new NodeRemovingConverter();
            options.CommentConverter = new NodeRemovingConverter();
            options.DocumentTypeConverter = new NodeRemovingConverter();
            options.ProcessingInstructionConverter = new NodeRemovingConverter();
            options.XmlDeclarationConverter = new NodeRemovingConverter();
            options.SignificantWhitespaceConverter = new NodeRemovingConverter();
            options.WhitespaceConverter = new NodeRemovingConverter();
        }

        public void AddHeaderElementConverters(ConverterOptions options) {
            options.ElementConverters.Add(new BlockElementConverter("# ", "h1"));
            options.ElementConverters.Add(new BlockElementConverter("## ", "h2"));
            options.ElementConverters.Add(new BlockElementConverter("### ", "h3"));
            options.ElementConverters.Add(new BlockElementConverter("#### ", "h4"));
            options.ElementConverters.Add(new BlockElementConverter("##### ", "h5"));
            options.ElementConverters.Add(new BlockElementConverter("###### ", "h6"));
        }
    }
}
