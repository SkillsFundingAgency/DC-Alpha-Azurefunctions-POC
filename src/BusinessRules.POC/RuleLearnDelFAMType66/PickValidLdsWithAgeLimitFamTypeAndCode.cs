using BusinessRules.POC.ExternalData;
using BusinessRules.POC.Helpers;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.RuleLearnDelFAMType66
{
    public interface IPickValidLdsWithAgeLimitFamTypeAndCode: IShortRule<List<LearningDelivery>, List<LearningDelivery>>
    {
        //List<LearningDelivery> Evaluate(List<LearningDelivery> objectToValidate);
    }

    public class PickValidLdsWithAgeLimitFamTypeAndCode : IPickValidLdsWithAgeLimitFamTypeAndCode
    {
        private readonly IDateHelper _dateHelper;
        private readonly IValidateLARNotionalNVQLevelRule _ValidateLARNotionalNVQLevelRule;

        public PickValidLdsWithAgeLimitFamTypeAndCode(IDateHelper dateHelper, IValidateLARNotionalNVQLevelRule validateLARNotionalNVQLevelRule)
        {
            _dateHelper = dateHelper;
            _ValidateLARNotionalNVQLevelRule = validateLARNotionalNVQLevelRule;
        }
        public List<LearningDelivery> Evaluate(List<LearningDelivery> objectToValidate)
        {
            var result = new List<LearningDelivery>();
            foreach (var learningDelivery in objectToValidate)
            {
                var learnerAgewithResToProgStartdate =
                    _dateHelper.GetAge(learningDelivery.LearnStartDate.Value, learningDelivery.DateOfBirth);
                //pick learningdeliverys  with age greater than or equal to 24
                if (learnerAgewithResToProgStartdate < 24) continue;

                //pick lds with FAMCode and type
                if (learningDelivery.LearningDeliveryFAMs
                    .Count(ldFAM => ldFAM.LearnDelFAMCode == "1" && ldFAM.LearnDelFAMType == "FFI") <= 0) continue;

                //check and validate the LARSNVQ condition
                if (_ValidateLARNotionalNVQLevelRule.Evaluate(learningDelivery))
                    result.Add(learningDelivery);

            }
            return result;
        }
    }
}
