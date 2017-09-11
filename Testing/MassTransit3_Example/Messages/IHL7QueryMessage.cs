using System;

namespace MedAssets.PatientFinancial.Contracts
{
	public interface IHL7QueryMessage
	{
		int ClientId { get; set; }
		int MedAssetsId { get; set; }
		string Message { get; set; }
		DateTimeOffset Timestamp { get; set; }
	}

	public class HL7QueryMessage : IHL7QueryMessage
	{
		public int ClientId { get; set; }
		public int MedAssetsId { get; set; }
		public string Message { get; set; }
		public DateTimeOffset Timestamp { get; set; }
	}
}
