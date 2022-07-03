using System.Collections.Generic;
using System.IO;
using VDT.Core.XmlConverter.Markdown;
using Xunit;

namespace VDT.Core.XmlConverter.Tests.Markdown {
    public class DefinitionDescriptionConverterTests {
        [Fact]
        public void RenderStart() {
            using var writer = new StringWriter();

            var converter = new DefinitionDescriptionConverter();
            var elementData = ElementDataHelper.Create("dd");

            converter.RenderStart(elementData, writer);

            Assert.Equal("\r\n: ", writer.ToString());
            Assert.Equal("\t", Assert.Single(Assert.IsType<Stack<string>>(elementData.AdditionalData[nameof(ContentTracker.Prefixes)])));
        }

        [Fact]
        public void RenderEnd() {
            using var writer = new StringWriter();

            var converter = new DefinitionDescriptionConverter();
            var prefixes = new Stack<string>();
            var elementData = ElementDataHelper.Create(
                "dd", 
                additionalData: new Dictionary<string, object?>() {
                    { nameof(ContentTracker.Prefixes), prefixes }
                }
            );

            prefixes.Push("> ");
            prefixes.Push("\t");

            converter.RenderEnd(elementData, writer);

            Assert.Equal("\r\n", writer.ToString());
            Assert.Equal("> ", Assert.Single(Assert.IsType<Stack<string>>(elementData.AdditionalData[nameof(ContentTracker.Prefixes)])));
        }
    }
}
