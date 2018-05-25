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
    /// <summary>
    /// DD28 criteria 3 rule
    /// </summary>
    public class DD28Criteria3 : IDD28RuleCriteria
    {
        private readonly IEnumerable<string> _allowedEmpStats;
        private readonly string _allowedESMtypePart1;
        private readonly string[] _allowedESMCodesPart1;
        private readonly string _allowedESMTypePart2;
        private readonly string[] _allowedESMCodesPart2;

        public DD28Criteria3(IReferenceData<string, string> referenceData)
        {
            _allowedEmpStats = referenceData.Get(AppConstants.DD28Criteria3EMPStats).Split(',');
            _allowedESMtypePart1 = referenceData.Get(AppConstants.DD28Criteria3ESMTypePart1);
            _allowedESMCodesPart1 = referenceData.Get(AppConstants.DD28Criteria3ESMCodesPart1).Split(',');

            _allowedESMTypePart2 = referenceData.Get(AppConstants.DD28Criteria3ESMTypePart2);
            _allowedESMCodesPart2 = referenceData.Get(AppConstants.DD28Criteria3ESMCodesPart2).Split(',');


        }

        public bool Evaluate(DD28SubModel dd28SubModel)
        {
            // LearnerEmploymentStatus.EmpStat = 10  and 
            // (EmploymentStatusMonitoring.ESMType = EII and EmploymentStatusMonitoring.ESMCode = 2) and 
            // (EmploymentStatusMonitoring.ESMType = BSI and EmploymentStatusMonitoring.ESMCode = 3 or 4), 

            //if the list contains 0 or more than 1 then return false, there can only be one LearnerEmpStatus obj 
            //valid for a specific LD.
            if (dd28SubModel.LearnerEmploymentStatusObj.Count() == 0 ||
                dd28SubModel.LearnerEmploymentStatusObj.Count() > 1) return false;

            //pick the first one
            var validLearnerEmpStatusObj = dd28SubModel.LearnerEmploymentStatusObj.FirstOrDefault();


            return (_allowedEmpStats.Contains(validLearnerEmpStatusObj.EmpStat.ToString()) &&
                    validLearnerEmpStatusObj.EmploymentStatusMonitorings.Any(x =>
                        x.ESMType == _allowedESMtypePart1 && _allowedESMCodesPart1.Contains(x.ESMCode.ToString())) &&
                    validLearnerEmpStatusObj.EmploymentStatusMonitorings.Any(x =>
                        x.ESMType == _allowedESMTypePart2 && _allowedESMCodesPart2.Contains(x.ESMCode.ToString()))
            );

        }
    }
}
