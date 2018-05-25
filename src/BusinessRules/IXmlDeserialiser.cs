using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules
{
    public interface IXmlDeserialiser
    {
        T DeserializeXmlStringToObject<T>(string xmlString);
    }
}
