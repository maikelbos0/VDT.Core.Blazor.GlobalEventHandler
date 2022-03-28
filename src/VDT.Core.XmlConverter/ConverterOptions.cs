using System.Collections.Generic;
using VDT.Core.XmlConverter.Elements;

namespace VDT.Core.XmlConverter {
    public class ConverterOptions {
        public List<IElementConverter> ElementConverters { get; set; } = new List<IElementConverter>();

        public IElementConverter DefaultElementConverter { get; set; } = new NoOpElementConverter();
    }
}