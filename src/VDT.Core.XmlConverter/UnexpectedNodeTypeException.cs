using System;
using System.Xml;

namespace VDT.Core.XmlConverter {
    /// <summary>
    /// Represents errors that occur when converting XML nodes
    /// </summary>
    public class UnexpectedNodeTypeException : Exception {
        /// <summary>
        /// Type of node that was not expected
        /// </summary>
        public XmlNodeType NodeType { get; }

        internal UnexpectedNodeTypeException(string message, XmlNodeType nodeType) : base(message) {
            NodeType = nodeType;
        }
    }
}