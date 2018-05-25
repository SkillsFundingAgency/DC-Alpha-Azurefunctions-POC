using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.SharedRules.DD28;

namespace BusinessRules.POC.SharedRules
{
    public class DD28Rule : ISharedRule<Learner, string>
    {
        private readonly IEnumerable<IDD28RuleCriteria> _dd28CriteriaRules;
        private readonly IShortRule<DD28SubModel, LearnerEmploymentStatus> _dd28PickMatchingEmpRecord;

        public DD28Rule(IShortRule<DD28SubModel, LearnerEmploymentStatus> dd28PickMatchingEmpRecord,
           IEnumerable<IDD28RuleCriteria> dd28CriteriaRules)
        {
            _dd28PickMatchingEmpRecord = dd28PickMatchingEmpRecord;

            //subrules to be exuected with OR condition
            _dd28CriteriaRules = dd28CriteriaRules;
        }

        public string Evaluate(Learner objectToValidate)
        {
            var result = "N";

            foreach (var learningDelivery in objectToValidate.LearningDeliveries)
            {
                //find matching employment record for the learningdelivery
                var matchedEmployementRecord =
                    _dd28PickMatchingEmpRecord.Evaluate(new DD28SubModel()
                    {
                        LearnerEmploymentStatusObj = objectToValidate.LearnerEmploymentStatuses,
                        LearningDeliveryObject = learningDelivery
                    });

                if (matchedEmployementRecord == null) return result;

                //pass the matched emp records and learning delivery to subrules
                var isAnyCreteriaValid = _dd28CriteriaRules.Any(criteria => criteria.Evaluate(new DD28SubModel()
                {
                    LearningDeliveryObject = learningDelivery,
                    LearnerEmploymentStatusObj = new List<LearnerEmploymentStatus>() {matchedEmployementRecord}
                }));

                if (!isAnyCreteriaValid) continue;
                //if any rule returns true then return Y
                result = "Y";
                return result;
            }
            //if it reaches here then result is N
            return result;

        }

        
        
      
    }
}
