using System;
using System.Collections.Generic;
using System.Xml;

namespace VDT.Core.XmlConverter.Tests {
    public static class NodeDataHelper {
        public static NodeData Create(
            XmlNodeType nodeType,
            string? name = null,
            string? value = null,
            IList<ElementData>? ancestors = null,
            bool isFirstChild = false,
            Dictionary<string, object?>? additionalData = null
        )
            => new NodeData(
                nodeType,
                name ?? "",
                value ?? "",
                ancestors ?? Array.Empty<ElementData>(),
                isFirstChild,
                additionalData ?? new Dictionary<string, object?>()
            );
    }
}
