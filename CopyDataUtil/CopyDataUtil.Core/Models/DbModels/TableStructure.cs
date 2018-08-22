using System;
using System.Collections.Generic;

namespace CopyDataUtil.Core.Models.DbModels
{
	public class DataFactoryTableSchema
	{
		public TableStructure source { get; set; }
		public TableStructure destination { get; set; }
		public CopyActivity copyActivity { get; set; }
	}

	public class TableStructure
	{
		public TableStructure()
		{
			structure = new List<TableColumnStructure>();
		}
		public List<TableColumnStructure> structure { get; set; }
		public string tableName { get; set; }
	}

	public class TableColumnStructure
	{
		public string name { get; set; }
		public string type { get; set; }
	}

	public class CopyActivity
	{
		public CopyTranslator translator { get; set; }
	}
	public class CopyTranslator
	{
		public string type = "TabularTranslator";
		public Dictionary<string, string> columnMappings { get; set; }
	}
}
