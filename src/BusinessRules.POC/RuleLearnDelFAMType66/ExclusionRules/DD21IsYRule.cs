using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;

namespace BusinessRules.POC.RuleLearnDelFAMType66.ExclusionRules
{
    public class DD21IsYRule : ILearnerDelFam66ExclusionRuleForLearner
    {
        private readonly ISharedRule<Learner, string> _dd21Rule;

        public DD21IsYRule(ISharedRule<Learner, string> dd21Rule)
        {
            _dd21Rule = dd21Rule;
        }

        public bool Evaluate(Learner learner)
        {
            return  _dd21Rule.Evaluate(learner) == "Y";
        }
    }
}
