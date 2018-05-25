using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace BusinessRules
{
    public class Rule2WithLoop 
    {
        public List<RuleResponse> Execute(CustomersData data)
        {
            var result = new ConcurrentDictionary<int, BusinessRules.RuleResponse>();
            Parallel.For(0, data.TotalCount, (index) =>
            {
               
                // parse query parameter
                Task.Delay(10);
                result.TryAdd(index, new BusinessRules.RuleResponse()
                {
                    IsValid = true,
                    RuleName = "Rule2",
                    Id = index
                });
            });

            return result.Select(x=> x.Value).ToList();
          

        }
    }
}
