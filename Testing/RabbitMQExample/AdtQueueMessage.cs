using System;

namespace MedAssets.AMS.Common
{
	public class AdtQueueMessage : IAdtQueueMessage
	{
		public int ClientId { get; set; }
		public string FacilityId { get; set; }
		public string AccountNumber { get; set; }
		public string Message { get; set; }
		public DateTimeOffset SocketTimestamp { get; set; }
		public DateTimeOffset Timestamp { get; set; }
		public Guid? MessageQueueId { get; set; }

	}

	public class MassTransitEnvelope
	{
		public IAdtQueueMessage message { get; set; }
		public string[] MessageType { get; set; }
	}

	public interface IAdtQueueMessage
	{
		int ClientId { get; set; }
		string FacilityId { get; set; }
		string AccountNumber { get; set; }
		string Message { get; set; }
		DateTimeOffset SocketTimestamp { get; set; }
		DateTimeOffset Timestamp { get; set; }
		Guid? MessageQueueId { get; set; }
	}
}
