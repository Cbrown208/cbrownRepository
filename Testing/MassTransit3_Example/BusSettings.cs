namespace MassTransit3_Example
{
	public class BusSettings
	{
		public string ConcurrentConsumerLimit { get; set; }
		public string RetryLimit { get; set; }
		public string IncomingUriString { get; set; }
		public string IncomingQueue { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public ushort HeartBeatInSeconds { get; set; }
		public string OutgoingQueue { get; set; }
	}
}
