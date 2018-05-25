using BusinessRules.POC.Enums;
using BusinessRules.POC.Helpers;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.RuleR105
{
    public class R105Validator : IRule<Learner>
    {
        private readonly IR105PickLdFamACTTypes _r105PickLdFamACTTypes;
        private readonly ILearningDeliveryNoOverlappingDatesRule _learningDeliveryNoOverlappingDatesRule;
        private const string ErrorMessage = "The learner must not have different Apprenticeship contract types recorded at the same time";

      
        public R105Validator(IR105PickLdFamACTTypes r105PickLdFamACTTypes,
            ILearningDeliveryNoOverlappingDatesRule learningDeliveryNoOverlappingDatesRule)
        {
            _r105PickLdFamACTTypes = r105PickLdFamACTTypes;
            _learningDeliveryNoOverlappingDatesRule = learningDeliveryNoOverlappingDatesRule;
        }

        public ValidationResult Validate(Learner learner)
        {
            var result = new ValidationResult()
            {
                IsValid = true
            };

            var validLearningDeliveries = _r105PickLdFamACTTypes.Evaluate(learner);
            if (validLearningDeliveries == null) return result;

            foreach (var learningDelivery in validLearningDeliveries)
            {
                if (!_learningDeliveryNoOverlappingDatesRule.Evaluate(learningDelivery)) continue;
                
                //if found a record that has overlapping dates then return false
                result.IsValid = false;
                result.ErrorMessages = new List<string>()
                {
                    ErrorMessage
                };
                return result;
            }

            return result;
        }
    }

   
  


}

