using System;

namespace Tools.Core.Models
{
	public class LogStats
	{
		public LogStats()
		{
			CreatedOn= DateTime.Now.ToLocalTime();
		}
		public int Id { get; set; }
		public LogStatsDetails Documents { get; set; }
		public LogStatsDetails MyScripts { get; set; }
		public LogStatsDetails Pictures { get; set; }
		public LogStatsDetails Music { get; set; }
		public DateTime CreatedOn { get; set; }
	}
}
