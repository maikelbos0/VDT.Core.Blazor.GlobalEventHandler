using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VDT.Core.XmlConverter.Elements {
    public class BasicElementConverter : IElementConverter {
        private readonly HashSet<string> validForElementNames;
        private readonly string startOutput;
        private readonly string endOutput;

        public BasicElementConverter(string startOutput, string endOutput, params string[] validForElementNames) : this(startOutput, endOutput, validForElementNames.AsEnumerable()) {
        }

        public BasicElementConverter(string startOutput, string endOutput, IEnumerable<string> validForElementNames) {
            this.validForElementNames = new HashSet<string>(validForElementNames);
            this.startOutput = startOutput;
            this.endOutput = endOutput;
        }

        public bool IsValidFor(ElementData elementData) => validForElementNames.Contains(elementData.Name);

        public void RenderStart(ElementData elementData, TextWriter writer) => writer.Write(startOutput);

        public bool ShouldRenderContent(ElementData elementData) => true;

        public void RenderEnd(ElementData elementData, TextWriter writer) => writer.Write(endOutput);
    }
}
