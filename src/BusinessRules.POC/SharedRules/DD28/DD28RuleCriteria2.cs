using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;

namespace BusinessRules.POC.SharedRules.DD28
{
    public interface IDD28RuleCriteria
    {
        bool Evaluate(DD28SubModel dd28SubModel);
    }

    public class DD28RuleCriteria2 : IDD28RuleCriteria
    {
        private readonly IEnumerable<string> _allowedEmpStats;
        private readonly string _allowedESMtype;
        private readonly IEnumerable<string> _allowedESMCodes;

        public DD28RuleCriteria2(IReferenceData<string, string> referenceData)
        {
            _allowedEmpStats = referenceData.Get(AppConstants.DD28Criteria2LearnerEmplStatusEmpStats).Split(',');
            _allowedESMtype = referenceData.Get(AppConstants.DD28Criteria2EmpStatusMonitoringESMType);
            _allowedESMCodes = referenceData.Get(AppConstants.DD28Criteria2EmpStatusMonitoringESMCodes).Split(',');

        }
        public bool Evaluate(DD28SubModel dd28SubModel)
        {

            //if the list contains 0 or more than 1 then return false, there can only be one LearnerEmpStatus obj 
            //valid for a specific LD.
            if (dd28SubModel.LearnerEmploymentStatusObj.Count() == 0 ||
                dd28SubModel.LearnerEmploymentStatusObj.Count() > 1) return false;

            //pick the first one
            var validLearnerEmpStatusObj = dd28SubModel.LearnerEmploymentStatusObj.FirstOrDefault();


            return (dd28SubModel.LearningDeliveryObject.FundModel == 35 &&
                    _allowedEmpStats.Contains(validLearnerEmpStatusObj.EmpStat.ToString()) &&
                    validLearnerEmpStatusObj.EmploymentStatusMonitorings.Any(x =>
                        x.ESMType == _allowedESMtype && _allowedESMCodes.Contains(x.ESMCode.ToString()))
            );

        }
    }
}
