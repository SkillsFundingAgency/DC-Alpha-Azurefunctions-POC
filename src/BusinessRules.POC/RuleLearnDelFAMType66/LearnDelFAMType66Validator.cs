using BusinessRules.POC.Data;
using BusinessRules.POC.ExternalData;
using BusinessRules.POC.Helpers;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using BusinessRules.POC.Enums;
using BusinessRules.POC.RuleLearnDelFAMType66.ExclusionRules;
using BusinessRules.POC.RuleR105;

namespace BusinessRules.POC.RuleLearnDelFAMType66
{
    public class LearnDelFAMType66Validator : IRule<Learner>
    {
        private readonly ISharedRule<Learner, bool> _learnerDoBShouldNotbeNull;
        private readonly IFetchSpecificFundModelsLDsWithLearnStartDate _fetchSpecificFundModelsLDsWithLearnStartDate;
        private readonly IPickValidLdsWithAgeLimitFamTypeAndCode _pickValidLdsWithAgeLimitFamTypeAndCode;
        private readonly ILearnerDelFam66ExclusionRule _learnerDelFamExclusionRulesValidator;
        private const string ErrorMessage = "The learner must not have different Apprenticeship contract types recorded at the same time";


        public LearnDelFAMType66Validator(
            [KeyFilter(SharedRuleNames.LearnerDobShouldNotBeNull)] ISharedRule<Learner, bool> learnerDoBShouldNotbeNull,
            IFetchSpecificFundModelsLDsWithLearnStartDate fetchSpecificFundModelsLDsWithLearnStartDate,
            IPickValidLdsWithAgeLimitFamTypeAndCode pickValidLdsWithAgeLimitFamTypeAndCode,
            ILearnerDelFam66ExclusionRule learnerDelFamExclusionRulesValidator)
        {
            _learnerDoBShouldNotbeNull = learnerDoBShouldNotbeNull;
            _fetchSpecificFundModelsLDsWithLearnStartDate = fetchSpecificFundModelsLDsWithLearnStartDate;
            _pickValidLdsWithAgeLimitFamTypeAndCode = pickValidLdsWithAgeLimitFamTypeAndCode;
            _learnerDelFamExclusionRulesValidator = learnerDelFamExclusionRulesValidator;
        }


        public ValidationResult Validate(Learner learner)
        {

            var result = new ValidationResult()
            {
                IsValid = true,
                RuleName = RuleNames.LearnDelFam66.ToString()
            };

            // check exclusion rules first, if any exclusion rule is returning true then skip this rule.
            if (_learnerDelFamExclusionRulesValidator.Evaluate(learner)) return result;

            //check DoB rule, proceed only if the Dob is not null
            if (_learnerDoBShouldNotbeNull.Evaluate(learner)) return result;


            //fetch fundmodel 35 LDs for this academic year .
            var eligibleLDs = _fetchSpecificFundModelsLDsWithLearnStartDate.Evaluate(learner);
            if (eligibleLDs.Count() == 0) return result;

            //check the learner age is 24 or more rule & LFAMType FAMCODE
            var validLDsWithCorrectAgeandFAMtypesAndLars = _pickValidLdsWithAgeLimitFamTypeAndCode.Evaluate(eligibleLDs);
            if (validLDsWithCorrectAgeandFAMtypesAndLars == null ||
                validLDsWithCorrectAgeandFAMtypesAndLars.Count == 0) return result;

            result.IsValid = false;
            result.ErrorMessages = new List<String>()
            {
                ErrorMessage
            };

            return result;

        }

       
    }

   









}
