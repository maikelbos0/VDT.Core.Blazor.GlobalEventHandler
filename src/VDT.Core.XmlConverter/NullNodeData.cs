using System;
using System.Collections.Generic;

namespace VDT.Core.XmlConverter {
    internal class NullNodeData : INodeData {
        public IReadOnlyList<ElementData> Ancestors => throw new NotImplementedException();

        public bool IsFirstChild => throw new NotImplementedException();

        public Dictionary<string, object?> AdditionalData => throw new NotImplementedException();
    }
}
