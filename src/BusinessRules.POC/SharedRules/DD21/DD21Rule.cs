using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.SharedRules.DD28;

namespace BusinessRules.POC.SharedRules.DD21
{
    public class DD21Rule : IShortRule<Learner, string>
    {
        private readonly IShortRule<DD28SubModel, LearnerEmploymentStatus> _dd28PickMatchingEmpRecord;
        private IDD21GetLearningDeliveriesWithSpecificEmpStatusMonitoringType _dd21GetLearningDeliveriesWithSpecificEmp;
        private string _allowedFamType;
        private string _FamCodeShouldNotBe;

        public DD21Rule(IShortRule<DD28SubModel, LearnerEmploymentStatus> dd28PickMatchingEmpRecord, 
            IDD21GetLearningDeliveriesWithSpecificEmpStatusMonitoringType dd21GetLearningDeliveriesWithSpecificEmp,
            IReferenceData<string, string> referenceData)
        {
            _dd28PickMatchingEmpRecord = dd28PickMatchingEmpRecord;
            _dd21GetLearningDeliveriesWithSpecificEmp = dd21GetLearningDeliveriesWithSpecificEmp;
            _allowedFamType = referenceData.Get(AppConstants.DD21AllowedFamType);
            _FamCodeShouldNotBe = referenceData.Get(AppConstants.DD21FamCodeShouldNotBe);

        }

        public string Evaluate(Learner learner)
        {
            var result = "N";

            //get the emprecords with allowed empstat and emptype code
            var empRecordsWithAllowedEmpStatAndEsmType =
                _dd21GetLearningDeliveriesWithSpecificEmp.Evaluate(learner.LearnerEmploymentStatuses);

            if (empRecordsWithAllowedEmpStatAndEsmType?.Count == 0) return result;

            foreach (var learningDelivery in learner.LearningDeliveries)
            {
                //check if the LDFam type and code are allowed for this LD.
                var isValidFamCodeAndType = learningDelivery.LearningDeliveryFAMs.Any(ldFam =>
                    ldFam.LearnDelFAMCode != _FamCodeShouldNotBe && ldFam.LearnDelFAMType == _allowedFamType);

                if(!isValidFamCodeAndType) continue;

                //find matching employment record for the learningdelivery
                var matchedEmployementRecord =
                    _dd28PickMatchingEmpRecord.Evaluate(new DD28SubModel()
                    {
                        LearnerEmploymentStatusObj = empRecordsWithAllowedEmpStatAndEsmType,
                        LearningDeliveryObject = learningDelivery
                    });

                if (matchedEmployementRecord == null) continue;
            
                //if any matched emploment record is found then return
                result = "Y";
                return result;
            }
            //if it reaches here then result is N
            return result;
        }
    }

   
}
