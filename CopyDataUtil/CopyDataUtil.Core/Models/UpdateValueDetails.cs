using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyDataUtil.Core.Models
{
	public class UpdateValueDetails
	{
			public string TableName { get; set; }
			public string ColumnName { get; set; }
			public string UpdateValue { get; set; }
			public string UniqueColumnName { get; set; }
			public string UniqueColumnValue { get; set; }
	}
}
