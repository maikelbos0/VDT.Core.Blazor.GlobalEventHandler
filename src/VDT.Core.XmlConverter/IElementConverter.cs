using System.IO;

namespace VDT.Core.XmlConverter {
    /// <summary>
    /// Definition of a converter that can convert an xml element to the desired output
    /// </summary>
    public interface IElementConverter {
        /// <summary>
        /// Determines if the converter can be used for the current xml element
        /// </summary>
        /// <param name="elementData">Information about the element currently being converted</param>
        /// <returns><see langword="true"/> if the converter should be used; otherwise, <see langword="false"/></returns>
        public bool IsValidFor(ElementData elementData);

        /// <summary>
        /// Renders output at the start of the element, before any possible child content is rendered
        /// </summary>
        /// <param name="elementData">Information about the element currently being converted</param>
        /// <param name="writer">Text writer to write the resulting output to</param>
        public void RenderStart(ElementData elementData, TextWriter writer);

        /// <summary>
        /// Determines if the child nodes of the current element should be rendered
        /// </summary>
        /// <param name="elementData">Information about the element currently being converted</param>
        /// <returns><see langword="true"/> if the child nodes of the current element should be rendered; otherwise, <see langword="false"/></returns>
        public bool ShouldRenderContent(ElementData elementData);

        /// <summary>
        /// Renders output at the end of the element, after any possible child content is rendered
        /// </summary>
        /// <param name="elementData">Information about the element currently being converted</param>
        /// <param name="writer">Text writer to write the resulting output to</param>
        public void RenderEnd(ElementData elementData, TextWriter writer);
    }
}
