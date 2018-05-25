using BusinessRules.POC.ExternalData;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.RuleLearnDelFAMType66
{
    public interface IValidateLARNotionalNVQLevelRule
    {
        bool Evaluate(LearningDelivery objectToValidate);
    }

    public class ValidateLARNotionalNVQLevelRule : IValidateLARNotionalNVQLevelRule
    {
        private IExternalData<string, List<string>> _LARSNotionalNVQLevelData;
        private IReferenceData<string, string> _referenceData;
        private readonly List<string> _AllowedLARSNotionalNVQLevelv2;

        public ValidateLARNotionalNVQLevelRule(IExternalData<string, List<string>> lARSNotionalNVQLevelData, IReferenceData<string, string> referenceDataFromSettings )
        {
            _LARSNotionalNVQLevelData = lARSNotionalNVQLevelData;
            _referenceData = referenceDataFromSettings;
            _AllowedLARSNotionalNVQLevelv2 = _referenceData.Get("AllowedLARSNotionalNVQLevelv2").Split(',').ToList();

        }
        public bool Evaluate(LearningDelivery objectToValidate)
        {
            var larsResult = _LARSNotionalNVQLevelData.Get(objectToValidate.LearnAimRef);

            return larsResult != null && larsResult.Intersect(_AllowedLARSNotionalNVQLevelv2).Any();
        }
    }
}
