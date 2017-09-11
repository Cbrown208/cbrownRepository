using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using MassTransit3_Example;
using RabbitMQ.Client;

namespace QueueTools.RabbitMQ
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

        public RabbitMqTransportManager(string host, int port, string userName, string password, string vhost)
        {
	        var factory = new ConnectionFactory();

			if (vhost != null)
	        {
		        factory = new ConnectionFactory
		        {
			        UserName = userName,
			        Password = password,
			        HostName = host,
			        Port = port,
			        VirtualHost = "PAS"
		        };
	        }
	        else
	        {
				factory = new ConnectionFactory
				{
					UserName = userName,
					Password = password,
					HostName = host,
					Port = port,
				};
			}
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

		public void Dispose()
        {
            _management.Dispose();
        }
    }

  
}
