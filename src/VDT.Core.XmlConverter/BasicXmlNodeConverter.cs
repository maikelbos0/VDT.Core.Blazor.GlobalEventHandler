using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VDT.Core.XmlConverter {
    public class BasicXmlNodeConverter : IXmlNodeConverter {
        private readonly HashSet<string> validForNodeNames;
        private readonly string startOutput;
        private readonly string endOutput;

        public BasicXmlNodeConverter(string startOutput, string endOutput, params string[] validForNodeNames) : this(startOutput, endOutput, validForNodeNames.AsEnumerable()) {
        }

        public BasicXmlNodeConverter(string startOutput, string endOutput, IEnumerable<string> validForNodeNames) {
            this.validForNodeNames = new HashSet<string>(validForNodeNames);
            this.startOutput = startOutput;
            this.endOutput = endOutput;
        }

        public bool IsValidFor(XmlNodeData nodeData) => validForNodeNames.Contains(nodeData.Name);

        public void RenderStart(XmlNodeData nodeData, TextWriter writer) => writer.Write(startOutput);

        public bool ShouldRenderContent(XmlNodeData nodeData) => true;

        public void RenderEnd(XmlNodeData nodeData, TextWriter writer) => writer.Write(endOutput);
    }
}
