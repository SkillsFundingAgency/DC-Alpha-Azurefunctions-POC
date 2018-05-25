using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;

namespace BusinessRules.POC.RuleLearnDelFAMType66.ExclusionRules
{
    public class RotlLearnerDelFamRule : ILearnerDelFam66ExclusionRuleForFam
    {
        private readonly string _allowedRotlFamType;
        private readonly string _allowedRotlFamCode;

        public RotlLearnerDelFamRule(IReferenceData<string, string> referenceData)
        {
            _allowedRotlFamType = referenceData.Get(AppConstants.LearnDelFam66RotlFamType);
            _allowedRotlFamCode = referenceData.Get(AppConstants.LearnDelFam66RotlFamCode);

        }
        public bool Evaluate(LearningDeliveryFAM ldFam)
        {
            if (ldFam == null) return false;

            return ldFam.LearnDelFAMType == _allowedRotlFamType &&
                   ldFam.LearnDelFAMCode == _allowedRotlFamCode;
        }
    }
}