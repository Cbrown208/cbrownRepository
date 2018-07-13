namespace CopyDataUtil.Svc
{
	class Program
	{
		static void Main(string[] args)
		{
			var dbCopyManager = new DatabaseCopyManager();

			//Run Update/ String Replace On One Column in a DB Table
			dbCopyManager.StringReplaceOnColumn("CpPackageDef", "DESCRIPTION", "SYSKEY");

			//dbCopyManager.BulkCopyDatabaseData();
			//dbCopyManager.CopyDataBaseData();
		}
	}
}
