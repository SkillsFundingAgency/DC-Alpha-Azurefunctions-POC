using BusinessRules.POC.Data;
using BusinessRules.POC.Helpers;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.SharedRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.RuleDOB48
{
    public class DD04IsInRangeRule : IShortRule<List<LearningDelivery>>
    {
        private ISharedRule<List<LearningDelivery>, List<DD04Result>> _dd04Rule;
        private IReferenceData<string, string> _ReferenceData;
        private IDateHelper _dateHelper;

        public DD04IsInRangeRule(ISharedRule<List<LearningDelivery>, List<DD04Result>> dd04Rule,
            IReferenceData<string,string> referenceData, IDateHelper dateHelper)
        {
            _dd04Rule = dd04Rule;
            _ReferenceData = referenceData;
            _dateHelper = dateHelper;
        }

        public bool Evaluate(List<LearningDelivery> ObjectToValidate)
        {
            //get the Progstart date from dd04 for each LD
            var validStartProgDatesWithLDs = _dd04Rule.Evaluate(ObjectToValidate);

            var doB = ObjectToValidate.FirstOrDefault().DateOfBirth;

            var yearLearnerTurning16 = _dateHelper.GetYearInWhichPersonTurnsTo(16, doB);

            if (yearLearnerTurning16 == 0) return false; //Dob is null

            var lastFridayInJuneInAcaYear = _dateHelper.GetLastFridayInJuneOfAcademicYear(new DateTime(yearLearnerTurning16,
                doB.Value.Month, doB.Value.Day));

            var apprenticeProgAllowedStartDate = Convert.ToDateTime(_ReferenceData.Get("ApprencticeProgAllowedStartDate"));

            foreach (var ld in validStartProgDatesWithLDs)
            {

                //if DD04 startdateofprog is less than allowedstart date then skip the ld
                if (ld.StartDateOfProgramme >= apprenticeProgAllowedStartDate && 
                    ld.StartDateOfProgramme <= lastFridayInJuneInAcaYear)
                    return true;
            }



            return false;
        }
    }
}
