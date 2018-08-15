using System;
using System.Collections.Generic;

namespace CopyDataUtil.Svc
{
	using System.Windows.Forms;

	public class Program
	{
		[STAThread]
		public static void Main()
		{
			Console.WriteLine("What would you like to run?");
			Console.WriteLine("1: Bulk Copy Manager");
			Console.WriteLine("2: Create Idempotent Script");
			Console.WriteLine("3: Run Schema Manager");
			Console.WriteLine("q: Quit");
			var input = Console.ReadLine();

			if (!string.IsNullOrWhiteSpace(input) && input.Contains("1"))
			{
				RunCopyManager();
			}
			else if (!string.IsNullOrWhiteSpace(input) && input.Contains("2"))
			{
				CreateIdempotentInsertScript();
			}
			else if (!string.IsNullOrWhiteSpace(input) && input.Contains("3"))
			{
				RunSchemaManager();
			}
			Console.ReadLine();
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
			var tableName = "BillMast";
			var schemaManager = new DatabaseSchemaManager();
			var schemaResult = schemaManager.GetJsonSchemaFormat(tableName);
			Clipboard.SetText(schemaResult);
			Console.WriteLine(schemaResult);
		}

		public static void RunCopyManager()
		{
			var dbCopyManager = new DatabaseCopyManager();
			//Run Update/ String Replace On One Column in a DB Table

			//dbCopyManager.StringReplaceOnColumn("CpPackageDef", "DESCRIPTION", "SYSKEY");


			var sourceConnectionStringContract = @"Data Source = LEWVQCMGDB01.nthrivenp.nthcrpnp.com\VAL; Initial Catalog = RMT_CHSC_Contract; Integrated Security = True;";
			var sourceConnectionStringRepo = @"Data Source = LEWVQCMGDB01.nthrivenp.nthcrpnp.com\VAL; Initial Catalog = RMT_CHS_CooperHlthSys; Integrated Security = True;";
			var serviceCategoryConnectionString = @"Data Source = RCM41VSPASDB02.medassets.com; Initial Catalog =SC_CHSQ; Integrated Security = True;";

			var azureConnectionString = @"Data Source = servicecategorysqldb.database.windows.net;uid=Dragon;password=SetMe*963.; Initial Catalog =SC_CHSQ; Integrated Security = False;";

			var sourceConnectionString = serviceCategoryConnectionString;
			var destinationConnectionString = azureConnectionString;

			//dbCopyManager.CreateIdempotentInsertScript();

			/***** Bulk Copy *****/
			/***** Get Mappings File Setup *****/
			var sourceTableName = "BillMast";
			var destinationTableName = "BillMast";

			var columnsToSkip = new List<string> { "MTIME" };

			//dbCopyManager.CreateBulkCopyMappingJson(sourceTableName, destinationTableName, columnsToSkip, sourceConnectionString, destinationConnectionString);
			dbCopyManager.CreateBulkCopyMappingJson(sourceTableName, destinationTableName, null, sourceConnectionString, destinationConnectionString);

			Console.WriteLine("Would you like to Start Copy? Y:Yes N:no");
			var input = Console.ReadLine();

			if (input != null && input.ToLower() == "y")
			{
				Console.WriteLine("Would you like to use Temp Mappings file? Y:Yes N:no");
				var mappingInput = Console.ReadLine();
				var useTempMappings = mappingInput != null && mappingInput.ToLower() == "y";

				/***** Copy *****/
				dbCopyManager.BulkCopyDatabaseData(useTempMappings, sourceConnectionString, destinationConnectionString);
			}
		}
	}
}
