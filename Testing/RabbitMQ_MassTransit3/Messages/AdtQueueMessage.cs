using System;

namespace MedAssets.AMS.Common
{
	public class AdtQueueMessage : IAdtQueueMessage
	{
		public int ClientId { get; set; }
		public string FacilityId { get; set; }
		public long MessageControlId { get; set; }
		public string AccountNumber { get; set; }
		public string Message { get; set; }
		public DateTimeOffset Timestamp { get; set; }
	}

	public interface IAdtQueueMessage
	{
		int ClientId { get; set; }
		string FacilityId { get; set; }
		long MessageControlId { get; set; }
		string AccountNumber { get; set; }
		string Message { get; set; }
		DateTimeOffset Timestamp { get; set; }
	}
}
