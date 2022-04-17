using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace VDT.Core.XmlConverter {
    /// <summary>
    /// Allows converting xml documents into other text-based document formats
    /// </summary>
    public class Converter {
        /// <summary>
        /// Options to use when calling <see cref="Convert(XmlReader, TextWriter)"/> or any of its overloads
        /// </summary>
        public ConverterOptions Options { get; set; }

        /// <summary>
        /// Construct an instance of a converter
        /// </summary>
        public Converter() : this(new ConverterOptions()) {
        }

        /// <summary>
        /// Construct an instance of a converter with the provided <see cref="ConverterOptions"/>
        /// </summary>
        /// <param name="options">Options to use when calling <see cref="Convert(XmlReader, TextWriter)"/> or any of its overloads</param>
        public Converter(ConverterOptions options) {
            Options = options;
        }

        /// <summary>
        /// Convert an xml document string using the provided <see cref="ConverterOptions"/>
        /// </summary>
        /// <param name="xml">Xml document to convert</param>
        /// <returns>Converted document</returns>
        public string Convert(string xml) {
            using var writer = new StringWriter();

            Convert(xml, writer);

            return writer.ToString();
        }

        /// <summary>
        /// Convert an xml document string using the provided <see cref="ConverterOptions"/>
        /// </summary>
        /// <param name="xml">Xml document to convert</param>
        /// <param name="writer">Converted document is written to this <see cref="TextWriter"/></param>
        public void Convert(string xml, TextWriter writer) {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));

            Convert(stream, writer);
        }

        /// <summary>
        /// Convert a stream containing an xml document using the provided <see cref="ConverterOptions"/>
        /// </summary>
        /// <param name="stream">Stream containing the xml document to convert</param>
        /// <returns>Converted document</returns>
        public string Convert(Stream stream) {
            using var writer = new StringWriter();

            Convert(stream, writer);

            return writer.ToString();
        }

        /// <summary>
        /// Convert a stream containing an xml document using the provided <see cref="ConverterOptions"/>
        /// </summary>
        /// <param name="stream">Stream containing the xml document to convert</param>
        /// <param name="writer">Converted document is written to this <see cref="TextWriter"/></param>
        public void Convert(Stream stream, TextWriter writer) {
            using var reader = XmlReader.Create(stream, new XmlReaderSettings() {
                ConformanceLevel = ConformanceLevel.Fragment
            });

            Convert(reader, writer);
        }

        /// <summary>
        /// Convert an xml document using the provided <see cref="ConverterOptions"/>
        /// </summary>
        /// <param name="reader">Xml document to convert</param>
        /// <returns>Converted document</returns>
        public string Convert(XmlReader reader) {
            using var writer = new StringWriter();

            Convert(reader, writer);

            return writer.ToString();
        }

        /// <summary>
        /// Convert an xml document using the provided <see cref="ConverterOptions"/>
        /// </summary>
        /// <param name="reader">Xml document to convert</param>
        /// <param name="writer">Converted document is written to this <see cref="TextWriter"/></param>
        public void Convert(XmlReader reader, TextWriter writer) {
            var data = new ConversionData();

            if (reader.NodeType == XmlNodeType.None) {
                reader.Read();
            }

            do {
                Convert(reader, writer, data);
            } while (reader.Read());
        }

        // TODO test and rename
        internal void Convert(XmlReader reader, TextWriter writer, ConversionData data) {
            data.ReadNode(reader);

            if (data.CurrentNodeData != null) {
                ConvertNode(reader, writer, data.CurrentNodeData);
            }
            else if (data.CurrentElementData != null) {
                ConvertElement(reader, writer, data, data.CurrentElementData);
            }
            else {
                throw new InvalidOperationException($"Unexpectedly found both {data.CurrentNodeData} and {data.CurrentElementData} to be null");
            }
        }

        internal void ConvertNode(XmlReader reader, TextWriter writer, NodeData nodeData) {
            switch (nodeData.NodeType) {
                case XmlNodeType.Text:
                    Options.TextConverter.Convert(reader, writer, nodeData);
                    break;
                case XmlNodeType.CDATA:
                    Options.CDataConverter.Convert(reader, writer, nodeData);
                    break;
                case XmlNodeType.Comment:
                    Options.CommentConverter.Convert(reader, writer, nodeData);
                    break;
                case XmlNodeType.XmlDeclaration:
                    Options.XmlDeclarationConverter.Convert(reader, writer, nodeData);
                    break;
                case XmlNodeType.Whitespace:
                    Options.WhitespaceConverter.Convert(reader, writer, nodeData);
                    break;
                case XmlNodeType.SignificantWhitespace:
                    Options.SignificantWhitespaceConverter.Convert(reader, writer, nodeData);
                    break;
                case XmlNodeType.DocumentType:
                    Options.DocumentTypeConverter.Convert(reader, writer, nodeData);
                    break;
                case XmlNodeType.ProcessingInstruction:
                    Options.ProcessingInstructionConverter.Convert(reader, writer, nodeData);
                    break;
                case XmlNodeType.EndElement:
                case XmlNodeType.Attribute:
                    throw new UnexpectedNodeTypeException($"Node type '{nodeData.NodeType}' was not handled by {nameof(ConvertElement)}; ensure {nameof(reader)} is in correct position before calling {nameof(Convert)}", nodeData.NodeType);
                case XmlNodeType.Document:
                case XmlNodeType.DocumentFragment:
                case XmlNodeType.EndEntity:
                case XmlNodeType.Entity:
                case XmlNodeType.EntityReference:
                case XmlNodeType.Notation:
                default:
                    throw new UnexpectedNodeTypeException($"Node type '{nodeData.NodeType}' is currently not supported", nodeData.NodeType);
            }
        }

        internal void ConvertElement(XmlReader reader, TextWriter writer, ConversionData data, ElementData elementData) {
            var depth = reader.Depth;
            var elementConverter = Options.ElementConverters.FirstOrDefault(c => c.IsValidFor(elementData)) ?? Options.DefaultElementConverter;
            var shouldRenderContent = elementConverter.ShouldRenderContent(elementData);

            elementConverter.RenderStart(elementData, writer);

            if (!elementData.IsSelfClosing) {
                while (reader.Read() && reader.Depth > depth) {
                    if (shouldRenderContent) {
                        Convert(reader, writer, data);
                    }
                }
            }

            elementConverter.RenderEnd(elementData, writer);
        }
    }
}