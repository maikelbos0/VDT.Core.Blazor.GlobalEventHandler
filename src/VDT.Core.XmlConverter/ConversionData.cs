using System.Collections.Generic;

namespace VDT.Core.XmlConverter {
    internal class ConversionData {
        internal Stack<ElementData> ElementAncestors { get; set; } = new Stack<ElementData>();
    }
}