using System.Collections.Generic;

namespace BusinessRules.POC.Interfaces
{
    public class ValidationResult
    {
        public string RuleName { get; set; }
        public bool IsValid { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }

    }
}