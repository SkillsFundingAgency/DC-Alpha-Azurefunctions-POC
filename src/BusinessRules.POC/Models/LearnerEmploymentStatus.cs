using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.Models
{
    public class LearnerEmploymentStatus
    {
        public int EmpStat { get; set; }
        public DateTime? DateEmpStatApp { get; set; }
        public int EmpId { get; set; }
        public List<EmploymentStatusMonitoring> EmploymentStatusMonitorings { get; set; }

    }
}
