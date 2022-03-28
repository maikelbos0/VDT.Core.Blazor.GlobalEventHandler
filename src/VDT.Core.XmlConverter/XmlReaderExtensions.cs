using System.Collections.Generic;
using System.Xml;

namespace VDT.Core.XmlConverter {
    internal static class XmlReaderExtensions {
        internal static Dictionary<string, string> GetAttributes(this XmlReader reader) {            
            var attributes = new Dictionary<string, string>();

            for (var i = 0; i < reader.AttributeCount; i++) {
                reader.MoveToAttribute(i);
                attributes.Add(reader.Name, reader.Value);
            }

            reader.MoveToElement();

            return attributes;
        }
    }
}
