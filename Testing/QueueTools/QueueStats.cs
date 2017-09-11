using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace QueueTools
{
    public class QueueStats
    {
        public static void GetAllQueueStats()
        {
            var address1 = new Uri("rabbitmq://Pas:Pas@RCM41VQPASAPP03/Pas_Adt_Router_1");
            var queue = GetQueuesFor();

            foreach (var q in queue)
            {
                Console.WriteLine("Queue Name: {0}", q.Name);
                Console.WriteLine("Consumer Count: {0}", q.Consumers);
                Console.WriteLine("Message Count: {0}", q.Messages);

            }
        }

        public static List<QueueInfo> GetQueuesFor()
        {
            var queues = new List<QueueInfo>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //var busHostUri = "rabbitmq://PAS:PAS@RCM41VQPASAPP03/";
                //var busHostUri = busSettings.IncomingUriString;
                var host = "RCM41VQPASAPP03"; ;
                var user = "PAS";
                var password = "PAS";
                var byteArray = Encoding.ASCII.GetBytes(user + ":" + password);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(byteArray));
                try
                {
                    var result = client.GetAsync(new Uri("http://" + host + ":15672/api/queues/#")).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        queues = result.Content.ReadAsAsync<List<QueueInfo>>().Result;
                    }
                }
                catch (AggregateException ex)
                {
                    Console.WriteLine($"Not able to get the list of queues for vhost '{host}'.  Message::{ex.InnerException.Message}");
                    throw;
                }
            }
            return queues;
        }
    }

    public class QueueInfo
    {
        public string Name { get; set; }
        public int Consumers { get; set; }
        public int Messages { get; set; }
        public bool Durable { get; set; }
    }
}
