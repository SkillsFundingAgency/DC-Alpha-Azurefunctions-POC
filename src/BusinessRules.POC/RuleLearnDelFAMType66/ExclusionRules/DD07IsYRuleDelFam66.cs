using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;

namespace BusinessRules.POC.RuleLearnDelFAMType66.ExclusionRules
{
    public class DD07IsYRuleDelFam66 : ILearnerDelFam66ExclusionRuleForLearner
    {
        private readonly ISharedRule<LearningDelivery, string> _dd07Rule;

        public DD07IsYRuleDelFam66(ISharedRule<LearningDelivery, string> dd07Rule)
        {
            _dd07Rule = dd07Rule;
        }
        public bool Evaluate(Learner objectToValidate)
        {
            return objectToValidate.LearningDeliveries.Any(ld => _dd07Rule.Evaluate(ld) == "Y");
        }
    }

    public interface ILearnerDelFam66ExclusionRuleForLearner : IShortRule<Learner>
    {
//        bool Evaluate(Learner learner);
    }
}
