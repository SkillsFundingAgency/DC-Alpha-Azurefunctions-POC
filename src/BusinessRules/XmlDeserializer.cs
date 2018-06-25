using System;
using System.IO;
using System.Xml.Serialization;

namespace BusinessRules
{
    public class XmlDeserializer : IXmlDeserialiser
    {
        public  T DeserializeXmlStringToObject<T>(string xmlString)
        {
           
            if (string.IsNullOrEmpty(xmlString)) return default(T);

            try
            {
                using (var stringReader = new StringReader(xmlString))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(stringReader);
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
