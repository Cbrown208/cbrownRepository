using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CopyDataUtil.Core.Mappings;
using CopyDataUtil.Core.Models;
using CopyDataUtil.DataAccess;
using Newtonsoft.Json;

namespace CopyDataUtil.Svc
{
	public class DatabaseCopyManager
	{
		private readonly string OutputConnectionString = "Data Source=RCM41VSPASDB02.medassets.com;Initial Catalog = SC_Centura; Integrated Security = True;";
		private readonly DbContext _outputDb;

		public DatabaseCopyManager()
		{
			_outputDb = new DbContext(OutputConnectionString);
		}

		public string CreateIdempotentInsertScript(string sourceConnectionString, string tableName)
		{
			var inputConTempDb = new DbContext(sourceConnectionString);
			var temp = inputConTempDb.GetInsertSetupString(tableName);
			//var tableList = outputdbContext.GetListofTableNames();
			var inputData = inputConTempDb.GetTableData(tableName);

			var columnList = inputConTempDb.GetColumnsNamesForTable(tableName);

			var outputString = inputConTempDb.BuildIdempotentInsertScript(tableName, columnList, inputData);

			foreach (var col in columnList)
			{
				Console.WriteLine(col.Column_Name);
			}
			return outputString;
		}

		public string CreateBulkCopyMappingJson(BulkCopyParams copyParams)
		{
			var sourceConDb = new DbContext(copyParams.SourceConnectionString);
			var destinationConDb = new DbContext(copyParams.DestinationConnectionString);
			var configMappings = new Configuration()
			{
				SourceTable = copyParams.SourceTableName,
				DestinationTable = copyParams.DestinationTableName,
				SourceDestinationColumnMapping = new List<SourceDestinationColumnMapping>()
			};
			var sourceColumnList = sourceConDb.GetColumnsNamesForTable(copyParams.SourceTableName, copyParams.ColumnsToSkip).ToArray();
			var destinationColumnList = destinationConDb.GetColumnsNamesForTable(copyParams.DestinationTableName).ToArray();

			for (var i = 0; i < sourceColumnList.Length; i++)
			{
				var mapping = new SourceDestinationColumnMapping { SourceColumn = sourceColumnList[i].Column_Name };
				if (destinationColumnList.Length + 1 > i)
				{
					mapping.DestinationColumn = destinationColumnList[i].Column_Name;
				}
				else
				{
					mapping.DestinationColumn = "";
				}
				configMappings.SourceDestinationColumnMapping.Add(mapping);
			}
			var jsonOutput = JsonConvert.SerializeObject(configMappings);
			//foreach (var col in configMappings.SourceDestinationColumnMapping)
			//{
			//	Console.WriteLine("Source: "+col.SourceColumn);
			//	Console.WriteLine("Dest:   " + col.DestinationColumn);
			//}
			var path = @"C:\Dev\cbrownRepository\CopyDataUtil\CopyDataUtil.Core\Mappings\SourceDestinationColumnMappings.json";
			var debugPath = Directory.GetCurrentDirectory() + "\\Mappings\\SourceDestinationColumnMappings.json";
			SaveToFile(path, configMappings);
			SaveToFile(debugPath, configMappings);
			Console.WriteLine("Json Config Saved to File");

			var isMappingMissmatched = configMappings.SourceDestinationColumnMapping.FirstOrDefault(x =>
				x.SourceColumn.ToLower() != x.DestinationColumn.ToLower());
			if (isMappingMissmatched != null)
			{
				Console.WriteLine("Warning! Mappings Are Missmatched");
			}
			return jsonOutput;
		}

		public string BulkCopyDatabaseData(bool useTempMappings, BulkCopyParams copyParams, int? facilityId)
		{
			var bulkHelper = new BulkCopyHelper();

			var copyDetails = new BulkCopyDetails()
			{
				SourceConnectionString = copyParams.SourceConnectionString,
				DestinationConnectionString = copyParams.DestinationConnectionString,
				Config = new Configuration()
			};

			var sourceDestinationTableMappings = useTempMappings ? SourceDestinationColumnMapper.GetTempMappings() : SourceDestinationColumnMapper.GetMappings();

			foreach (var mappings in sourceDestinationTableMappings.Configurations)
			{
				copyDetails.Config.SourceDestinationColumnMapping = mappings.SourceDestinationColumnMapping;
				copyDetails.Config.SourceTable = mappings.SourceTable;
				copyDetails.Config.DestinationTable = mappings.DestinationTable;
				copyDetails.Config.StagingTable = mappings.StagingTable;

				var copyTimer = new Stopwatch();
				copyTimer.Start();
				Console.WriteLine("Copy From: " + mappings.SourceTable);
				Console.WriteLine("Copy To:   " + mappings.DestinationTable);
				Console.WriteLine("Copy Starting At: " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt")+ "...");

				bulkHelper.Copy(copyDetails, facilityId);
				copyTimer.Stop();

				Console.WriteLine("Result:     Successful");

				// Format and display the TimeSpan value.
				var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", copyTimer.Elapsed.Hours, copyTimer.Elapsed.Minutes, copyTimer.Elapsed.Seconds,
					copyTimer.Elapsed.Milliseconds / 10);
				Console.WriteLine("RunTime:   " + elapsedTime + Environment.NewLine);
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

		public bool SaveToFile(string path, Configuration configMappings)
		{
			using (StreamWriter file = File.CreateText(path))
			{
				JsonSerializer serializer = new JsonSerializer();
				var configForFile = new RootObject { Configurations = new List<Configuration>() };
				configForFile.Configurations.Add(configMappings);

				serializer.Serialize(file, configForFile);
			}
			return true;
		}
	}
}
