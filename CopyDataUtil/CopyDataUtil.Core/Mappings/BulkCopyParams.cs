using System.Collections.Generic;

namespace CopyDataUtil.Core.Mappings
{
	public class BulkCopyParams
	{
		public BulkCopyParams()
		{
			ColumnsToSkip = new List<string>();
		}
		public string SourceConnectionString { get; set; }
		public string DestinationConnectionString { get; set; }
		public string SourceTableName { get; set; }
		public string DestinationTableName { get; set; }
		public List<string> ColumnsToSkip { get; set; }
	}
}
