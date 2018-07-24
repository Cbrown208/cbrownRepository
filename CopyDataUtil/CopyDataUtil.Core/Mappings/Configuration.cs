using System.Collections.Generic;

namespace CopyDataUtil.Core.Mappings
{
	public class Configuration
	{
		public Configuration()
		{
			SourceDestinationColumnMapping = new List<SourceDestinationColumnMapping>();
		}
		public string SourceTable { get; set; }
		public string DestinationTable { get; set; }
		public string StagingTable { get; set; }
		public List<SourceDestinationColumnMapping> SourceDestinationColumnMapping { get; set; }
	}
}
