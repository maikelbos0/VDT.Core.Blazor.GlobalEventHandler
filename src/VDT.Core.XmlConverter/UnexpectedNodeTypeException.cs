using System;
using System.Xml;

namespace VDT.Core.XmlConverter {
    // TODO figure out if we need this
    public class UnexpectedNodeTypeException : Exception {
        public XmlNodeType NodeType { get; }

        internal UnexpectedNodeTypeException(string message, XmlNodeType nodeType) : base(message) {
            NodeType = nodeType;
        }
    }
}