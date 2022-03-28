using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VDT.Core.XmlConverter.Elements {
    public class ElementData {
        public string Name { get; }
        public IReadOnlyDictionary<string, string> Attributes { get; }
        public bool IsSelfClosing { get; }

        internal ElementData(string name, Dictionary<string, string> attributes, bool isSelfClosing) {
            Name = name;
            Attributes = new ReadOnlyDictionary<string, string>(attributes);
            IsSelfClosing = isSelfClosing;
        }
    }
}
