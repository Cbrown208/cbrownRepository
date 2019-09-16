using System;

namespace Tools.Core.Models
{
	public class Services
	{
		public int Id { get; set; }
		public int Environment { get; set; }
		public string ServerName { get; set; }
		public string ServiceName { get; set; }
		public string ServiceDescription { get; set; }
		public string StartMode { get; set; }
		public bool Status { get; set; }
		public string Results { get; set; }
		public DateTime LastUpdated { get; set; }
		public bool IsActive { get; set; }
	}
}
