using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types
using System.Configuration;
using System.Collections.Concurrent;

namespace FunctionsStats
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["StorageConnectionString"]);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("DurableFunctionsHubHistory");

            while (true)
            {



                ConcurrentDictionary<string, DateTime?> partitionKeys = new ConcurrentDictionary<string, DateTime?>();
                Parallel.ForEach(table.ExecuteQuery(new TableQuery()), entity =>
                {
                    partitionKeys.TryAdd(entity.PartitionKey, entity.Properties["_Timestamp"].DateTime);
                });


                var stats = new Dictionary<string, TimeSpan>();

                foreach (var partitionKey in partitionKeys.OrderBy(x=>x.Value))
                {

                    table = tableClient.GetTableReference("DurableFunctionsHubHistory");

                    // Construct the query operation for all customer entities where PartitionKey="Smith".
                    var query = new TableQuery().Where(
                        TableQuery.CombineFilters(
                            TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey.Key),
                            TableOperators.And,
                            TableQuery.GenerateFilterCondition("Name", QueryComparisons.Equal, "OPARuleOrchestrator")
                            )
                        );

                    var jobStartedRows = table.ExecuteQuery(query).ToList();

                    var startTime = jobStartedRows[0].Properties["_Timestamp"].DateTime;

                    var secondQuery = new TableQuery().Where(
                        TableQuery.CombineFilters(
                            TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey.Key),
                            TableOperators.And,
                            TableQuery.GenerateFilterCondition("OrchestrationStatus", QueryComparisons.Equal, "Completed")
                            )
                        );

                    var jobEndedRows = table.ExecuteQuery(secondQuery).ToList();
                    if (jobEndedRows.Count == 0) continue;

                    var endTime = jobEndedRows[0].Properties["_Timestamp"].DateTime;

                    var differenceInTimespan = endTime - startTime;

                    stats.Add(partitionKey.Key, differenceInTimespan.Value);

                }
                Console.WriteLine("**************************************");
                Console.WriteLine("Job executiion partition key,     Time(in seconds),   Time(in milliseconds)");

                foreach (var item in stats)
                {

                    Console.WriteLine(item.Key + ",       " + item.Value.Seconds + ",             " + item.Value.Milliseconds);

                }
                Console.WriteLine("**************************************");
                Console.ReadLine();
                //// Print the fields for each customer.
                //foreach (CustomerEntity entity in table.ExecuteQuery(query))
                //{
                //    Console.WriteLine("{0}, {1}\t{2}\t{3}", entity.PartitionKey, entity.RowKey,
                //        entity.Email, entity.PhoneNumber);
                //}
            }
        }

        public class DurableFuncHubHistoryModel
        {
            public string PartitionKey { get; set; }
            public string Timestamp { get; set; }
            public string EventId { get; set; }
            public string IsPlayed { get; set; }
            public string _Timestamp { get; set; }
            public string EventType { get; set; }
            public string ExecutionId { get; set; }
            public string Name { get; set; }
            public string Version { get; set; }
            public string Input { get; set; }
            public string Result { get; set; }
            public string OrchestrationStatus { get; set; }


        }
    }
}
