using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessRules.POC.ReferenceData;

namespace BusinessRules.POC.ExternalData
{
    public class LARSCategoryRefData : IExternalData<string, List<string>>
    {

        public LARSCategoryRefData()
        {
            
        }
        public List<string> Get(string learnerAimRef)
        {
            //stub it for now. but replace this with actual fetch from LARS data or cache
            if (!string.IsNullOrEmpty(learnerAimRef))
                return new List<string>()
                {
                    "2","4"
                };

            return null;
        }
    }
}
