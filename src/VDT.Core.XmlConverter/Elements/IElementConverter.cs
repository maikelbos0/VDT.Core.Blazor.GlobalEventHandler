using System.IO;

namespace VDT.Core.XmlConverter.Elements {
    public interface IElementConverter {
        public bool IsValidFor(ElementData elementData);

        public void RenderStart(ElementData elementData, TextWriter writer);

        public bool ShouldRenderContent(ElementData elementData);

        public void RenderEnd(ElementData elementData, TextWriter writer);
    }
}
