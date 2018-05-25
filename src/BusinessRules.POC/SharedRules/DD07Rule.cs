using BusinessRules.POC.Data;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.SharedRules
{
    public class DD07Rule : ISharedRule<LearningDelivery, string>
    {
        private ISharedData _sharedData;

        public DD07Rule(ISharedData sharedData)
        {
            _sharedData = sharedData;
        }
        public string Evaluate(LearningDelivery objectToValidate)
        {
            return _sharedData.ApprenticeProgTypes.Contains(Convert.ToInt16(objectToValidate.ProgType)) ? "Y" : "N";
        }
    }
}
