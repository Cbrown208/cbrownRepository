using System;

namespace Tools.Core.Models
{
	public class Servers
	{
		public int Id { get; set; }
		public int Environment { get; set; }
		public string ServerName { get; set; }
		public string ServerDescription { get; set; }
		public string Ip { get; set; }
		public bool Status { get; set; }
		public string Results { get; set; }
		public DateTime? LastUpdated { get; set; }
		public bool IsActive { get; set; }
	}
}
