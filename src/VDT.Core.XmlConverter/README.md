# VDT.Core.XmlConverter

Converter for converting XML documents to other formats such as Markdown

## Features

- A converter to allow you to convert XML documents to any other text format
- Easily extensible options for converting different node types and elements exactly as desired
- Specific extensions for easily converting (X)HTML to basic Markdown

## XmlConverter basics

A new XmlConverter with default options converts each node and each element into a semantically identical version of itself. Essentially it does nothing. To
convert nodes into other content, implement your own `INodeConverter` or `IElementConverter` and set it up using the `ConverterOptions` object passed when
creating your `XmlConverter`. This allows you to strip or replace specific XML nodes or XML elements with your own content.

## INodeConverter

Each node type (except for `XmlNodeType.Element` which has more detailed options, see below) that is supported by `XmlReader` can be converted by using this
converter. To convert only a specific node type, change the converter for that specific type on the `ConverterOptions` object:

- `ConverterOptions.TextConverter` for node text content
- `ConverterOptions.CDataConverter` for CDATA
- `ConverterOptions.CommentConverter` for comments
- `ConverterOptions.XmlDeclarationConverter` for the XML declaration
- `ConverterOptions.WhitespaceConverter` for insignificant whitespace
- `ConverterOptions.SignificantWhitespaceConverter` for significant whitespace
- `ConverterOptions.DocumentTypeConverter` for document type declarations
- `ConverterOptions.ProcessingInstructionConverter` for XML processing instructions

Implementations of `INodeConverter` must implement the `INodeConverter.Convert` method which writes the converted content to a `TextWriter`. This method
receives three parameters:
- `reader`: XML reader for which to convert the current node; it is at the position of the node which needs converting
- `writer`: text writer to write the resulting output to
- `data`: data relating to the current node

The `NodeData` object contains the following information:
- `NodeType`: type of the node
- `Ancestors`: ancestor elements to the current node in order from lowest (direct parent) to highest (most far removed ancestor)
- `IsFirstChild`: true if this node is the first child of its parent, otherwise false
- `AdditionalData`: additional data that is shared by the entire conversion of an XML document and can be freely used by converters

Specifically worth mentioning is that AdditionalData will refer to the exact same dictionary across all node data and element data during a single conversion,
enabling you to share context between different conversion steps.

### Example

```

```

## IElementConverter

Nodes of type `XmlNodeType.Element` can be converted with the help of implementations of `IElementConverter`. Element converters can be added to the list of
converters in `ConverterOptions.ElementConverters`. By default this list is empty and the `ConverterOptions.DefaultElementConverter` will be used to convert
all element nodes. If any element converters are present in `ConverterOptions.ElementConverters` then each converter will be considered for use in order from
first to last, using the `IElementConverter.IsValidFor` method to determine if a converter can be used. Once found, only this converter will be used for the
current element.

The method `IElementConverter.IsValidFor` receives the parameter `ElementData` which contains the following information:

- `Name`: tag name of the element
- `Attributes`: collection of attributes found on the element
- `IsSelfClosing`: true if the element is an empty, self-closing element and false if the element has a separate opening and closing tag
- `Ancestors`: ancestor elements to the current node in order from lowest (direct parent) to highest (most far removed ancestor)
- `IsFirstChild`: true if this node is the first child of its parent, otherwise false
- `AdditionalData`: additional data that is shared by the entire conversion of an XML document and can be freely used by converters

Specifically worth mentioning is that AdditionalData will refer to the exact same dictionary across all node data and element data during a single conversion,
enabling you to share context between different conversion steps.

Implementations of `IElementData` must also implement the following methods to convert element nodes:

- `RenderStart` renders output at the start of the element, before any possible child content is rendered; it receives two parameters:
	- `elementData`: Information about the element currently being converted
	- `writer`: text writer to write the resulting output to
- `ShouldRenderContent` determines if the child nodes of the current element should be rendered; it receives one parameter:
	- `elementData`: Information about the element currently being converted
- `RenderEnd` renders output at the end of the element, after any possible child content is rendered; it receives two parameters:
	- `elementData`: Information about the element currently being converted
	- `writer`: text writer to write the resulting output to

### Example

```

```

## Extensibility

TODO

### Converting nodes of different types

TODO

#### Example

```
TODO
```

### Converting elements with different names and attributes

TODO

#### Example

```
TODO
```

## Markdown extensions

TODO

### Example

```
TODO
`` 
