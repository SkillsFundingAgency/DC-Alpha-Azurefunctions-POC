using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace FunctionApp1
{
    public static class XmlDeserialiserAT
    {
        [FunctionName("XmlDeserialiser")]
        public static BusinessRules.CustomersData Run([ActivityTrigger] DurableActivityContext context)
        {
            //log.Info("C# HTTP trigger function processed a request.");

            #region xmlstring

            var xmlString = @"<?xml version=""1.0"" ?>
<CustomerQueryRs>
  <CustomerRet>
    <ListID>6BE0000-1159990808</ListID>
    <Name>+ Blaine Bailey</Name>
    <FullName>+ Blaine Bailey</FullName>
    <Phone>866-855-0800</Phone>
  </CustomerRet>
  <CustomerRet>
    <ListID>9BA0000-1165353294</ListID>
    <Name>+ Brian Boyd</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
  <CustomerRet>
    <ListID>9280000-1164147562</ListID>
    <Name>+ Brian Leahy</Name>
    <FullName>+ Brian Leahy</FullName>
    <Phone>508-341-0955</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-13165353294</ListID>
    <Name>+ Brian Boyd1</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-11265353294</ListID>
    <Name>+ Brian Boyd2</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-11365353294</ListID>
    <Name>+ Brian Boyd33</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1145353294</ListID>
    <Name>+ Brian Boyd4</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1155353294</ListID>
    <Name>+ Brian Boyd5</Name>
    <FullName>+ Brian Boyd5</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1165353294</ListID>
    <Name>+ Brian Boyd</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1857</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1165363294</ListID>
    <Name>+ Brian Boyd6</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1876</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1175353294</ListID>
    <Name>+ Brian Boyd7</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1777</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1165353294</ListID>
    <Name>+ Brian Boyd</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1165353294</ListID>
    <Name>+ Brian Boyd</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1165353294</ListID>
    <Name>+ Brian Boyd</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1165353294</ListID>
    <Name>+ Brian Boyd</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1165353294</ListID>
    <Name>+ Brian Boyd</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1165353294</ListID>
    <Name>+ Brian Boyd</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1165353294</ListID>
    <Name>+ Brian Boyd</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1165353294</ListID>
    <Name>+ Brian Boyd</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1165353294</ListID>
    <Name>+ Brian Boyd</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1165353294</ListID>
    <Name>+ Brian Boyd</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1165353294</ListID>
    <Name>+ Brian Boyd</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1165353294</ListID>
    <Name>+ Brian Boyd</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1165353294</ListID>
    <Name>+ Brian Boyd</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1165353294</ListID>
    <Name>+ Brian Boyd</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1165353294</ListID>
    <Name>+ Brian Boyd</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1165353294</ListID>
    <Name>+ Brian Boyd</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1165353294</ListID>
    <Name>+ Brian Boyd</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1165353294</ListID>
    <Name>+ Brian Boyd</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1165353294</ListID>
    <Name>+ Brian Boyd</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
 <CustomerRet>
    <ListID>9BA0000-1165353294</ListID>
    <Name>+ Brian Boyd</Name>
    <FullName>+ Brian Boyd</FullName>
    <Phone>203-245-1877</Phone>
  </CustomerRet>
</CustomerQueryRs>";

            #endregion
            var serializer = new XmlSerializer(typeof(BusinessRules.CustomersData), new XmlRootAttribute("CustomerQueryRs"));
            using (var stringReader = new StringReader(xmlString))
            using (var reader = XmlReader.Create(stringReader))
            {
                var result = (BusinessRules.CustomersData)serializer.Deserialize(reader);
                return result;
            }

            //BusinessRules.IXmlDeserialiser xmlDeserialiser = new BusinessRules.XmlDeserializer();
            //return xmlDeserialiser.DeserializeXmlStringToObject<BusinessRules.catalog>(xmlString);
    
        }
    }
}
