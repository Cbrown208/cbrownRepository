using LiteDB;
using Tools.Core.Models;

namespace Tools.DataAccess
{
	public class ManualBackupRepository
	{
		public string DbFileLocation;
		public ManualBackupRepository()
		{
			DbFileLocation = @"ManualBackupLogDb.db";
		}

		public void AddRecord(LogStats manualBackupRecord)
		{
			using (var db = new LiteDatabase(DbFileLocation))
			{
				// Get customer collection
				var backupRecords = db.GetCollection<LogStats>("LogStats");
				backupRecords.Insert(manualBackupRecord);
				
				backupRecords.EnsureIndex(x => x.Id);
			}
		}

		public LogStats GetBackupRecord(int id)
		{
			using (var db = new LiteDatabase(DbFileLocation))
			{
				// Get customer collection
				var backupRecords = db.GetCollection<LogStats>("LogStats");
				var results = backupRecords.FindById(id);
				return results;
			}
		}
	}
}
