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
			//RunSchemaManager();
			RunCopyManager();
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


			//dbCopyManager.CreateIdempotentInsertScript();

			/***** Bulk Copy *****/
			/***** Get Mappings File Setup *****/
			var sourceTableName = "CPPKG_PARAMS_DTLS";
			var destinationTableName = "CpPkgParamsDtls";

			sourceTableName = "CPPACKAGE_PARAMS";
			destinationTableName = "CpPackageParams";

			sourceTableName = "BILLPAYRCLASSSMRY";
			destinationTableName = "BillPayrClassSmry";

			var columnsToSkip = new List<string> { "MTIME" };

			//dbCopyManager.CreateBulkCopyMappingJson(sourceTableName, destinationTableName, columnsToSkip);
			dbCopyManager.CreateBulkCopyMappingJson(sourceTableName, destinationTableName, null);

			Console.WriteLine("Would you like to Start Copy? Y:Yes N:no");
			var input = Console.ReadLine();

			if (input != null && input.ToLower() == "y")
			{
				Console.WriteLine("Would you like to use Temp Mappings file? Y:Yes N:no");
				var mappingInput = Console.ReadLine();
				var useTempMappings = mappingInput != null && mappingInput.ToLower() == "y";

				/***** Copy *****/
				dbCopyManager.BulkCopyDatabaseData(useTempMappings);
				Console.ReadLine();
			}
		}
	}
}
