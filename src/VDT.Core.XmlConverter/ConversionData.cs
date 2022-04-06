using System.Collections.Generic;
using VDT.Core.XmlConverter.Elements;

namespace VDT.Core.XmlConverter {
    internal class ConversionData {
        internal Stack<ElementData> ElementAncestors { get; set; } = new Stack<ElementData>();
    }
}