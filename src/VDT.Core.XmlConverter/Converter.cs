using System.IO;
using System.Linq;
using System.Text;
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

        public string Convert(string xml) {
            using var writer = new StringWriter();

            Convert(xml, writer);

            return writer.ToString();
        }

        public void Convert(string xml, TextWriter writer) {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));

            Convert(stream, writer);
        }

        public string Convert(Stream stream) {
            using var writer = new StringWriter();

            Convert(stream, writer);

            return writer.ToString();
        }

        public void Convert(Stream stream, TextWriter writer) {
            using var reader = XmlReader.Create(stream);

            Convert(reader, writer);
        }

        public string Convert(XmlReader reader) {
            using var writer = new StringWriter();

            Convert(reader, writer);

            return writer.ToString();
        }

        public void Convert(XmlReader reader, TextWriter writer) {
            if (reader.NodeType == XmlNodeType.None) {
                reader.Read();
            }

            do {
                ConvertNode(reader, writer);
            } while (reader.Read());
        }

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
                case XmlNodeType.Comment:
                    Options.CommentConverter.Convert(reader, writer);
                    break;
                case XmlNodeType.XmlDeclaration:
                    Options.XmlDeclarationConverter.Convert(reader, writer);
                    break;
                case XmlNodeType.Whitespace:
                    Options.WhitespaceConverter.Convert(reader, writer);
                    break;
                case XmlNodeType.SignificantWhitespace:
                    Options.SignificantWhitespaceConverter.Convert(reader, writer);
                    break;
                case XmlNodeType.DocumentType:
                    Options.DocumentTypeConverter.Convert(reader, writer);
                    break;
                case XmlNodeType.ProcessingInstruction:
                    Options.ProcessingInstructionConverter.Convert(reader, writer);
                    break;
                case XmlNodeType.EndElement:
                case XmlNodeType.Attribute:
                    throw new UnexpectedNodeTypeException($"Node type '{reader.NodeType}' was not handled by {nameof(ConvertElement)}; ensure {nameof(reader)} is in correct position before calling {nameof(Convert)}", reader.NodeType);
                case XmlNodeType.Document:
                case XmlNodeType.DocumentFragment:
                case XmlNodeType.EndEntity:
                case XmlNodeType.Entity:
                case XmlNodeType.EntityReference:
                case XmlNodeType.Notation:
                default:
                    throw new UnexpectedNodeTypeException($"Node type '{reader.NodeType}' is currently not supported", reader.NodeType);
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