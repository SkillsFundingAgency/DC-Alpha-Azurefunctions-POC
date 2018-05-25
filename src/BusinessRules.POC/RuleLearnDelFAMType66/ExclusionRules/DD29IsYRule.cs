using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;

namespace BusinessRules.POC.RuleLearnDelFAMType66.ExclusionRules
{
    class DD29IsYRule :  ILearnerDelFam66ExclusionRuleForLearner
    {
        private readonly ISharedRule<LearningDelivery, string> _dd29Rule;

        public DD29IsYRule(ISharedRule<LearningDelivery, string> dd29Rule)
        {
            _dd29Rule = dd29Rule;
        }
        public bool Evaluate(Learner objectToValidate)
        {
            return objectToValidate.LearningDeliveries.Any(ld => _dd29Rule.Evaluate(ld) == "Y");
        }
    }
}
