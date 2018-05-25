using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;

namespace BusinessRules.POC.RuleLearnDelFAMType66.ExclusionRules
{
    public class RestartLearnerDelFamRule : ILearnerDelFam66ExclusionRuleForFam
    {
        private readonly string _restartFamType;
        private readonly string _restartFamCode;


        public RestartLearnerDelFamRule(IReferenceData<string, string> referenceData)
        {
            _restartFamType = referenceData.Get(AppConstants.LearnDelFam66RestartFamType);
            _restartFamCode = referenceData.Get(AppConstants.LearnDelFam66RestartFamCode);


        }
        public bool Evaluate(LearningDeliveryFAM ldFam)
        {
            if (ldFam == null) return false;

            return ldFam.LearnDelFAMType == _restartFamType && ldFam.LearnDelFAMCode == _restartFamCode;
        }
    }
}