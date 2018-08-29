﻿using System;
using System.Collections.Generic;
using CopyDataUtil.Core.Mappings;
using System.Windows.Forms;

namespace CopyDataUtil.Svc
{
	public class Program
	{
		[STAThread]
		public static void Main()
		{
			Console.WriteLine("What would you like to run?");
			Console.WriteLine("1: Bulk Copy Manager");
			Console.WriteLine("2: Create Idempotent Script");
			Console.WriteLine("3: Run Schema Manager");
			Console.WriteLine("4: Run Schema Manager All Tables");
			Console.WriteLine("5: Read Schema Mappings");
			Console.WriteLine("q: Quit");
			var input = Console.ReadLine();

			if (!string.IsNullOrWhiteSpace(input) && input.Contains("1"))
			{
				RunCopyManager();
				Console.ReadLine();
			}
			else if (!string.IsNullOrWhiteSpace(input) && input.Contains("2"))
			{
				CreateIdempotentInsertScript();
				Console.ReadLine();
			}
			else if (!string.IsNullOrWhiteSpace(input) && input.Contains("3"))
			{
				RunSchemaManager();
			}
			else if (!string.IsNullOrWhiteSpace(input) && input.Contains("4"))
			{
				RunSchemaManagerAllTables();
			}
			else if (!string.IsNullOrWhiteSpace(input) && input.Contains("5"))
			{
				ReadScMappingFile();
			}
			else if (!string.IsNullOrWhiteSpace(input) && input.ToLower().Contains("q"))
			{
				return;
			}
		}

		public static void CreateIdempotentInsertScript()
		{
			var dbCopyManager = new DatabaseCopyManager();
			var serviceCategoryConnectionString = @"Data Source = servicecategorysqldb.database.windows.net;uid=Dragon;password=SetMe*963.; Initial Catalog =SC_CHSQ; Integrated Security = False;";
			var tableName = "ChangeTrackingVersion";

			var scriptResult = dbCopyManager.CreateIdempotentInsertScript(serviceCategoryConnectionString, tableName);
			Clipboard.SetText(scriptResult);
			Console.WriteLine(scriptResult);
		}

		public static void RunSchemaManager()
		{
			var connectionString = "Data Source = RCM41VSPASDB02.medassets.com; Initial Catalog = SC_CHSQ; Integrated Security = True; ";
			var tableName = "BillPayrClassSmry";
			var schemaManager = new DatabaseSchemaManager();
			var schemaResult = schemaManager.GetJsonSchemaFormat(connectionString,tableName);

			Clipboard.SetText(schemaResult);
			Console.WriteLine(schemaResult);
		}

		public static void RunSchemaManagerAllTables()
		{
			var connectionString = "Data Source = RCM41VSPASDB02.medassets.com; Initial Catalog = SC_CHSQ; Integrated Security = True; ";
			var schemaManager = new DatabaseSchemaManager();
			var schemaResultList = schemaManager.GetAllTablesJsonSchemaFormat(connectionString);

			Clipboard.SetText(schemaResultList);
			Console.WriteLine(schemaResultList);
		}

		public static void ReadScMappingFile()
		{
			var schemaManager = new DatabaseSchemaManager();
			var tableList = schemaManager.ReadFromConfigFile();
			Clipboard.SetText(tableList);
			Console.WriteLine(tableList);
		}

		public static void RunCopyManager()
		{
			var dbCopyManager = new DatabaseCopyManager();
			//Run Update/ String Replace On One Column in a DB Table
			//dbCopyManager.StringReplaceOnColumn("CpPackageDef", "DESCRIPTION", "SYSKEY");

			var CboGlobalConnectionString = @"Data Source = LEWVQCMGDB02.nthrivenp.nthcrpnp.com\VAL_GLOBAL01; Initial Catalog = CBO_Global; Integrated Security = True;";
			var CooperContractConnectionString = @"Data Source = LEWVQCMGDB01.nthrivenp.nthcrpnp.com\VAL; Initial Catalog = RMT_CHSC_Contract; Integrated Security = True;";
			var CooperRepoConnectionString = @"Data Source = LEWVQCMGDB01.nthrivenp.nthcrpnp.com\VAL; Initial Catalog = RMT_CHS_CooperHlthSys; Integrated Security = True;";
			var serviceCategoryConnectionString = @"Data Source = RCM41VSPASDB02.medassets.com; Initial Catalog =SC_CHSQ; Integrated Security = True;";
			var azureConnectionString = @"Data Source = servicecategorysqldb.database.windows.net;uid=Dragon;password=SetMe*963.; Initial Catalog =SC_CHSQ; Integrated Security = False;";

			var copyDetails = new BulkCopyParams
			{
				SourceConnectionString = CooperRepoConnectionString,
				DestinationConnectionString = serviceCategoryConnectionString,
				SourceTableName = "BillMast",
				DestinationTableName = "BillMast",
				ColumnsToSkip = null
			};

			var facilityId = 1412;
			var shouldPurgeData = true;

			Console.WriteLine("Would you like to use Temp Mappings file? Y:Yes N:no");
			var mappingInput = Console.ReadLine();
			var useTempMappings = mappingInput != null && mappingInput.ToLower() == "y";

			/***** Get Mappings File Setup *****/
			if (!useTempMappings)
			{
				Console.WriteLine("Creating Mapping File...");
				//copyDetails.ColumnsToSkip = new List<string> { "MTIME" };
				dbCopyManager.CreateBulkCopyMappingJson(copyDetails);
				Console.WriteLine("Mapping File Created Successfully!");
			}

			Console.WriteLine("Would you like to Start Copy? Y:Yes N:no");
			var input = Console.ReadLine();

			if (input != null && input.ToLower() == "y")
			{
				/***** Copy *****/
				dbCopyManager.BulkCopyDatabaseData(useTempMappings, copyDetails, facilityId, shouldPurgeData);
			}
		}
	}
}
