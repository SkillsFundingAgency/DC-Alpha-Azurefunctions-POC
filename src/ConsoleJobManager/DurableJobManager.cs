using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleJobManager
{
    public sealed class DurableJobManager
    {
        public async Task<List<HttpResponseMessage>> Execute()
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 1000;

            var tasksForUser1 = new List<Task<HttpResponseMessage>>();
            var tasksForUser2 = new List<Task<HttpResponseMessage>>();

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(5);
                Stopwatch sw = new Stopwatch();

                var learnersTasks = new List<List<Task<HttpResponseMessage>>>();

                Console.Write("Please enter number of learners: (10 or 100 or 500 etc):");
                var readFromUser = Console.ReadLine();
                int totalLearners;
                int.TryParse(readFromUser, out totalLearners);

                Console.WriteLine("Request Process Started!");

                var allTasks = new ConcurrentDictionary<int, Task<HttpResponseMessage>>();

                var reqs = new ConcurrentDictionary<Guid, HttpRequestMessage>();
                var reqsListNotThreadSafe = new List<HttpRequestMessage>();

                int counter = 0;
                Parallel.For(0, totalLearners, (index) =>
                {
                    var tasksForUser = new List<Task<HttpResponseMessage>>();
                  
                    // allTasks.TryAdd(counter, client.SendAsync(CreateRequestObject()));
                    reqs.TryAdd(Guid.NewGuid(), CreateRequestObject());

                    // learnersTasks.Add(tasksForUser);
                });

                //for (int i = 0; i < totalLearners; i++)
                //{
                //    for (int j = 0; j < 600; j++)
                //    {
                //        reqsListNotThreadSafe.Add(CreateRequestObject());
                //    }
                //}

                try
                {
                    sw.Start();

                    Console.WriteLine("Request waiting for the learners tasks to complete! - Total requests: " + reqs.Count);
                    // Parallel.ForEach(learnersTasks, async (tasks) => await Task.WhenAll(tasks));

                    // await learnersTasks.ParallelForEachAsync(async tasks => await Task.WhenAll(tasks));
                    var reqList = reqs.Select(x => x.Value).ToList();
                    var responses = await reqList.DownloadAsync(1000);

                    // await Task.WhenAll(allTasks.ToList().Select(x=>x.Value));
                    // foreach (var tasks in learnersTasks)
                    // {
                    //      await Task.WhenAll(tasks);
                    // }

                    sw.Stop();
                    // var result1 = responses[0];

                    Console.WriteLine("Total requests with success status:" + responses.Count(x => x.IsSuccessStatusCode));
                    Console.WriteLine("Total requests with Error status:" + responses.Count(x => !x.IsSuccessStatusCode));

                    // result.ForEach(x => Console.WriteLine(x.ToString()));
                    Console.WriteLine("Request Completed for all learners!");
                    Console.WriteLine("Time it took - in seconds: " + sw.Elapsed + " , in milliseconds: " + sw.ElapsedMilliseconds);

                    return null;
                }
                catch (Exception ex)
                {
                    sw.Stop();

                    Console.WriteLine("Error occured while making http requests");
                    Console.WriteLine("Error details: " + ex.ToString());
                    Console.WriteLine("Elapsed time:" + sw.Elapsed);
                }

                return null;
            }
        }

        private HttpRequestMessage CreateRequestObject()
        {
            var url = "https://sai-functionapppoc.azurewebsites.net/api/orchestrators/OPARuleOrchestrator";
            var url2 = "https://sai-functionapppoc.azurewebsites.net/api/HttpTriggerRule2WithLoop";
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Post,
            };

            request.Headers.Add("x-functions-key", "UvTYOFSHLfCBImU1c9NFemBpkvxYsgtZNh4fleMT1DILCBq115Lv7w==");
            // request.Headers.Add("x-functions-key", "JFrswP8sSv6pVlDV97wL0LHXcLkXzMiVYoz4Wwz7kla6u2ZeUp5wYQ==");

            return request;
        }
    }
}