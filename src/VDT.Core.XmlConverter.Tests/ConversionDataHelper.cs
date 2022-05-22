namespace VDT.Core.XmlConverter.Tests {
    internal static class ConversionDataHelper {
        internal static ConversionData Create(ElementData currentElementData)
            => new ConversionData() {
                CurrentNodeData = currentElementData
            };
    }
}
