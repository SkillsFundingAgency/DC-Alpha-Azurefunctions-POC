using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;

namespace BusinessRules.POC.RuleLearnDelFAMType66.ExclusionRules
{
    public class OlassLearnerDelFamRule : ILearnerDelFam66ExclusionRuleForFam
    {
        private readonly string _allowedOlassFamType;
        private readonly string _allowedOlassFamCode;

        public OlassLearnerDelFamRule(IReferenceData<string, string> referenceData)
        {
            _allowedOlassFamType = referenceData.Get(AppConstants.LearnDelFam66OlassFamType);
            _allowedOlassFamCode = referenceData.Get(AppConstants.LearnDelFam66OlassFamCode);

        }
        public bool Evaluate(LearningDeliveryFAM ldFam)
        {
            if (ldFam == null) return false;

            return ldFam.LearnDelFAMType == _allowedOlassFamType &&
                   ldFam.LearnDelFAMCode == _allowedOlassFamCode;
        }

      
    }

    public interface ILearnerDelFam66ExclusionRuleForFam : IShortRule<LearningDeliveryFAM>
    {
//        bool Evaluate(LearningDeliveryFAM ldFam);
    }
}