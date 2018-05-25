using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace FunctionApp1
{
    public static class HttpTriggerRule2WithLoop
    {
        /// <summary>
        /// Loops through all the learners passed in and executes single rule for all those learners.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("HttpTriggerRule2WithLoop")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            //log.Info("C# HTTP trigger function processed a request.");
            var businessRulesObj = new BusinessRules.Rule2WithLoop();

            // Get request body
            NameValueCollection data = req.Content.ReadAsFormDataAsync().Result;
           

            // Set name to query string or body data
            var totalCount =  int.Parse(data[0]);

            var result= businessRulesObj.Execute(new BusinessRules.CustomersData() {
                TotalCount =  totalCount
            });
           

            return req.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
