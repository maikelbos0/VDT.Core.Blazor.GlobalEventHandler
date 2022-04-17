namespace VDT.Core.XmlConverter.Tests {
    public static class ConversionDataHelper {
        public static ConversionData Create(ElementData currentElementData)
            => new ConversionData() {
                CurrentElementData = currentElementData
            };
    }
}
