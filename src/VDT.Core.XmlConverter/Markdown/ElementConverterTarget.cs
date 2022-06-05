namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Targets for converting HTML elements to Markdown
    /// </summary>
    public enum ElementConverterTarget {
        /// <summary>
        /// Heading tags &lt;h1&gt; through &lt;h6&gt;
        /// </summary>
        Heading,

        /// <summary>
        /// Paragraph tag &lt;p&gt;
        /// </summary>
        Paragraph,

        /// <summary>
        /// Linebreak tag &lt;br/&gt;
        /// </summary>
        Linebreak,

        /// <summary>
        /// Ordered and unordered list item tag &lt;li&gt;
        /// </summary>
        ListItem,

        /// <summary>
        /// Horizontal rule tag &lt;hr/&gt;
        /// </summary>
        HorizontalRule,

        /// <summary>
        /// Blockquote tag &lt;blockquote&gt;
        /// </summary>
        Blockquote,

        /// <summary>
        /// Pre tag &lt;pre&gt;
        /// </summary>
        Pre,

        /// <summary>
        /// Hyperlink tag &lt;a&gt;
        /// </summary>
        Hyperlink,

        /// <summary>
        /// Image tag &lt;img/&gt;
        /// </summary>
        Image,

        /// <summary>
        /// Important tags &lt;strong&gt; and &lt;b&gt;
        /// </summary>
        Important,

        /// <summary>
        /// Emphasis tags &lt;em&gt; and &lt;i&gt;
        /// </summary>
        Emphasis,

        /// <summary>
        /// Inline code tags &lt;code&gt;, &lt;kbd&gt;, &lt;samp&gt; and &lt;var&gt;
        /// </summary>
        InlineCode,

        /// <summary>
        /// Strikethrough tag &lt;del&gt;
        /// </summary>
        Strikethrough,

        /// <summary>
        /// Highlight tag &lt;mark&gt;
        /// </summary>
        Highlight,

        /// <summary>
        /// Subscript tag &lt;sub&gt;
        /// </summary>
        Subscript,

        /// <summary>
        /// Superscript tag &lt;sup&gt;
        /// </summary>
        Superscript,

        /// <summary>
        /// Remove non-semantic tags surrounding content
        /// </summary>
        RemoveTag,

        /// <summary>
        /// Remove elements that are should not be rendered in Markdown
        /// </summary>
        RemoveElement
    }
}
