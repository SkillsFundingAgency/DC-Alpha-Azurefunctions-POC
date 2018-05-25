using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Threading;

namespace FunctionApp1
{
    public static class HelloWorldAT
    {
        [FunctionName("E1_SayHello")]
        public static string SayHello([ActivityTrigger] string name)
        {
            //Thread.Sleep(10000);
            return new BusinessRules.SayHello().Display(name);
        }
    }
}
