using BusinessRules.POC.Data;
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
    public interface IFetchSpecificFundModelsLDsWithLearnStartDate: IShortRule<Learner, List<LearningDelivery>>
    {
//        
    }

    /// <summary>
    /// 
    /// </summary>
    public class FetchSpecificFundModelsLDsWithLearnStartDate : IFetchSpecificFundModelsLDsWithLearnStartDate
    {
        private readonly DateTime _apprencticeProgAllowedStartDate;

        public FetchSpecificFundModelsLDsWithLearnStartDate(IReferenceData<string, string> referenceData)
        {            
            _apprencticeProgAllowedStartDate = Convert.ToDateTime(referenceData.Get("ApprencticeProgAllowedStartDate"));

        }
        public List<LearningDelivery> Evaluate(Learner objectToValidate)
        {
            return objectToValidate.LearningDeliveries
                .Where(x => x.FundModel == 35 && x.LearnStartDate >= _apprencticeProgAllowedStartDate)
                .ToList();
        }
    }
}
