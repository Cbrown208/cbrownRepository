using System.Collections.Generic;
using System;
using System.Linq;
using CopyDataUtil.Core.Mappings;
using CopyDataUtil.Core.Models.DbModels;
using CopyDataUtil.DataAccess;
using Newtonsoft.Json;
using NLog;

namespace CopyDataUtil.Svc
{
	public class DatabaseSchemaManager
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		private readonly Dictionary<string, string> _azureColumnDict;

		public DatabaseSchemaManager()
		{
			_azureColumnDict = PopulateAzureColumnDict();
		}

		public string GetJsonSchemaFormat(string connectionString, string tableName)
		{
			var inputDb = new DbContext(connectionString);
			var outputColumns = inputDb.GetColumnsNamesForTable(tableName);

			var schemaList = new List<DataFactoryTableSchema>();
			var result = new TableStructure { structure = new List<TableColumnStructure>(), tableName = @"[dbo].[" + tableName + "]" };
			var lastColumnInList = outputColumns.LastOrDefault();
			var dictTest = new Dictionary<string, string>();
			if (lastColumnInList == null)
			{
				Logger.Error("GetJsonSchemaFormat - Could not find Last Column in List");
				throw new ApplicationException("Could not find Last Column in List");
			}

			foreach (var tableColumnInfo in outputColumns)
			{
				var value = _azureColumnDict[tableColumnInfo.Data_Type];
				var colInfo = new TableColumnStructure() { name = tableColumnInfo.Column_Name, type = value, };
				result.structure.Add(colInfo);
				dictTest.Add(tableColumnInfo.Column_Name, tableColumnInfo.Column_Name);
			}

			var copySchema = new DataFactoryTableSchema
			{
				source = result,
				destination = result,
				copyActivity = new CopyActivity { translator = new CopyTranslator { columnMappings = dictTest } }
			};
			schemaList.Add(copySchema);

			var stringResult = JsonConvert.SerializeObject(schemaList, Formatting.Indented);
			return stringResult;
		}

		public string ReadFromConfigFile()
		{
			var mappingList = SourceDestinationColumnMapper.GetServiceCategoryMappings();
			var tableList = "";
			foreach (var tableSchema in mappingList)
			{
				var formattedTableName = tableSchema.source.tableName.Replace("[dbo].[", "");
				formattedTableName=  formattedTableName.Replace("]", "");
				tableList = tableList + formattedTableName + Environment.NewLine;
			}

			return tableList;
		}

		public string GetAllTablesJsonSchemaFormat(string connectionString)
		{
			var inputDb = new DbContext(connectionString);
			var schemaList = new List<DataFactoryTableSchema>();

			var tableList = inputDb.GetListofTableNames();

			foreach (var table in tableList)
			{
				var outputColumns = inputDb.GetColumnsNamesForTable(table.Table_Name);
				var result = new TableStructure
				{
					structure = new List<TableColumnStructure>(),
					tableName = @"[dbo].[" + table.Table_Name + "]"
				};
				var lastColumnInList = outputColumns.LastOrDefault();
				var dictTest = new Dictionary<string, string>();
				if (lastColumnInList == null)
				{
					Logger.Error("GetJsonSchemaFormat - Could not find Last Column in List");
					throw new ApplicationException("Could not find Last Column in List");
				}

				foreach (var tableColumnInfo in outputColumns)
				{
					var value = _azureColumnDict[tableColumnInfo.Data_Type];
					var colInfo = new TableColumnStructure() {name = tableColumnInfo.Column_Name, type = value,};
					result.structure.Add(colInfo);
					dictTest.Add(tableColumnInfo.Column_Name, tableColumnInfo.Column_Name);
				}

				var copySchema = new DataFactoryTableSchema
				{
					source = result,
					destination = result,
					copyActivity = new CopyActivity {translator = new CopyTranslator {columnMappings = dictTest}}
				};
				schemaList.Add(copySchema);
			}
			var stringResult = JsonConvert.SerializeObject(schemaList, Formatting.Indented);
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
