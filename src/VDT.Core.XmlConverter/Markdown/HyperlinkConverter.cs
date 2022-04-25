using System;
using System.IO;
using System.Linq;

namespace VDT.Core.XmlConverter.Markdown {
    internal class HyperlinkConverter : BaseElementConverter {
        public HyperlinkConverter() : base("a") { }

        public override void RenderStart(ElementData elementData, TextWriter writer)
            => elementData.GetContentTracker().Write(writer, "[");

        public override void RenderEnd(ElementData elementData, TextWriter writer) {
            var tracker = elementData.GetContentTracker();
            
            tracker.Write(writer, "](");

            var attributeName = elementData.Attributes.Keys.FirstOrDefault(k => string.Equals(k, "href", StringComparison.OrdinalIgnoreCase));

            if (attributeName != null) {
                tracker.Write(writer, elementData.Attributes[attributeName]);
            }

            tracker.Write(writer, ")");
        }
    }
}
