using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleJobManager
{
    class Program
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
