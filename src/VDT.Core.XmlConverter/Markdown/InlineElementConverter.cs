using System.IO;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for rendering elements as inline Markdown
    /// </summary>
    public class InlineElementConverter : BaseElementConverter {
        private readonly string startOutput;
        private readonly string endOutput;

        /// <summary>
        /// Constructs an instance of a Markdown inline element converter
        /// </summary>
        /// <param name="startOutput">Value to render at the start of the element, before any possible child content is rendered</param>
        /// <param name="endOutput">Value to render at the end of the element, after any possible child content is rendered</param>
        /// <param name="validForElementNames">Element names for which this converter is valid; names are case-insensitive</param>
        public InlineElementConverter(string startOutput, string endOutput, params string[] validForElementNames) : base(validForElementNames) {
            this.startOutput = startOutput;
            this.endOutput = endOutput;
        }

        /// <inheritdoc/>
        public override void RenderStart(ElementData elementData, TextWriter writer) 
            => elementData.GetContentTracker().Write(writer, startOutput);

        /// <inheritdoc/>
        public override void RenderEnd(ElementData elementData, TextWriter writer)
            => elementData.GetContentTracker().Write(writer, endOutput);
    }
}
