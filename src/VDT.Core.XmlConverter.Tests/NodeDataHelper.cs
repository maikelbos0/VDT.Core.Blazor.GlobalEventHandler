using System;
using System.Collections.Generic;
using System.Xml;

namespace VDT.Core.XmlConverter.Tests {
    public static class NodeDataHelper {
        public static NodeData Create(XmlNodeType nodeType, IList<ElementData>? ancestors = null, Dictionary<string, object?>? additionalData = null)
            => new NodeData(
                nodeType, 
                ancestors ?? Array.Empty<ElementData>(),
                additionalData ?? new Dictionary<string, object?>()
            );
    }
}
