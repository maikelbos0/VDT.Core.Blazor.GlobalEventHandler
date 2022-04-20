using System;
using System.Collections.Generic;
using System.Linq;

namespace VDT.Core.XmlConverter.Tests {
    public static class ElementDataHelper {
        public static ElementData Create(string name, params ElementData[] ancestors)
            => Create(name, null, false, ancestors);

        public static ElementData Create(string name, IEnumerable<ElementData> ancestors)
            => Create(name, null, false, ancestors.ToList());

        public static ElementData Create(
            string name, 
            Dictionary<string, string>? attributes = null, 
            bool isSelfClosing = false, 
            IList<ElementData>? ancestors = null, 
            bool isFirstChild = false,
            Dictionary<string, object?>? additionalData = null
        )
            => new ElementData(
                name,
                attributes ?? new Dictionary<string, string>(),
                isSelfClosing,
                ancestors ?? Array.Empty<ElementData>(),
                isFirstChild,
                additionalData ?? new Dictionary<string, object?>()
            );
    }
}
