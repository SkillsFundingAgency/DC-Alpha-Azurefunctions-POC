using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.RuleDOB48
{
    public class DD07IsYRule : IShortRule<LearningDelivery>
    {
        private ISharedRule<LearningDelivery, string> _dd07Rule;

        public DD07IsYRule(ISharedRule<LearningDelivery, string> dd07Rule)
        {
            _dd07Rule = dd07Rule;
        }

        public bool Evaluate(LearningDelivery ObjectToValidate)
        {
            return _dd07Rule.Evaluate(ObjectToValidate) != "Y";
        }
    }
}
