using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace FunctionApp1
{
    public static class HttpTriggerRule2
    {
        [FunctionName("HttpTriggerRule2")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // parse query parameter
            await Task.Delay(10);
            var result= new BusinessRules.RuleResponse()
            {
                IsValid = true,
                RuleName = "Rule2"
            };

            return req.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
