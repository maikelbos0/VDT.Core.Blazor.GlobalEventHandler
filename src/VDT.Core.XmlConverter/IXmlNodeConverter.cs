using System.IO;

namespace VDT.Core.XmlConverter {
    public interface IXmlNodeConverter {
        public bool IsValidFor(XmlNodeData nodeData);

        public void RenderStart(XmlNodeData nodeData, TextWriter writer);

        public bool ShouldRenderContent(XmlNodeData nodeData);

        public void RenderEnd(XmlNodeData nodeData, TextWriter writer);
    }
}
