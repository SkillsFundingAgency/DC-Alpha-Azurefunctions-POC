using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.ExternalData
{
    public class LARSNotionalNVQLevelData : IExternalData<string, List<string>>
    {
        public List<string> Get(string input)
        {
            //based on the LearnAimReference fetch the NotionalNVQLevelv2
            //stub it for now

            if(input == "Z0007835")
                return new List<string>()
                {
                    "E", "1","2"
                };

            return null;

        }
    }
}
