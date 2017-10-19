using System;

namespace DapperTesting.Models
{
	public class HealthChecks
	{
		public int Id { get; set; }
		public int Environment { get; set; }
		public int ProductName { get; set; }
		public string SiteName { get; set; }
		public string ServerName { get; set; }
		public int Port { get; set; }
		public string Appendix { get; set; }
		public bool Status { get; set; }
		public string Results { get; set; }
		public DateTime LastUpdated { get; set; }
		public bool IsActive { get; set; }
	}
}
