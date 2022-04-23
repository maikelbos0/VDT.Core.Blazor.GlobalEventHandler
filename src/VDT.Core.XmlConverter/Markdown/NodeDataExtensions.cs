namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Markdown extension methods for <see cref="INodeData"/>
    /// </summary>
    public static class NodeDataExtensions {
        /// <summary>
        /// Get the trailing new line tracker for the node data
        /// </summary>
        /// <param name="nodeData">Node data to which the tracker belongs</param>
        /// <returns>Trailing new line tracker for the node data</returns>
        public static TrailingNewLineTracker GetTrailingNewLineTracker(this INodeData nodeData) {
            if (!nodeData.AdditionalData.TryGetValue(nameof(TrailingNewLineTracker), out var elementTrackerObj) || !(elementTrackerObj is TrailingNewLineTracker elementTracker)) {
                elementTracker = new TrailingNewLineTracker(nodeData.AdditionalData);
                nodeData.AdditionalData[nameof(TrailingNewLineTracker)] = elementTracker;
            }

            return elementTracker;
        }
    }
}
