using Microsoft.Azure.WebJobs;

namespace FunctionApp1
{
    public static class HelloWorldAT
    {
        [FunctionName("E1_SayHello")]
        public static string SayHello([ActivityTrigger] string name)
        {
            // Thread.Sleep(10000); -
            return new BusinessRules.SayHello().Display(name);
        }
    }
}
