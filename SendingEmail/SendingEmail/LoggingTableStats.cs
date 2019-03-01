using System.Collections.Generic;

namespace SendingEmail
{
    public class LoggingTableStats
    {
        public string DirectorySummary { get; set; }
        public string Total { get; set; }
        public string FilesSummary { get; set; }
        public string EndedOn { get; set; }
        public string bytes { get; set; }
        public string times { get; set; }
		public List<string> FilesAdded { get; set; }
	    public List<string> FilesUpdated { get; set; }
	}
}
