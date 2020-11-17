using System;

namespace RabbitMQApiCalls.Models
{
	public class RmqConnection
	{
		public RmqClientProperties client_properties { get; set; }
	}

	public class RmqClientProperties
	{
		public string hostname { get; set; }
		public string process_name { get; set; }
		public DateTime connected { get; set; }
	}
}
