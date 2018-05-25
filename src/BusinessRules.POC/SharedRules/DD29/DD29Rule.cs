using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessRules.POC.ExternalData;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;

namespace BusinessRules.POC.SharedRules.DD29
{
    /// <summary>
    /// 
    /// </summary>
    public class DD29Rule : ISharedRule<Learner, string>
    {
        private readonly IExternalData<string, List<string>> _larsExternalData;
        private readonly IReferenceData<string, string> _referenceData;

        public DD29Rule(IExternalData<string, List<string>> larsExternalData, IReferenceData<string, string> referenceData )
        {
            _larsExternalData = larsExternalData;
            _referenceData = referenceData;
        }
        public string Evaluate(Learner learner)
        {
            if (learner?.LearningDeliveries == null)  return "N";

            var allowedLARSCategoryRefs = _referenceData.Get(AppConstants.DD29LARSCategoryRef).Split(',').ToList();

            foreach (var learningDelivery in learner.LearningDeliveries)
            {
                var larsResult = _larsExternalData.Get(learningDelivery.LearnAimRef) ?? new List<string>();

                if (learningDelivery.ProgType == _referenceData.Get(AppConstants.DD29LearningDeliveryProgType) &&
                    larsResult.Intersect(allowedLARSCategoryRefs).Any())
                    return "Y";
            }

            return "N";
        }
    }
}
