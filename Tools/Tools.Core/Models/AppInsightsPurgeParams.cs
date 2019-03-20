using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
