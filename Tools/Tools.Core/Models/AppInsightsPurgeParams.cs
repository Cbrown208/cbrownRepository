using System.Collections.Generic;

namespace Tools.Core.Models
{
	public class AppInsightsPurgeParams
	{
		public AppInsightsPurgeParams()
		{
			filters = new List<AiPurgeParamFilters>();
		}
		public string table { get; set; }
		public List<AiPurgeParamFilters> filters { get; set; }
	}

	public class AiPurgeParamFilters
	{
		public string column { get; set; }
		public string  Operator { get; set; }
		public string value { get; set; }
	}
}
