using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace VDT.Core.XmlConverter {
    /// <summary>
    /// Options to use when calling <see cref="Converter.Convert(XmlReader, TextWriter)"/> or any of its overloads
    /// </summary>
    public class ConverterOptions {
        /// <summary>
        /// Converters that will be considered to use to convert <see cref="XmlNodeType.Element"/> nodes
        /// </summary>
        /// <remarks>
        /// Converters will be considered in order from first to last and used if <see cref="IElementConverter.IsValidFor(ElementData)"/> returns <see langword="true"/>
        /// </remarks>
        public List<IElementConverter> ElementConverters { get; set; } = new List<IElementConverter>();

        /// <summary>
        /// Used to convert <see cref="XmlNodeType.Element"/> nodes if no converter in <see cref="ElementConverters"/> is valid for the element
        /// </summary>
        public IElementConverter DefaultElementConverter { get; set; } = new NoOpElementConverter();

        /// <summary>
        /// Used to convert <see cref="XmlNodeType.Text"/> nodes
        /// </summary>
        public INodeConverter TextConverter { get; set; } = new FormattingNodeConverter((name, value) => value, true);
        
        /// <summary>
        /// Used to convert <see cref="XmlNodeType.CDATA"/> nodes
        /// </summary>
        public INodeConverter CDataConverter { get; set; } = new FormattingNodeConverter((name, value) => $"<![CDATA[{value}]]>", false);

        /// <summary>
        /// Used to convert <see cref="XmlNodeType.Comment"/> nodes
        /// </summary>
        public INodeConverter CommentConverter { get; set; } = new FormattingNodeConverter((name, value) => $"<!--{value}-->", false);

        /// <summary>
        /// Used to convert <see cref="XmlNodeType.XmlDeclaration"/> nodes
        /// </summary>
        public INodeConverter XmlDeclarationConverter { get; set; } = new FormattingNodeConverter((name, value) => $"<?xml {value}?>", false);

        /// <summary>
        /// Used to convert <see cref="XmlNodeType.Whitespace"/> nodes
        /// </summary>
        public INodeConverter WhitespaceConverter { get; set; } = new FormattingNodeConverter((name, value) => value, false);

        /// <summary>
        /// Used to convert <see cref="XmlNodeType.SignificantWhitespace"/> nodes
        /// </summary>
        public INodeConverter SignificantWhitespaceConverter { get; set; } = new FormattingNodeConverter((name, value) => value, false);

        /// <summary>
        /// Used to convert <see cref="XmlNodeType.DocumentType"/> nodes
        /// </summary>
        public INodeConverter DocumentTypeConverter { get; set; } = new FormattingNodeConverter((name, value) => $"<!DOCTYPE {name} [{value}]>", false);

        /// <summary>
        /// Used to convert <see cref="XmlNodeType.ProcessingInstruction"/> nodes
        /// </summary>
        public INodeConverter ProcessingInstructionConverter { get; set; } = new FormattingNodeConverter((name, value) => $"<?{name} {value}?>", false);
    }
}