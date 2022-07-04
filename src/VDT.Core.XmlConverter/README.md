# VDT.Core.XmlConverter

Converter for converting XML documents to other formats such as Markdown.

A new XmlConverter with default options converts each node and each element into a semantically identical version of itself; essentially it does nothing. To
convert nodes into other content, implement your own `INodeConverter` or `IElementConverter` and set it up using the `ConverterOptions` object passed when
creating your `XmlConverter`. This allows you to strip or replace specific XML nodes or XML elements with your own content.

## Features

- A converter to allow you to convert XML documents to any other text format
- Easily extensible options for converting different node types and elements exactly as desired
- Specific extensions for easily converting (X)HTML to Markdown

## INodeConverter for converting nodes of different types

Any node type (except for `XmlNodeType.Element` which has more detailed options, see below) that is supported by `XmlReader` can be converted by using this
converter. To convert a specific node type, change the converter for that specific type on the `ConverterOptions` object:

- `ConverterOptions.TextConverter` for text content
- `ConverterOptions.CDataConverter` for CDATA content
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
- `IsFirstChild`: true if this node is the first child of its parent; otherwise false
- `AdditionalData`: additional data that is shared by the entire conversion of an XML document and can be freely used by converters

Specifically worth mentioning is that AdditionalData will refer to the exact same dictionary across all node data and element data during a single conversion,
enabling you to share context between different conversion steps.

### Example

Suppose you have XML documents in which some comments need to be converted into text nodes depending on what parent element they have. You can create a custom
converter to turn comment nodes in certain elements into text.

```
public class CustomCommentConverter : INodeConverter {
    public void Convert(XmlReader reader, TextWriter writer, NodeData data) {
        if (data.Ancestors.FirstOrDefault().Name == "CommentData") {
            writer.Write(reader.Value.Trim());
        }
        else {
            writer.Write("<!--");
            writer.Write(reader.Value);
            writer.Write("-->");
        }
    }
}

var xml = @"<Data>
    <!-- This comment will be left as-is -->
    <CommentData><!-- This comment will be turned into a text node --></CommentData>
</Data>";

var converter = new Converter(new ConverterOptions() {
    CommentConverter = new CustomCommentConverter()
});

var result = converter.Convert(xml);

```

Above example will result in the following XML:

```
<Data>
    <!-- This comment will be left as-is -->
    <CommentData>This comment will be turned into a text node</CommentData>
</Data>
```

## IElementConverter for converting element nodes

Nodes of type `XmlNodeType.Element` can be converted with the help of implementations of `IElementConverter`. Element converters can be added to the list of
converters in `ConverterOptions.ElementConverters`. By default this list is empty and the `ConverterOptions.DefaultElementConverter` will be used to convert
all element nodes. Each converter in `ConverterOptions.ElementConverters` will be considered for use in order from first to last, using the 
`IElementConverter.IsValidFor` method to determine if a converter can be used. Once found, only this converter will be used for the current element.

The method `IElementConverter.IsValidFor` receives the parameter `ElementData` which contains the following information:

- `Name`: tag name of the element
- `Attributes`: collection of attributes found on the element
- `IsSelfClosing`: true if the element is an empty, self-closing element and false if the element has a separate opening and closing tag
- `Ancestors`: ancestor elements to the current node in order from lowest (direct parent) to highest (most far removed ancestor)
- `IsFirstChild`: true if this node is the first child of its parent; otherwise false
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

Suppose you have HTML documents where bold and italic text are achieved by inline CSS and you want to use proper semantic HTML tags such as `strong` and `em`.
You can create a custom converter that checks the style content of `span` tags and converts them as appropriate.

```
public class InlineStyleConverter : IElementConverter {
    public bool IsValidFor(ElementData elementData) => string.Equals("span", elementData.Name, System.StringComparison.OrdinalIgnoreCase);

    public void RenderStart(ElementData elementData, TextWriter writer) {
        if (IsBold(elementData)) {
            writer.Write("<strong>");
        }

        if (IsItalic(elementData)) {
            writer.Write("<em>");
        }
    }

    public bool ShouldRenderContent(ElementData elementData) => true;

    public void RenderEnd(ElementData elementData, TextWriter writer) {
        if (IsItalic(elementData)) {
            writer.Write("</em>");
        }

        if (IsBold(elementData)) {
            writer.Write("</strong>");
        }
    }

    private bool IsBold(ElementData elementData) => GetStyle(elementData)?.Contains("bold", StringComparison.OrdinalIgnoreCase) ?? false;

    private bool IsItalic(ElementData elementData) => GetStyle(elementData)?.Contains("italic", StringComparison.OrdinalIgnoreCase) ?? false;

    private string? GetStyle(ElementData elementData) {
        if (elementData.TryGetAttribute("style", out var style)) {
            return style;
        }

        return null;
    }
}

var xml = "<p>This paragraph converts <span style=\"font-style: italic\">italic</span> and <span style=\"font-weight: bold\">bold</span> spans to more appropriate tags.</p>";

var converterOptions = new ConverterOptions();
var converter = new Converter(converterOptions);

converterOptions.ElementConverters.Add(new InlineStyleConverter());

var result = converter.Convert(xml);
```

Above example will result in the following XML:

```
<p>This paragraph converts <em>italic</em> and <strong>bold</strong> spans to more appropriate tags.</p>
```

## Converting HTML to Markdown 

Methods to convert HTML to Markdown can be found in the `VDT.Core.XmlConverter.Markdown` namespace. Only converting HTML that is also valid XML is supported,
so if your documents are not well-formed XML an additional conversion is required first.

### Basic conversions

The extension method `ConverterOptionsExtensions.UseMarkdown` for the `ConverterOptions` class automatically adds a set of converters to convert HTML into a
Markdown formatted document.

`ConverterOptionsExtensions.UseMarkdown` adds support for converting the following elements to Markdown by default:

- `h1` through `h6`: headings 1 through 6
- `p`: paragraph
- `li` inside `ol` or `ul`: ordered or unordered list; supports nesting
- `a`: hyperlink with content and optional title
- `img`: image with optional alt text and title
- `strong` or `b`: bold
- `emp` or `i`: italic
- `blockquote`: blockquote; supports nesting
- `code`, `kbd`, `samp` or `var`: inline code
- `pre`: code block
- `hr`: horizontal rule
- `br`: linebreak

For the following elements only the content is rendered: `html`, `body`, `div` and `span`.

The following elements are removed entirely: `script`, `style`, `head`, `frame`, `meta`, `iframe` and `frameset`.

The optional parameter `useExtendedSyntax` can be used to add supported HTML to extended Markdown syntax converters:

- `del`: strikethrough
- `mark`: highlight
- `sub`: subscript
- `super`: superscript

Finally, the optional parameter `unknownElementHandlingMode` can be used to specify how to handle elements that can't be converted:
- `UnknownElementHandlingMode.None`: leave the elements as-is
- `UnknownElementHandlingMode.RemoveTags`: remove only the tags but render the child content of the elements
- `UnknownElementHandlingMode.RemoveElements`: remove the entire elements including child content

#### Example

```
var xml = @"
<h1>Header</h1>

<p>This is an example document. It will get converted to Markdown.</p>

<ol>
	<li>Here is a list item</li>
	<li>And another <strong>very important</strong> one</li>
</ol>
";
var options = new ConverterOptions().UseMarkdown();
var converter = new Converter(options);

var markdown = converter.Convert(xml);
```

Above example will result in the following Markdown:

```
 
\# Header

This is an example document\. It will get converted to Markdown\.

1\. Here is a list item
1\. And another \*\*very important\*\* one

```

### Customized conversions

If you need fine-grained control over how your HTML is converted to Markdown, use the `ConverterOptionsBuilder` class. It supports the following
customizations:

- `ElementConverterTargets` and its builder methods can be used to specify which HTML elements to convert
- `TagsToRemove` and its builder methods can be used to specify for which elements only content is rendered
- `ElementsToRemove` and its builder methods can be used to specify which elements should not be converted at all
- `PreConversionMode` and its builder methods can be used to specify how to render &lt;pre&gt; elements
- `UnknownElementHandlingMode` and its builder methods can be used to specify how to handle elements that can't be converted
- `CharacterEscapeMode` and `CustomCharacterEscapes` and their builder methods can be used to specify which characters to escape

#### Example

```
var xml = @"
<h1>Header</h1>

<p>This is an example document. It will get converted to Markdown.</p>

<pre>
function SomeCodeHere() {
}
</pre>

<p>Here we have more text.</p>

<ol>
	<li>Here is a list item</li>
	<li>And another <strong>very important</strong> one</li>
</ol>
";
var options = new ConverterOptionsBuilder()
    .RemoveElementConverters(ElementConverterTarget.Important)
    .UsePreConversionMode(PreConversionMode.Indented)
    .Build();
var converter = new Converter(options);

var markdown = converter.Convert(xml);
```

Above example will result in the following Markdown:

```
    
# Header

This is an example document\. It will get converted to Markdown\.

	
	function SomeCodeHere() {
	}
	
Here we have more text\.

1. Here is a list item
1. And another <strong>very important</strong> one

```
