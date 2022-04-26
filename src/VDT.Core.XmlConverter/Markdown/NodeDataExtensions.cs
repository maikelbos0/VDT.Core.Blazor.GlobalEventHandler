using System;
using System.Linq;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Markdown extension methods for <see cref="INodeData"/>
    /// </summary>
    public static class NodeDataExtensions {
        /// <summary>
        /// Get the content tracker for the node data
        /// </summary>
        /// <param name="nodeData">Node data to which the tracker belongs</param>
        /// <returns>Content tracker for the node data</returns>
        public static ContentTracker GetContentTracker(this INodeData nodeData) {
            if (!nodeData.AdditionalData.TryGetValue(nameof(ContentTracker), out var elementTrackerObj) || !(elementTrackerObj is ContentTracker elementTracker)) {
                elementTracker = new ContentTracker(nodeData.AdditionalData);
                nodeData.AdditionalData[nameof(ContentTracker)] = elementTracker;
            }

            return elementTracker;
        }

        /// <summary>
        /// Try to get an attribute by case-insensitive name
        /// </summary>
        /// <param name="elementData">Element data for which to get the attribute</param>
        /// <param name="name">Attribute name</param>
        /// <param name="value">Attribute value if found; otherwise <see langword="null"/></param>
        /// <returns><see langword="true"/> if the attribute was present; otherwise <see langword="false"/></returns>
        public static bool TryGetAttribute(this ElementData elementData, string name, out string value) {
            var isFound = elementData.Attributes.TryGetValue(name, out value);

            if (!isFound) {
                var attributeName = elementData.Attributes.Keys.FirstOrDefault(n => string.Equals(n, name, StringComparison.OrdinalIgnoreCase));

                if (attributeName != null) {
                    value = elementData.Attributes[attributeName];
                    isFound = true;
                }
            }            

            return isFound;
        }
    }
}
