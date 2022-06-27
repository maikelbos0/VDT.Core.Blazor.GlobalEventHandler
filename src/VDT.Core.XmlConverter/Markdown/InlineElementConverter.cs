using System.IO;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for rendering elements as inline Markdown
    /// </summary>
    public class InlineElementConverter : BaseElementConverter {
        /// <summary>
        /// Value to render at the start of the element, before any possible child content is rendered
        /// </summary>
        public string StartOutput { get; }

        /// <summary>
        /// Value to render at the end of the element, after any possible child content is rendered
        /// </summary>
        public string EndOutput { get; }

        /// <summary>
        /// Constructs an instance of a Markdown inline element converter
        /// </summary>
        /// <param name="startOutput">Value to render at the start of the element, before any possible child content is rendered</param>
        /// <param name="endOutput">Value to render at the end of the element, after any possible child content is rendered</param>
        /// <param name="validForElementNames">Element names for which this converter is valid; names are case-insensitive</param>
        public InlineElementConverter(string startOutput, string endOutput, params string[] validForElementNames) : base(validForElementNames) {
            StartOutput = startOutput;
            EndOutput = endOutput;
        }

        /// <inheritdoc/>
        public override void RenderStart(ElementData elementData, TextWriter writer) 
            => elementData.GetContentTracker().Write(writer, StartOutput);

        /// <inheritdoc/>
        public override void RenderEnd(ElementData elementData, TextWriter writer)
            => elementData.GetContentTracker().Write(writer, EndOutput);
    }
}
