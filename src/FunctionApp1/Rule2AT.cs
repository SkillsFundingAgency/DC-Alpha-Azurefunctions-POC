using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Threading.Tasks;

namespace FunctionApp1
{
    public static class Rule2AT

    {
        [FunctionName("Rule2")]
        public static async Task<BusinessRules.RuleResponse> Run([ActivityTrigger] BusinessRules.CustomersData data)
        {
            await Task.Delay(10);
            return new BusinessRules.RuleResponse()
            {
                IsValid = true,
                RuleName = "Rule2"


            };
        }
    }
}
