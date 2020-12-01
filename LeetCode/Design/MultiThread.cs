using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeetCode.Design
{
    public class MultiThread
    {
        [Test]
        public void Main()
        {
            // Task factory load api call infor and credential, populate necessary paramesters, segement api calls by time interval, by driver group, by vehicle type etc
            // Usea queue to story info, once a task returns add a new task
            Task[] taskArray = new Task[10];
            for (int i = 0; i < taskArray.Length; i++)
            {
                taskArray[i] = Task.Factory.StartNew((Object obj) =>
                {
                    ApiData data = obj as ApiData;

                    string callId = Guid.NewGuid().ToString();
                    var client = new RestClient("https://apidojo-yahoo-finance-v1.p.rapidapi.com/auto-complete?q=tesla&region=US");
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("x-rapidapi-key", "7fa8c34606msh952c056e9c38ebap1535f0jsn153c2300556a");
                    request.AddHeader("x-rapidapi-host", "apidojo-yahoo-finance-v1.p.rapidapi.com");
                    IRestResponse response = client.Execute(request);

                    data.threadNum = Thread.CurrentThread.ManagedThreadId;
                    data.bytes = response.RawBytes;
                },
                new ApiData()
                {
                    taskId = i,
                    creationTime = DateTime.Now.Ticks
                });
            };
            Task.WaitAll(taskArray);

            // await all tasks and store to azure blob
            foreach (var task in taskArray)
            {
                var data = task.AsyncState as ApiData;
                if (data != null) {
                    Debug.WriteLine("Task #{0} created at {1}, ran on thread #{2}.", data.taskId, data.creationTime, data.threadNum);
                    using (FileStream fs = File.Create($@"C:\Users\dshi\Documents\LeetCode\LeetCode\Design\Data\Task {data.taskId}-{data.creationTime}-{data.threadNum}.json"))
                    {
                        // Add some information to the file.
                        fs.Write(data.bytes, 0, data.bytes.Length);
                    }
                }
            }
        }
    }

    public class ApiData
    {
        public int taskId { get; set; }
        public string id { get; set; }
        public byte[] bytes { get; set; }
        public long creationTime { get; set; }
        public int threadNum { get; set; }
    }
}
