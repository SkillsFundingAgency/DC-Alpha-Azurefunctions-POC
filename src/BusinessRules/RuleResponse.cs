using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules
{
    public class RuleResponse
    {
        public string RuleName { get; set; }
        public bool IsValid { get; set; }
        public string ErrorString { get; set; }
        public int Id { get; set; }

    }
}
