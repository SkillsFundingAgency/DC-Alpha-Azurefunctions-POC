using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;

namespace BusinessRules.POC.RuleLearnDelFAMType66.ExclusionRules
{
    public class SteelIndustryRedundancyTrainingLearnerDelFamRule : ILearnerDelFam66ExclusionRuleForFam
    {
        private readonly string _allowedSteelIndRedTrainingFamType;
        private readonly string _allowedSteelIndRedTrainingFamCode;

        public SteelIndustryRedundancyTrainingLearnerDelFamRule(IReferenceData<string, string> referenceData)
        {
            _allowedSteelIndRedTrainingFamType = referenceData.Get(AppConstants.LearnDelFam66SteelIndRedTrainingFamType);
            _allowedSteelIndRedTrainingFamCode = referenceData.Get(AppConstants.LearnDelFam66SteelIndRedTrainingFamCode);

        }
        public bool Evaluate(LearningDeliveryFAM ldFam)
        {
            return ldFam.LearnDelFAMType == _allowedSteelIndRedTrainingFamType &&
                   ldFam.LearnDelFAMCode == _allowedSteelIndRedTrainingFamCode;
        }
    }
}