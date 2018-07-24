using System;
using System.Collections.Generic;

namespace CopyDataUtil.Svc
{
	class Program
	{
		static void Main()
		{
			var dbCopyManager = new DatabaseCopyManager();

			//Run Update/ String Replace On One Column in a DB Table
			//dbCopyManager.StringReplaceOnColumn("CpPackageDef", "DESCRIPTION", "SYSKEY");


			//dbCopyManager.CreateIdempotentInsertScript();

			/***** Bulk Copy *****/
			/***** Get Mappings File Setup *****/
			var sourceTableName = "CpDrg";
			var destinationTableName = "CpDrg";
			var columnsToSkip = new List<string> { "MTIME" };
			
			//dbCopyManager.CreateBulkCopyMappingJson(sourceTableName, destinationTableName, columnsToSkip);
			dbCopyManager.CreateBulkCopyMappingJson(sourceTableName, destinationTableName, null);

			Console.WriteLine("Would you like to Start Copy? Y:Yes N:no");
			var input = Console.ReadLine();

			if (input != null && input.ToLower() == "y")
			{
				/***** Copy *****/
				dbCopyManager.BulkCopyDatabaseData();
				Console.ReadLine();
			}
			//dbCopyManager.CopyDataBaseData();

			
		}
	}
}
