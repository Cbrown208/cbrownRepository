using System;
using Newtonsoft.Json;

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
		public DateTime idle_since { get; set; }
		public DateTime IdleSinceLocalDateTime {
			get
			{
				TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
				DateTime idleSince = TimeZoneInfo.ConvertTimeFromUtc(idle_since, cstZone);
				return idleSince;
			} // get method
			set => IdleSinceLocalDateTime = value;
		}
		public BackingQueueStatus backing_queue_status { get; set; }
	}

	public class BackingQueueStatus
	{
		public decimal avg_ingress_rate { get; set; }
		public decimal avg_egress_rate { get; set; }
	}

	public class RmqConsumerProperties
	{
		public string active { get; set; }
		public RmqQueueProperties queue { get; set; }
	}

	public class RmqExchangeProperties
	{
		public ExchangeArguments arguments { get; set; }
		public bool auto_delete { get; set; }
		public bool durable { get; set; }
		public string name { get; set; }
		public string type { get; set; }
		public string vhost { get; set; }
		public string policy { get; set; }

	}

	public class ExchangeArguments
	{
		[JsonProperty("x-expires")]
		public int expires { get; set; }
	}

	public class ShovelRequest
	{
		[JsonProperty("value")]
		public ShovelDetails Details { get; set; }
	}

	public class ShovelDetails
	{
		[JsonProperty("src-protocol")]
		public string SourceProtocol { get; set; }
		[JsonProperty("src-uri")]
		public string SourceUri { get; set; }
		[JsonProperty("src-queue")]
		public string SourceQueue { get; set; }
		[JsonProperty("dest-protocol")]
		public string DestinationProtocol { get; set; }
		[JsonProperty("dest-uri")]
		public string DestinationUri { get; set; }
		[JsonProperty("dest-queue")]
		public string DestinationQueue { get; set; }

	}

}
