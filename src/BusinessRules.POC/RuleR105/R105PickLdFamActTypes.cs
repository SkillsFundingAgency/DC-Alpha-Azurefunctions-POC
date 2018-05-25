using BusinessRules.POC.Enums;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.RuleR105
{
    /// <summary>
    /// returns list of Learning deliveries which has more than one FAM type = ACT 
    /// </summary>
    public class R105PickLdFamActTypes : IR105PickLdFamACTTypes
    {

        public List<LearningDelivery> Evaluate(Learner learner)
        {
            return learner?.LearningDeliveries?.Where(ld =>
            {
                if (ld.LearningDeliveryFAMs == null) return false;
                return ld.LearningDeliveryFAMs.Count(ldFam =>
                    ldFam.LearnDelFAMType == LearningDeliveryFAMTypes.ACT.ToString()) > 1;
            }).ToList();
        }
    }

  
    public interface ILearningDeliveryNoOverlappingDatesRule
    {
        bool Evaluate(LearningDelivery learningDeliverys);
    }

    public interface IR105PickLdFamACTTypes
    {
        List<LearningDelivery> Evaluate(Learner learner);
    }

}
