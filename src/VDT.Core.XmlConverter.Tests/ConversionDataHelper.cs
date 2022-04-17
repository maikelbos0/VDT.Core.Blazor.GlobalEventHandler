using System;
using System.Xml;

namespace VDT.Core.XmlConverter.Tests {
    public static class ConversionDataHelper {
        public static ConversionData Create(ElementData currentElementData)
            => new ConversionData() {
                CurrentNodeType = XmlNodeType.Element,
                CurrentElementData = currentElementData
            };
    }
}
