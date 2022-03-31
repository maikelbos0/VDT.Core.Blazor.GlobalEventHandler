using System.IO;
using System.Linq;
using System.Xml;
using VDT.Core.XmlConverter.Elements;

namespace VDT.Core.XmlConverter {
    public class Converter {
        public ConverterOptions Options { get; }

        public Converter() : this(new ConverterOptions()) {
        }

        public Converter(ConverterOptions options) {
            Options = options;
        }

        // TODO finish and bring under test
        public void Convert(XmlReader reader, TextWriter writer) {
            if (reader.NodeType == XmlNodeType.None) {
                reader.Read();
            }

            do {
                ConvertNode(reader, writer);
            } while (reader.Read());
        }

        // TODO finish and bring under test
        internal void ConvertNode(XmlReader reader, TextWriter writer) {
            switch (reader.NodeType) {
                case XmlNodeType.Element:
                    ConvertElement(reader, writer);
                    break;
                case XmlNodeType.Text:
                    Options.TextConverter.Convert(reader, writer);
                    break;
                case XmlNodeType.CDATA:
                    Options.CDataConverter.Convert(reader, writer);
                    break;
                case XmlNodeType.EndElement:
                case XmlNodeType.Attribute:
                    throw new UnexpectedNodeTypeException($"Node type '{reader.NodeType}' was not handled by {nameof(ConvertElement)}; ensure {nameof(reader)} is in correct position before calling {nameof(Convert)}", reader.NodeType);                
                case XmlNodeType.Comment:
                case XmlNodeType.Document:
                case XmlNodeType.DocumentFragment:
                case XmlNodeType.DocumentType:
                case XmlNodeType.EndEntity:
                case XmlNodeType.Entity:
                case XmlNodeType.EntityReference:
                case XmlNodeType.Notation:
                case XmlNodeType.ProcessingInstruction:
                case XmlNodeType.SignificantWhitespace:
                case XmlNodeType.Whitespace:
                case XmlNodeType.XmlDeclaration:
                default:
                    throw new UnexpectedNodeTypeException($"No node converter was specified for node type '{reader.NodeType}'", reader.NodeType);
            }
        }

        internal void ConvertElement(XmlReader reader, TextWriter writer) {
            var depth = reader.Depth;
            var elementData = new ElementData(
                reader.Name,
                reader.GetAttributes(),
                reader.IsEmptyElement
            );
            var elementConverter = Options.ElementConverters.FirstOrDefault(c => c.IsValidFor(elementData)) ?? Options.DefaultElementConverter;
            var shouldRenderContent = elementConverter.ShouldRenderContent(elementData);

            elementConverter.RenderStart(elementData, writer);

            if (!reader.IsEmptyElement) {
                while (reader.Read() && reader.Depth > depth) {
                    if (shouldRenderContent) {
                        ConvertNode(reader, writer);
                    }
                }
            }

            elementConverter.RenderEnd(elementData, writer);
        }
    }
}