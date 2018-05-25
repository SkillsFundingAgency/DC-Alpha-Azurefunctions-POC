using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Collections.Generic;
using System.Threading;

namespace FunctionApp1
{
    public static class OPARuleOrchestrator
    {
        [FunctionName("OPARuleOrchestrator")]
        public static async Task<List<BusinessRules.RuleResponse>> Run(
            [OrchestrationTrigger] DurableOrchestrationContext context)
        {


            var data = await context.CallActivityAsync<BusinessRules.CustomersData>("XmlDeserialiser");


            var tasks = new List<Task<BusinessRules.RuleResponse>>();

            


                for (int i = 0; i < 600; i++)
                {
                    tasks.Add(context.CallActivityAsync<BusinessRules.RuleResponse>("Rule1", data));
                    //  tasks.Add(context.CallActivityAsync<BusinessRules.RuleResponse>("Rule2", data));
                    //tasks.Add(context.CallActivityAsync<BusinessRules.RuleResponse>("Rule2", data));
                }


                await Task.WhenAll(tasks);
            return new List<BusinessRules.RuleResponse>();

              var result = tasks.Select(x => x.Result).ToList();
              return result;

            //return null;
        }

       
    }
}
