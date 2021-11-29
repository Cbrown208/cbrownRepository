using Newtonsoft.Json;

namespace QueueTools.RabbitMQ
{
	public class QueueInfo
	{
		public string Name { get; set; }
		public int Consumers { get; set; }
		public int Messages { get; set; }
		public bool Durable { get; set; }
	}

	public class ExchangeInfo
	{
		public ExchangeArgumentsInfo arguments { get; set; }
		public bool auto_delete { get; set; }
		public bool durable { get; set; }
		public string name { get; set; }
		public string type { get; set; }
		public string vhost { get; set; }
		public string policy { get; set; }

	}

	public class ExchangeArgumentsInfo
	{
		[JsonProperty("x-expires")]
		public int expires { get; set; }
	}
}
