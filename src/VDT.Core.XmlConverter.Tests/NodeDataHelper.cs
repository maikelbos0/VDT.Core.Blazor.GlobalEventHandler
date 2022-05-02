using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace VDT.Core.XmlConverter.Tests {
    public static class NodeDataHelper {
        public static NodeData Create(XmlNodeType nodeType, params ElementData[] ancestors)
            => Create(nodeType, ancestors, false, null);

        public static NodeData Create(
            XmlNodeType nodeType, 
            IList<ElementData>? ancestors = null, 
            bool isFirstChild = false,
            Dictionary<string, object?>? additionalData = null
        )
            => new NodeData(
                nodeType, 
                ancestors ?? Array.Empty<ElementData>(),
                isFirstChild,
                additionalData ?? new Dictionary<string, object?>()
            );
    }
}
