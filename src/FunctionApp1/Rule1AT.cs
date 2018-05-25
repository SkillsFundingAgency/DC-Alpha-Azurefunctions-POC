using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace FunctionApp1
{
    public static class Rule1AT
    {
        [FunctionName("Rule1")]
        public static async Task<BusinessRules.RuleResponse> Run([ActivityTrigger] BusinessRules.CustomersData data)
        {
            //BusinessRules.IRule rule = new BusinessRules.Rule1();
            //return rule.Execute(data);

           // await Task.Delay(10);
            return new BusinessRules.RuleResponse()
            {
                IsValid = true,
                RuleName = "Rule1"


            };

        }
    }
}
