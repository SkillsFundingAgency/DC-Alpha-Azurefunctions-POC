using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.Models
{
    public class LearningDelivery
    {
        public string LearnAimRef { get; set; }
        public int AimType { get; set; }
        public int AimSeqNumber { get; set; }
        public DateTime? LearnStartDate { get; set; }
        public DateTime? LearnPlanEndDate { get; set; }
        public string FworkCode { get; set; }
        public string PwayCode { get; set; }
        public string ProgType { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int FundModel { get; set; }


        public List<LearningDeliveryFAM> LearningDeliveryFAMs { get; set; }


    }
    
}
