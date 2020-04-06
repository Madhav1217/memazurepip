using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Aliera.Utilities.Helpers
{
    public class XMLGeneratorService : IXMLGeneratorService
    {
        public void GenerateXML(object hrpObject, string fileName)
        {   
            var xmlDoc = new XmlDocument();
            var xmlSerializer = new XmlSerializer(hrpObject.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, hrpObject);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                xmlDoc.Save("D:\\HRPDocument\\" + fileName);
            }
        }
    }
}
