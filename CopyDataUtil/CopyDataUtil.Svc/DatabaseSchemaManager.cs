using System.Collections.Generic;

namespace CopyDataUtil.Svc
{
	using CopyDataUtil.Core.Models.DbModels;
	using CopyDataUtil.DataAccess;

	using Newtonsoft.Json;

	public class DatabaseSchemaManager
	{
		private readonly string DbConnectionString = "Data Source=RCM41VSPASDB02.medassets.com;Initial Catalog = SC_Centura; Integrated Security = True;";
		private readonly DbContext _inputDb;
		private readonly Dictionary<string,string> AzureColumnDict;

		public DatabaseSchemaManager()
		{
			AzureColumnDict = PopulateAzureColumnDict();
			_inputDb = new DbContext(DbConnectionString);
		}

		public string GetJsonSchemaFormat(string tableName)
		{
			var outputColumns = _inputDb.GetColumnsNamesForTable(tableName);
			var result = new destination { structure = new List<TableStructure>(), tableName = @"[dbo].["+tableName+"]" };
			foreach (var tableColumnInfo in outputColumns)
			{
				var value = AzureColumnDict[tableColumnInfo.Data_Type]; 
				var colInfo = new TableStructure() { name = tableColumnInfo.Column_Name, type = value, };
				result.structure.Add(colInfo);
			}
			var stringResult = JsonConvert.SerializeObject(result);
			return stringResult;
		}

		public Dictionary<string, string> PopulateAzureColumnDict()
		{
			return new Dictionary<string, string>
				           {
					           { "bigint", "Int64" },
					           { "binary", "Byte[]" },
					           { "bit", "Boolean" },
					           { "char", "String" },
					           { "date", "DateTime" },
					           { "datetime", "DateTime" },
					           { "datetime2", "DateTime" },
					           { "datetimeoffset", "DateTimeOffset" },
					           { "decimal", "Decimal" },
					           { "FILESTREAM attribute (varbinary(max))", "Byte[]" },
					           { "Float", "Double" },
					           { "image", "Byte[]" },
					           { "int", "Int32" },
					           { "money", "Decimal" },
					           { "nchar", "String" },
					           { "ntext", "String" },
					           { "numeric", "Decimal" },
					           { "nvarchar", "String" },
					           { "real", "Single" },
					           { "rowversion", "Byte[]" },
					           { "smalldatetime", "DateTime" },
					           { "smallint", "Int16" },
					           { "smallmoney", "Decimal" },
					           { "text", "String" },
					           { "time", "TimeSpan" },
					           { "timestamp", "Byte[]" },
					           { "tinyint", "Int16" },
					           { "uniqueidentifier", "Guid" },
					           { "varbinary", "Byte[]" },
					           { "varchar", "String" },
					           { "xml", "Xml" }
				           };
		}

	}
}
