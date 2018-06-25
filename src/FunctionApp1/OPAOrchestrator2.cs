using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using System.Collections.Generic;

namespace FunctionApp1
{
    public static class OPAOrchestrator2
    {
        [FunctionName("OPAOrchestrator2")]
        public static async Task<List<BusinessRules.RuleResponse>> Run(
            [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var data = await context.CallActivityAsync<BusinessRules.CustomersData>("XmlDeserialiser");

            var tasks = new List<Task<BusinessRules.RuleResponse>>();

            for (int i = 0; i < 300; i++)
            {
                tasks.Add(context.CallActivityAsync<BusinessRules.RuleResponse>("Rule1", data));
                // tasks.Add(context.CallActivityAsync<BusinessRules.RuleResponse>("Rule2", data)); -
                // tasks.Add(context.CallActivityAsync<BusinessRules.RuleResponse>("Rule2", data)); -
            }
            await Task.WhenAll(tasks);

            var result = tasks.Select(x => x.Result).ToList();
            return result;
        }
    }
}
