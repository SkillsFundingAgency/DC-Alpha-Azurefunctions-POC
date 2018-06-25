using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessRules
{
    public class Rule2WithLoop 
    {
        public List<RuleResponse> Execute(CustomersData data)
        {
            var result = new ConcurrentDictionary<int, RuleResponse>();
            Parallel.For(0, data.TotalCount, index =>
            {
                // parse query parameter
                Task.Delay(10);
                result.TryAdd(index, new RuleResponse
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
