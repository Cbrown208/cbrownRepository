using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using CopyDataUtil.Core.Mappings;
using CopyDataUtil.Core.Models;
using CopyDataUtil.DataAccess;
using Newtonsoft.Json;

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

		public string CreateIdempotentInsertScript()
		{
			var inputConTempDb = new DbContext(@"Data Source = LEWVQCMGDB02.nthrivenp.nthcrpnp.com\VAL_GLOBAL01; Initial Catalog = CBO_Global; Integrated Security = True;");
			var tableName = "CP_CONFIDENCE_LEVELS";

			var temp = inputConTempDb.GetInsertSetupString(tableName);
			//var tableList = outputdbContext.GetListofTableNames();
			var inputData = inputConTempDb.GetTableData(tableName);

			var columnList = inputConTempDb.GetColumnsNamesForTable(tableName);

			var outputString = inputConTempDb.BuildIdempotentInsertScript(tableName, columnList, inputData);

			foreach (var col in columnList)
			{
				Console.WriteLine(col.Column_Name);
			}
			return "True";
		}

		public string CreateBulkCopyMappingJson(string sourceTableName, string destinationTableName, List<string> columnsToSkip)
		{
			//var sourceConnectionString = @"Data Source = LEWVQCMGDB02.nthrivenp.nthcrpnp.com\VAL_GLOBAL01; Initial Catalog = CBO_Global; Integrated Security = True;";
			//var sourceConnectionString = @"Data Source = LEWVQCMGDB01.nthrivenp.nthcrpnp.com\VAL; Initial Catalog = RMT_CHS_CooperHlthSys; Integrated Security = True;";
			var sourceConnectionString = @"Data Source = LEWVQCMGDB01.nthrivenp.nthcrpnp.com\VAL; Initial Catalog = RMT_CHSC_Contract; Integrated Security = True;";
			var destinationConnectionString = @"Data Source = RCM41VSPASDB02.medassets.com; Initial Catalog =SC_Centura; Integrated Security = True;";
			//var sourceTableName = "CP_DTPROC";
			//var destinationTableName = "CpDtProc";

			//var columnsToSkip = new List<string> {"MTIME"};

			var souceConDb = new DbContext(sourceConnectionString);
			var destinationConDb = new DbContext(destinationConnectionString);
			var configMappings = new Configuration()
			{
				SourceTable = sourceTableName,
				DestinationTable = destinationTableName,
				SourceDestinationColumnMapping = new List<SourceDestinationColumnMapping>()
			};
			var sourceColumnList = souceConDb.GetColumnsNamesForTable(sourceTableName,columnsToSkip).ToArray();
			var destinationColumnList = destinationConDb.GetColumnsNamesForTable(destinationTableName).ToArray();

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
			return jsonOutput;
		}

		public string BulkCopyDatabaseData(bool useTempMappings)
		{
			//var sourceConnectionString = @"Data Source = LEWVQCMGDB02.nthrivenp.nthcrpnp.com\VAL_GLOBAL01; Initial Catalog = CBO_Global; Integrated Security = True;";
			//var sourceConnectionString = @"Data Source = LEWVQCMGDB01.nthrivenp.nthcrpnp.com\VAL; Initial Catalog = RMT_CHS_CooperHlthSys; Integrated Security = True;";
			var sourceConnectionString = @"Data Source = LEWVQCMGDB01.nthrivenp.nthcrpnp.com\VAL; Initial Catalog = RMT_CHSC_Contract; Integrated Security = True;";
			var destinationConnectionString = @"Data Source = RCM41VSPASDB02.medassets.com; Initial Catalog =SC_Centura; Integrated Security = True;";
			var bulkHelper = new BulkCopyHelper();

			var copyDetails = new BulkCopyDetails()
			{
				SourceConnectionString = sourceConnectionString,
				DestinationConnectionString = destinationConnectionString,
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
				Console.WriteLine("Copy From: "+ mappings.SourceTable );
				Console.WriteLine("Copy To:   " + mappings.DestinationTable);
				bulkHelper.Copy(copyDetails);
				copyTimer.Stop();
				Console.WriteLine("Result:     Successful");

				// Format and display the TimeSpan value.
				var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",copyTimer.Elapsed.Hours, copyTimer.Elapsed.Minutes, copyTimer.Elapsed.Seconds,
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

				var configForFile = new RootObject() {Configurations = new List<Configuration>()};
				configForFile.Configurations.Add(configMappings);

				//serialize object directly into file stream
				serializer.Serialize(file, configForFile);
			}
			return true;
		}
	}
}
