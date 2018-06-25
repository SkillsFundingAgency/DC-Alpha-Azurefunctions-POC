using System;
using System.Threading.Tasks;

namespace ConsoleJobManager
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                var jobMgr = new LoopJobManager();
                await jobMgr.Execute();
            }).GetAwaiter().GetResult();

            Console.ReadLine();
        }
    }
}
