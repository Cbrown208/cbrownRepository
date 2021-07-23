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

	public class RmqQueueProperties
	{
		public string name { get; set; }
		public string vhost { get; set; }
		public int consumers { get; set; }
		public int messages_ready { get; set; }
	}

	public class RmqConsumerProperties
	{
		public string active { get; set; }
		public RmqQueueProperties queue { get; set; }
	}
}
