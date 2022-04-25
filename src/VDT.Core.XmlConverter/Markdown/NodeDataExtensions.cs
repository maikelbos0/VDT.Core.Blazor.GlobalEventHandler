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
    }
}
