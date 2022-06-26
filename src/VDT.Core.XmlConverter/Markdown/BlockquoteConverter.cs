using System.IO;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for rendering elements as block quotations in Markdown
    /// </summary>
    public class BlockquoteConverter : BlockElementConverter {
        /// <summary>
        /// Construct an instance of a Markdown blockquote converter
        /// </summary>
        public BlockquoteConverter() : base("", "blockquote") { }
                
        /// <inheritdoc/>
        public override void RenderStart(ElementData elementData, TextWriter writer) {
            base.RenderStart(elementData, writer);
            elementData.GetContentTracker().Prefixes.Push("> ");
        }

        /// <inheritdoc/>
        override public void RenderEnd(ElementData elementData, TextWriter writer) {
            var tracker = elementData.GetContentTracker();
            
            tracker.Prefixes.Pop();
            tracker.WriteLine(writer);
        }
    }
}
