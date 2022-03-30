using System.IO;
using System.Xml;

namespace VDT.Core.XmlConverter.Nodes {
    public interface INodeConverter {
        public void Convert(XmlReader reader, TextWriter writer);
    }
}
