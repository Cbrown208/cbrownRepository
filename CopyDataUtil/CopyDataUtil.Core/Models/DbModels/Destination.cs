using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyDataUtil.Core.Models.DbModels
{
	public class destination
	{
		public List<TableStructure> structure { get; set; }
		public string tableName { get; set; }
	}
}
