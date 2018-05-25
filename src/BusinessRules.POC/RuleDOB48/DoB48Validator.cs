using BusinessRules.POC.Data;
using BusinessRules.POC.Helpers;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.SharedRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.RuleDOB48
{
    public class DoB48Validator : IRule<Learner>
    {
        private IShortRule<LearningDelivery> _dd07IsYRule;
        private ISharedRule<Learner, bool> _learnerDoBNotNullRule;
        private IShortRule<List<LearningDelivery>> _dd04IsInRangeRule;
        private IShortRule<Learner> _IsLearnerBelowSchoolAge;
        private const string _ErrorMessage = "The learner is not over the school leaving age";

        public DoB48Validator(IShortRule<LearningDelivery> dd07IsYRule, 
            ISharedRule<Learner, bool> learnerDoBNotNullRule,
            IShortRule<List<LearningDelivery>> dd04IsInRangeRule,
            IShortRule<Learner> isLearnerBelowSchoolAge)
        {
            _dd07IsYRule = dd07IsYRule;
            _learnerDoBNotNullRule = learnerDoBNotNullRule;
            _dd04IsInRangeRule = dd04IsInRangeRule;
            _IsLearnerBelowSchoolAge = isLearnerBelowSchoolAge;

        }

        public ValidationResult Validate(Learner ObjectToValidate)
        {

            var result = new ValidationResult()
            {
                IsValid = true,
                RuleName = "DateOfBirth_48"
            };
            

            //check DoB rule, proceed only if the Dob is not null
            if (_learnerDoBNotNullRule.Evaluate(ObjectToValidate)) return result;

            //check agerule, if learner is above 16 then skip this rule
            if (_IsLearnerBelowSchoolAge.Evaluate(ObjectToValidate)) return result;

            //check the DD07 rule and get the valid prog type LDs
            var validProgTypeLDs = ObjectToValidate.LearningDeliveries.Where(ld => !_dd07IsYRule.Evaluate(ld)).ToList();


            //execute DD04 and fetch programme start date.
           if(_dd04IsInRangeRule.Evaluate(validProgTypeLDs))
           {
                result.IsValid = false;
                result.ErrorMessages = new List<string>()
                {
                    _ErrorMessage
                };
                return result;
           }
          
            return result;
        }
    }





    public class IsLearnerBelowSchoolAge : IShortRule<Learner>
    {
        private IDateHelper _dateHelper;

        public IsLearnerBelowSchoolAge(IDateHelper dateHelper)
        {
            _dateHelper = dateHelper;
        }

        public bool Evaluate(Learner ObjectToValidate)
        {
            return _dateHelper.GetAge(DateTime.Now, ObjectToValidate.DateOfBirth??DateTime.Now) > 16;
        }
    }







}
