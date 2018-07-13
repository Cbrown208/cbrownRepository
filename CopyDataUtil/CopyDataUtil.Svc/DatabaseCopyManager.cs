using System;
using System.Collections.Generic;
using CopyDataUtil.Core.Mappings;
using CopyDataUtil.Core.Models;
using CopyDataUtil.DataAccess;

namespace CopyDataUtil.Svc
{
	public class DatabaseCopyManager
	{
		private readonly string InputConnectionString = "Data Source=RCM41VDCPEDB01.medassets.com;Initial Catalog = Pricing.Dev; Integrated Security = True;";
		private readonly string OutputConnectionString = "Data Source=RCM41VSPASDB02.medassets.com;Initial Catalog = SC_Centura; Integrated Security = True;";
		private readonly DbContext _inputDb;
		private readonly DbContext _outputDb;

		public DatabaseCopyManager()
		{
			_inputDb = new DbContext(InputConnectionString);
			_outputDb = new DbContext(OutputConnectionString);
		}
		//private readonly string centuraConnectionString = @" Database = Pricing.Dev; Trusted_Connection = true;";
		//private readonly string serviceCategoryCenturaConnectionString = @"Server = RCM41VSPASDB02.medassets.com; Database = SC_Centura; Trusted_Connection = true;";

		public string CopyDataBaseData()
		{
			var tableName = "CPPACKAGEDEF";

			var temp = _outputDb.GetInsertSetupString(tableName);
			//var tableList = outputdbContext.GetListofTableNames();
			var inputData = _inputDb.GetTableData(tableName);

			var columnList = _outputDb.GetColumnsNamesForTable(tableName);

			_outputDb.InsertTableData(tableName, columnList, inputData);

			foreach (var col in columnList)
			{
				Console.WriteLine(col.Column_Name);
			}
			return "True";
		}

		public string CreateInsertStatement()
		{
			var tableName = "CPPACKAGEDEF";

			var temp = _outputDb.GetInsertSetupString(tableName);
			//var tableList = outputdbContext.GetListofTableNames();
			var inputData = _inputDb.GetTableData(tableName);

			var columnList = _outputDb.GetColumnsNamesForTable(tableName);

			_outputDb.InsertTableData(tableName, columnList, inputData);

			foreach (var col in columnList)
			{
				Console.WriteLine(col.Column_Name);
			}
			return "True";
		}

		public string BulkCopyDatabaseData()
		{
			var sourceDestinationTableMappings = SourceDestinationColumnMapper.GetMappings();

			foreach (var mappings in sourceDestinationTableMappings.Configurations)
			{
				var sourceTable = mappings.SourceTable;
				var destinationTable = mappings.DestinationTable;
				var stagingTable = mappings.StagingTable;

				Console.WriteLine(sourceTable);
				Console.WriteLine(destinationTable);
				Console.WriteLine(stagingTable);
			}

			return "Testing";
		}

		public string StringReplaceOnColumn(string tableName, string targetColumnName, string uniqueColumnName)
		{

			var query = "";
			var updateQueryList = new List<UpdateValueDetails>();

			var tableRowsList = _outputDb.GetAllValuesFromTable(tableName);
			foreach (var values in tableRowsList)
			{
				string newValue = values.DESCRIPTION.ToString();
				if (newValue.Contains(",") || newValue.Contains("'"))
				{
					newValue = newValue.Replace(",", "|");
					newValue = newValue.Replace("'", " ");
					updateQueryList.Add(new UpdateValueDetails
					{
						TableName = tableName,
						ColumnName = targetColumnName,
						UpdateValue = newValue,
						UniqueColumnName = uniqueColumnName,
						UniqueColumnValue = values.SYSKEY
					});
				}
			}
			_outputDb.UpdateValueInTable(updateQueryList);
			return query;
		}
	}
}
