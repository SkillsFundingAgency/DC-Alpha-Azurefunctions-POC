using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace FunctionApp1
{
    public static class HttpStart
    {
        [FunctionName("HttpStart")]
        public static async Task<HttpResponseMessage> Run(
           [HttpTrigger(AuthorizationLevel.Function, methods: "post", Route = "orchestrators/{functionName}")] HttpRequestMessage req,
           [OrchestrationClient] DurableOrchestrationClient starter,
           string functionName,
           TraceWriter log)
        {
            // Function input comes from the request content.
            dynamic eventData = await req.Content.ReadAsAsync<object>();
            string instanceId = await starter.StartNewAsync(functionName, eventData);

            log.Info($"Started orchestration with ID = '{instanceId}'.");
            var status = await starter.GetStatusAsync(instanceId);
            while (status?.Output == null)
            {
                status = await starter.GetStatusAsync(instanceId);
            }

            return req.CreateResponse(HttpStatusCode.OK, status.Output);

            // return starter.CreateCheckStatusResponse(req, instanceId); -
        }
    }
}
