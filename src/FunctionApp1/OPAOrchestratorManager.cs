using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using System.Collections.Generic;

namespace FunctionApp1
{
    public static class OPAOrchestratorManager
    {
        [FunctionName("OPAOrchestratorManager")]
        public static async Task<List<BusinessRules.RuleResponse>> Run(
            [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var data = await context.CallActivityAsync<BusinessRules.CustomersData>("XmlDeserialiser");
            var tasks = new List<Task<List<BusinessRules.RuleResponse>>>();
            var firstOrchestratorTask = context.CallSubOrchestratorAsync<List<BusinessRules.RuleResponse>>("OPARuleOrchestrator");
            var secondOrchestratorTask = context.CallSubOrchestratorAsync<List<BusinessRules.RuleResponse>>("OPAOrchestrator2");

            tasks.Add(firstOrchestratorTask);
            tasks.Add(secondOrchestratorTask);

            var result1 = await firstOrchestratorTask;
            // var result2 =  await secondOrchestratorTask; -

            // result1.AddRange(result2); -
            // return result1; -
            return result1;

            // await Task.WhenAll(tasks); -

            // var resultFromOrchestrators = tasks.Select(x=> x.Result).ToList(); -

            // var result = new List<BusinessRules.RuleResponse>(); -
            //foreach (var item in resultFromOrchestrators)
            // {
            //     result.AddRange(item);
            // }

            // return result;

            // return null;
        }
    }
}
