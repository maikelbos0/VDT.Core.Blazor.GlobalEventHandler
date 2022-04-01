using System.Collections.Generic;
using VDT.Core.XmlConverter.Elements;
using VDT.Core.XmlConverter.Nodes;

namespace VDT.Core.XmlConverter {
    public class ConverterOptions {
        public List<IElementConverter> ElementConverters { get; set; } = new List<IElementConverter>();

        public IElementConverter DefaultElementConverter { get; set; } = new NoOpElementConverter();

        public INodeConverter TextConverter { get; set; } = new FormattingNodeConverter((name, value) => value, true);

        public INodeConverter CDataConverter { get; set; } = new FormattingNodeConverter((name, value) => $"<![CDATA[{value}]]>", false);

        public INodeConverter CommentConverter { get; set; } = new FormattingNodeConverter((name, value) => $"<!--{value}-->", false);

        public INodeConverter XmlDeclarationConverter { get; set; } = new FormattingNodeConverter((name, value) => $"<?xml {value}?>", false);

        public INodeConverter WhitespaceConverter { get; set; } = new FormattingNodeConverter((name, value) => value, false);

        public INodeConverter SignificantWhitespaceConverter { get; set; } = new FormattingNodeConverter((name, value) => value, false);

        public INodeConverter DocumentTypeConverter { get; set; } = new FormattingNodeConverter((name, value) => $"<!DOCTYPE {name} [{value}]>", false);

        public INodeConverter ProcessingInstructionConverter { get; set; } = new FormattingNodeConverter((name, value) => $"<?{name} {value}?>", false);
    }
}