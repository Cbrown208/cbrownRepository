using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMQ_MassTransit3.RabbitMQ
{
    public class RabbitMqTransportManager : IDisposable
    {
        private readonly RabbitMqEndpointManagement _management;
		private readonly BusSettings _busSettings = new BusSettings();

        public RabbitMqTransportManager(Uri address)
        {
            var factory = new ConnectionFactory
            {
                UserName = address.UserInfo.Split(':')[0],
                Password = address.UserInfo.Split(':')[1],
                HostName = address.Host,
                Port = address.Port,
                //VirtualHost = address.Segments[0]
            };
            _management = new RabbitMqEndpointManagement(factory.CreateConnection());
        }

        public RabbitMqTransportManager(string host, int port, string userName, string password)
        {
            var factory = new ConnectionFactory
            {
                UserName = userName,
                Password = password,
                HostName = host,
                Port = port,
            };

            _management = new RabbitMqEndpointManagement(factory.CreateConnection());
        }

        public void EnsureQueue(Uri address)
        {
            var name = RabbitMqEndpointAddress.Parse(address).Name;
            _management.BindQueue(name, name, ExchangeType.Fanout, "", null);
        }

        public void EnsureQueue(string name)
        {
            _management.BindQueue(name, name, ExchangeType.Fanout, "", null);
        }

        public List<QueueInfo> GetQueuesNameStartsWith(string busHostUri, string vhost, string nameStartsWith)
        {
            return GetQueuesFor(busHostUri, vhost).Where(x => x.Name.StartsWith(nameStartsWith)).ToList();
        }
        public List<QueueInfo> GetQueuesFor(string busHostUri, string vhost)
        {
            var queues = new List<QueueInfo>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var endpoint = RabbitMqEndpointAddress.Parse(busHostUri);
                var host = endpoint.Uri.Host;
                var user = endpoint.ConnectionFactory.UserName;
                var password = endpoint.ConnectionFactory.Password;
                var byteArray = Encoding.ASCII.GetBytes(String.Format("{0}:{1}", user, password));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(byteArray));

                try
                {
                    var result = client.GetAsync(new Uri(String.Format("http://{0}:15672/api/queues/{1}", host, vhost))).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        queues = result.Content.ReadAsAsync<List<QueueInfo>>().Result;
                    }
                }
                catch (AggregateException ex)
                {
                    throw;
                }
            }
            return queues;
        }

        public List<QueueInfo> GetQueuesFor(string busHostUri, string queue, string vhost = @"%2F")
        {
            var queues = new List<QueueInfo>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var endpoint = RabbitMqEndpointAddress.Parse(busHostUri);
                var host = endpoint.Uri.Host;
                var user = endpoint.ConnectionFactory.UserName;
                var password = endpoint.ConnectionFactory.Password;
                var byteArray = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", user, password));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(byteArray));

                try
                {
                    var result = client.GetAsync(new Uri(String.Format("http://{0}:15672/api/queues/{1}/{2}", host, vhost, queue))).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        queues = result.Content.ReadAsAsync<List<QueueInfo>>().Result;
                    }
                }
                catch (AggregateException ex)
                {
                    throw;
                }
            }
            return queues;
        }

	    public void PurgeQueues(string queueName)
	    {
			_management.Purge(queueName);
		}

		public void DeleteQueues(string queueName)
		{
			_management.Delete(queueName);
		}

		//public static ConnectionFactory GetConnectionFactory()
		//{
		//	var address = new Uri(BusDetails.BusSettings.OutgoingBusSettings.BaseUriString);
		//	var brokerHost = ("amqp://" + BusDetails.BusSettings.OutgoingBusSettings.Username + ":" +
		//					  BusDetails.BusSettings.OutgoingBusSettings.Password + "@" + address.Host + ":5672/");
		//	//var brokerHostUri = "amqp://PAS:PAS@rcm41vqpasapp03:5672/";
		//	return new ConnectionFactory
		//	{
		//		Uri = brokerHost,
		//		RequestedHeartbeat = 10,
		//		AutomaticRecoveryEnabled = true,
		//		NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
		//	};
		//}

		public void Dispose()
        {
            _management.Dispose();
        }
    }

  
}
