using System.Collections.Generic;

namespace Tools.SendingEmail.Models
{
	public class LogStatsDetails
	{
		public CopyDetails Dirs { get; set; }
		public CopyDetails Files { get; set; }
		public CopyDetails Bytes { get; set; }
		public CopyDetails Times { get; set; }
		public List<string> FilesAdded { get; set; }
		public List<string> FilesUpdated { get; set; }
		public string[] EndedTime { get; set; }
	}
}
