using System;
using System.Collections.Async;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleJobManager
{
    /// <summary>
    /// This calls the Azure functions - httptriggers async 
    /// </summary>
    class JobManager
    {
        public async Task<List<HttpResponseMessage>> Execute()
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 1000;

            var tasksForUser1 = new List<Task<HttpResponseMessage>>();
            var tasksForUser2 = new List<Task<HttpResponseMessage>>();

            using (var client = new HttpClient())
            {
                Stopwatch sw = new Stopwatch();
               

                var learnersTasks = new List<List<Task<HttpResponseMessage>>>();

                Console.Write("Please enter number of learners: (10 or 100 or 500 etc):");
                var readFromUser = Console.ReadLine();
                int totalLearners;
                int.TryParse(readFromUser, out totalLearners);

                Console.WriteLine("Request Process Started!");


                

                var allReqts = new ConcurrentDictionary<Guid, List<HttpRequestMessage>>();

                var reqs = new ConcurrentDictionary<Guid, HttpRequestMessage>();
                var reqsListNotThreadSafe = new List<HttpRequestMessage>();

                int counter = 0;
                Parallel.For(0, totalLearners,  (index) =>
                 {
                     var tasksPerLearner = new List<HttpRequestMessage>();
                     for (int i = 0; i < 600; i++)
                     {
                         counter++;
                         //allTasks.TryAdd(counter, client.SendAsync(CreateRequestObject()));
                         tasksPerLearner.Add(CreateRequestObject());
                     }

                     allReqts.TryAdd(Guid.NewGuid(), tasksPerLearner);
                           
                     //learnersTasks.Add(tasksForUser);
                     
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

                    Console.WriteLine("Request waiting for the learners tasks to complete! - Total requests: " + allReqts.Count() );
                    //Parallel.ForEach(learnersTasks, async (tasks) => await Task.WhenAll(tasks));

                    // await learnersTasks.ParallelForEachAsync(async tasks => await Task.WhenAll(tasks));
                    var reqList = reqs.Select(x => x.Value).ToList();
                    var responses = new List<HttpResponseMessage>();
                    foreach (var item in allReqts)
                    {
                        responses.AddRange(await item.Value.DownloadAsync(1000));
                    };

                   // var responses = await reqList.DownloadAsync(1000);

                    //await Task.WhenAll(allTasks.ToList().Select(x=>x.Value));
                    //foreach (var tasks in learnersTasks)
                    //{
                    //    await Task.WhenAll(tasks);
                    //}


                    sw.Stop();
                    // var result1 = responses[0];


                   // Console.WriteLine("Total requests with success status:" + responses.Count(x => x.IsSuccessStatusCode));
                //    Console.WriteLine("Total requests with Error status:" + responses.Count(x => !x.IsSuccessStatusCode));


                    // result.ForEach(x => Console.WriteLine(x.ToString()));
                    Console.WriteLine("Request Completed for all learners!");
                    Console.WriteLine("Time it took - in seconds: " + sw.Elapsed + " , in milliseconds: " + sw.ElapsedMilliseconds);

                    return null;

                }
                catch (Exception ex)
                {

                    Console.WriteLine("Error occured while making http requests");
                    Console.WriteLine("Error details: " + ex.ToString());

                }

                return null;


              
            }

            
        }

      

        public HttpRequestMessage CreateRequestObject()
        {
            var url = "https://sai-functionapppoc.azurewebsites.net/api/HttpTriggerRule2";
            var url2 = "https://sai-functionapppoc.azurewebsites.net/api/HttpTriggerRule2WithLoop";
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Post
            };

            request.Headers.Add("x-functions-key", "qZwaXP1SL31HvpvGV5QtlX5HpLw5oiipTjJWzehLFxGGeKd6VAR4eA==");
            //request.Headers.Add("x-functions-key", "JFrswP8sSv6pVlDV97wL0LHXcLkXzMiVYoz4Wwz7kla6u2ZeUp5wYQ==");
            
            return request;
        }
    }

    public static class exten
    {

        public static async Task<List<HttpResponseMessage>> DownloadAsync(this List<HttpRequestMessage> reqs, int maxDownloads)
        {
            var concurrentQueue = new ConcurrentQueue<HttpResponseMessage>();

            using (var semaphore = new SemaphoreSlim(maxDownloads))
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout =  TimeSpan.FromMinutes(15);
                var tasks = reqs.Select(async (req) =>
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        var data = await httpClient.SendAsync(req);
                         concurrentQueue.Enqueue(data);
                       
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });

                await Task.WhenAll(tasks.ToArray());
            }
            return concurrentQueue.ToList();
        }

    }

}
