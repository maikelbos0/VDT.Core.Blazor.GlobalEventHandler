using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VDT.Core.XmlConverter {
    public class XmlNodeData {
        public string Name { get; }
        public IReadOnlyDictionary<string, string> Attributes { get; }

        internal XmlNodeData(string name, Dictionary<string, string> attributes) {
            Name = name;
            Attributes = new ReadOnlyDictionary<string, string>(attributes);
        }
    }
}
