using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.SharedRules
{
    /// <summary>
    /// 
    /// </summary>
    public class DD04Rule : ISharedRule<List<LearningDelivery>, List<DD04Result>>
    {

        List<DD04Result> ISharedRule<List<LearningDelivery>, List<DD04Result>>.Evaluate(List<LearningDelivery> objectToValidate)
        {
            var result = new List<DD04Result>();

            //find valid start dates  is the earliest value of LearningDelivery.LearnStartDate for all programme aims with 
            //LearningDelivery.AimType = 1 and
            //the same value of Learner.LearnRefNumber, LearningDelivery.ProgType, LearningDelivery.FworkCode(only include for apprenticeships(not apprenticeship standards)) and LearningDelivery.PwayCode as this aim(only include for apprenticeships).
            var validProgStartDatesList = objectToValidate.Where(x => x.ProgType != null && x.AimType == 1)
                          .GroupBy(code => new DD04ValidLDKey { ProgType = code.ProgType,FworkCode= code.FworkCode,PwayCode= code.PwayCode })
                          .Select(grp => new DD04ValidLDResult { Key = grp.Key, Value = grp.OrderBy(x => x.LearnStartDate).FirstOrDefault() })
                          .ToList();

            //loop through the LDs passed in and populate the calculated prog startdate from the above for each LD
            foreach (var ld in objectToValidate)
            {
                var matchedLDStartdate = validProgStartDatesList.Where(x => x.Key.PwayCode == ld.PwayCode 
                            && x.Key.ProgType == ld.ProgType && x.Key.FworkCode == ld.FworkCode).Select(x=> x.Value).FirstOrDefault();

                if (matchedLDStartdate != null)
                    result.Add(new DD04Result() { LearningDelivery = ld, StartDateOfProgramme = matchedLDStartdate.LearnStartDate });
                else
                    result.Add(new DD04Result() { LearningDelivery = ld, StartDateOfProgramme = null });

            }

            return result;
        }

       
    }

    public class DD04ValidLDKey
    {
        public string ProgType { get; set; }
        public string FworkCode { get; set; }
        public string PwayCode { get; set; }
    }

    public class DD04ValidLDResult
    {
        public DD04ValidLDKey Key { get; set; }
        public LearningDelivery Value { get; set; }
    }

    

    public class DD04Result
    {
        public LearningDelivery LearningDelivery { get; set; }
        public DateTime? StartDateOfProgramme { get; set; }

    }

}
