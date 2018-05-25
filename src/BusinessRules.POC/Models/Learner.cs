using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.Models
{
    public class Learner
    {
        public string LearnRefNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<LearningDelivery> LearningDeliveries { get; set; }
        public List<LearnerEmploymentStatus> LearnerEmploymentStatuses { get; set; }

    }
}
