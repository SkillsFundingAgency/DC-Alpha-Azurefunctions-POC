using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules
{
    public class Rule1 : IRule
    {
        public RuleResponse Execute(CustomersData data)
        {
            if (data == null)
            {
                return new BusinessRules.RuleResponse()
                {
                    ErrorString = "missing data"
                };
            }

            var isValid = false;
            foreach (var item in data.Customers)
            {
                if (item.FullName == "Blaine Bailey5")
                {
                    isValid = true;
                }
            }

            return new RuleResponse()
            {
                IsValid = isValid,
                RuleName = "Rule1"
            };

        }
    }
}
