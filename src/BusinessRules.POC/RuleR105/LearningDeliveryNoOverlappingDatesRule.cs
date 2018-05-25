using BusinessRules.POC.Enums;
using BusinessRules.POC.Helpers;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.RuleR105
{
    public class LearningDeliveryNoOverlappingDatesRule : ILearningDeliveryNoOverlappingDatesRule
    {
        public LearningDeliveryNoOverlappingDatesRule()
        {

        }

        public bool Evaluate(LearningDelivery ObjectToValidate)
        {

            var learningDelFAMs = ObjectToValidate.LearningDeliveryFAMs
                .Where(ldFAM => ldFAM.LearnDelFAMType == LearningDeliveryFAMTypes.ACT.ToString())
                .OrderBy(x => x.LearnDelFAMDateFrom).ToList();


            for (int i = 0; i < learningDelFAMs.Count; i++)
            {
               
                var ldFAMOuterLevel = learningDelFAMs[i];

                var outLevelRange = new Range<DateTime>(ldFAMOuterLevel.LearnDelFAMDateFrom ?? DateTime.Now,
                      ldFAMOuterLevel.LearnDelFAMDateTo ?? DateTime.Now);

                for (int j = i + 1; j < learningDelFAMs.Count; j++)
                {
                    var ldFAMInnerLevel = learningDelFAMs.ElementAt(j);
                    //if the FAMCode is same then skip this
                    if (ldFAMOuterLevel.LearnDelFAMCode == ldFAMInnerLevel.LearnDelFAMCode) continue;
                   
                    var innerLevelRange = new Range<DateTime>(ldFAMInnerLevel.LearnDelFAMDateFrom ?? DateTime.Now,
                        ldFAMInnerLevel.LearnDelFAMDateTo ?? DateTime.Now);

                    //find intersection periods
                    if (outLevelRange.IsOverlapped(innerLevelRange)) return true;


                }

            }

            return false;


        }
    }

}
